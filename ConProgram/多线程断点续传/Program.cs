using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace 多线程断点续传
{
    class Program
    {
        static void Main(string[] args)
        {

            Test();
            Console.WriteLine("写入完成");
            Console.Read();
        }


        public static void Test()
        {
            string path1 = @"https://www.cnblogs.com/wangqiang3311/p/8986603.html";
            string path2 = @"2.txt";
            HttpWebRequest request = WebRequest.Create(path1) as HttpWebRequest;
            // request.AllowAutoRedirect = false;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            using (Stream stream = response.GetResponseStream())
            {
                using (FileStream fs = new FileStream(path2, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    byte[] buffer = new byte[10];

                    int a = 0;
                    while (true)
                    {
                        a = stream.Read(buffer, 0, buffer.Length);
                        if (a <= 0)
                        {
                            break;
                        }
                        fs.Write(buffer, 0, buffer.Length);
                    }
                    
                }
            }

          
            using (FileStream fs = new FileStream(path2, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);

                while (sr.ReadLine() != null)
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }


        }




        //这个方法可以放到Remoting或者WCF服务中去，然后本地调用该方法即可实现多线程断点续传
        public static byte[] GetFile(int start, int length)
        {
            string SeverFilePath = @"E:\AC_Victor\My\PDF书籍\Net\微软.NET程序的加密与解密.pdf";
            using (FileStream ServerStream = new FileStream(SeverFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024 * 80, true))
            {
                byte[] buffer = new byte[length];
                ServerStream.Position = start;
                //ServerStream.Seek(start, SeekOrigin.Begin);
                ServerStream.Read(buffer, 0, length);
                return buffer;
            }
        }


        public static void DownLoadWithThreads()
        {
            string LocalSavePath = @"C:\Users\AC-深蓝\Desktop\微软.NET程序的加密与解密.pdf";  //本地目标文件路径

            FileInfo SeverFilePath = new FileInfo(@"E:\AC_Victor\My\PDF书籍\Net\微软.NET程序的加密与解密.pdf"); //服务器待文件路径
            long FileLength = SeverFilePath.Length; //待下载文件大小


            Console.WriteLine("Start Configuration");
            int PackCount = 0;  //初始化数据包个数

            long PackSize = 1024000; //数据包大小

            if (FileLength % PackSize > 0)
            {
                PackCount = (int)(FileLength / PackSize) + 1;
            }

            else
            {
                PackCount = (int)(FileLength / PackSize);
            }


            Console.WriteLine("Start Recieve");
            var tasks = new Task[PackCount];  //多线程任务

            for (int index = 0; index < PackCount; index++)
            {


                int Threadindex = index; //这步很关键，在Task()里的绝对不能直接使用index
                var task = new Task(() =>
                {
                    string tempfilepath = @"C:\Users\AC-深蓝\Desktop\Temp\" + "QS_" + Threadindex + "_" + PackCount; //临时文件路径

                    using (FileStream tempstream = new FileStream(tempfilepath, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        int length = (int)Math.Min(PackSize, FileLength - Threadindex * PackSize);

                        var bytes = GetFile(Threadindex * PackCount, length);

                        tempstream.Write(bytes, 0, length);
                        tempstream.Flush();
                        tempstream.Close();
                        tempstream.Dispose();
                    }
                });
                tasks[Threadindex] = task;
                task.Start();
            }

            Task.WaitAll(tasks); //等待所有线程完成
            Console.WriteLine("Recieve End");


            //检测有哪些数据包未下载
            Console.WriteLine("Start Compare");
            DirectoryInfo TempDir = new DirectoryInfo(@"C:\Users\AC-深蓝\Desktop\temp"); //临时文件夹路径
            List<string> Comparefiles = new List<string>();

            for (int i = 0; i < PackCount; i++)
            {
                bool hasfile = false;
                foreach (FileInfo Tempfile in TempDir.GetFiles())
                {
                    if (Tempfile.Name.Split('_')[1] == i.ToString())
                    {
                        hasfile = true;
                        break;
                    }
                }
                if (hasfile == false)
                {
                    Comparefiles.Add(i.ToString());
                }
            }

            //最后补上这些缺失的文件
            if (Comparefiles.Count > 0)
            {
                foreach (string com_index in Comparefiles)
                {
                    string tempfilepath = @"C:\Users\AC-深蓝\Desktop\Temp\" + "QS_" + com_index + "_" + PackCount;
                    using (FileStream Compstream = new FileStream(tempfilepath, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        int length = (int)Math.Min(PackSize, FileLength - Convert.ToInt32(com_index) * PackSize);
                        var bytes = GetFile(Convert.ToInt32(com_index) * PackCount, length);
                        Compstream.Write(bytes, 0, length);
                        Compstream.Flush();
                        Compstream.Close();
                        Compstream.Dispose();
                    }
                }

            }
            Console.WriteLine("Compare End");


            //准备将临时文件融合并写到微软.NET程序的加密与解密.pdf中
            Console.WriteLine("Start Write");
            using (FileStream writestream = new FileStream(LocalSavePath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                foreach (FileInfo Tempfile in TempDir.GetFiles())
                {
                    using (FileStream readTempStream = new FileStream(Tempfile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long onefileLength = Tempfile.Length;
                        byte[] buffer = new byte[Convert.ToInt32(onefileLength)];
                        readTempStream.Read(buffer, 0, Convert.ToInt32(onefileLength));
                        writestream.Write(buffer, 0, Convert.ToInt32(onefileLength));
                    }
                }
                writestream.Flush();
                writestream.Close();
                writestream.Dispose();
            }
            Console.WriteLine("Write End");



            //删除临时文件
            Console.WriteLine("Start Delete Temp Files");
            foreach (FileInfo Tempfile in TempDir.GetFiles())
            {
                Tempfile.Delete();
            }
            Console.WriteLine("Delete Success");
            Console.ReadKey();

        }




        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static bool HttpDownload(string url, string path)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
  
}
