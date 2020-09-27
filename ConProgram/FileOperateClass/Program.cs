using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.ComponentModel;
using ICSharpCode.SharpZipLib.Zip;
using System.Net;
using System.Drawing;

namespace FileOperateClass
{
    class Program
    {

        public static void CreateDir(List<string> paths, string rootPath)
        {
            string temp = string.Empty;
            for (int i = 0; i < paths.Count; i++)
            {
                temp += paths[i];
                if (!Directory.Exists(rootPath + "/" + temp))
                {
                    Directory.CreateDirectory(rootPath + "/" + temp);
                }

            }
        }
        static void Main(string[] args)
        {
            string str = "icon-剖切-normal";
          string ss=  str.Replace("noamal","touch");
            Console.WriteLine(str);
            Console.WriteLine(ss);
            Console.Read();
        }

        public static void DeleteFile(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                File.Delete(path);
            }
        }

    }

    public class File_Test
    {
        /// <summary>
        /// 文件流按行读取
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> ReadStream_Fun(string path)
        {
            List<string> list = new List<string>();
            if (path==null)
            {
                return null;
            }
            using (FileStream fs=new FileStream(path,FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                while (sr.ReadLine()!=null)
                {
                    list.Add(sr.ReadLine());
                    #if DEBUG
                    Console.WriteLine(sr.ReadLine());
                    #endif
                }
            }
            return list;

        }
        public static List<string> ReadFile_Fun(string path)
        {
            if (path==null)
            {
                return null;
            }
           return File.ReadAllLines(path).ToList();
        }


        /// <summary>
        /// 文件hash比较
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public bool CompareFile(string str1, string str2)
        {
            string p_1 = str1;
            string p_2 = str2;
            //计算第一个文件的哈希值
            var hash = System.Security.Cryptography.HashAlgorithm.Create();
            var stream_1 = new System.IO.FileStream(p_1, System.IO.FileMode.Open);
            byte[] hashByte_1 = hash.ComputeHash(stream_1);
            stream_1.Close();
            //计算第二个文件的哈希值
            var stream_2 = new System.IO.FileStream(p_2, System.IO.FileMode.Open);
            byte[] hashByte_2 = hash.ComputeHash(stream_2);
            stream_2.Close();
            //比较两个哈希值
            if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public class RemoteDownload
        {
            public static void DownLoad(string addressUrl, string localName)
            {
                //下载文件
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myWebClient.DownloadFile(@"/10.2.0.254/software/01279.lic.txt", "testdownload.txt");
                //下载end
            }
        }


    }

    public class WebDownload
    {
        public static void DownLoad(string Url, string FileName)
        {
            bool Value = false;
            WebResponse response = null;
            Stream stream = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                response = request.GetResponse();
                stream = response.GetResponseStream();

                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    Value = SaveBinaryFile(response, FileName);

                }

            }
            catch (Exception err)
            {
                string aa = err.ToString();
            }

        }

        /// <summary>
        /// Save a binary file to disk.
        /// </summary>
        /// <param name="response">The response used to save the file</param>
        // 将二进制文件保存到磁盘
        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            bool Value = true;
            byte[] buffer = new byte[1024];

            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = System.IO.File.Create(FileName);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }
    }



    public class FtpDownload
    {
        public static void DownLoad(string FtpPath)
        {
            /*首先从配置文件读取ftp的登录信息*/
            string TempFolderPath = System.Configuration.ConfigurationManager.AppSettings["TempFolderPath"].ToString();
            string FtpUserName = System.Configuration.ConfigurationManager.AppSettings["FtpUserName"].ToString();
            string FtpPassWord = System.Configuration.ConfigurationManager.AppSettings["FtpPassWord"].ToString();
            string LocalFileExistsOperation = System.Configuration.ConfigurationManager.AppSettings["LocalFileExistsOperation"].ToString();


            Uri uri = new Uri(FtpPath);
            string FileName = Path.GetFullPath(TempFolderPath) + Path.DirectorySeparatorChar.ToString() + Path.GetFileName(uri.LocalPath);

            //创建一个文件流
            FileStream fs = null;
            Stream responseStream = null;
            try
            {
                //创建一个与FTP服务器联系的FtpWebRequest对象
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                //设置请求的方法是FTP文件下载
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                //连接登录FTP服务器
                request.Credentials = new NetworkCredential(FtpUserName, FtpPassWord);

                //获取一个请求响应对象
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //获取请求的响应流
                responseStream = response.GetResponseStream();

                //判断本地文件是否存在，如果存在，则打开和重写本地文件

                if (File.Exists(FileName))
                {
                    if (LocalFileExistsOperation == "write")
                    {
                        fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite);

                    }
                }

                //判断本地文件是否存在，如果不存在，则创建本地文件
                else
                {
                    fs = File.Create(FileName);
                }

                if (fs != null)
                {

                    int buffer_count = 65536;
                    byte[] buffer = new byte[buffer_count];
                    int size = 0;
                    while ((size = responseStream.Read(buffer, 0, buffer_count)) > 0)
                    {
                        fs.Write(buffer, 0, size);

                    }
                    fs.Flush();
                    fs.Close();
                    responseStream.Close();
                }
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (responseStream != null)
                    responseStream.Close();
            }


        }
    }



    /// <summary>
    /// 问价压缩与解压缩
    /// </summary>
    public class CompressHelper
    {
        /// <summary>
        /// 压缩多个文件/文件夹
        /// </summary>
        /// <param name="sourceList">源文件/文件夹路径列表</param>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="compressionLevel">压缩等级，范围从0到9，可选，默认为6</param>
        /// <returns></returns>
        public static bool CompressFile(string filePath, IEnumerable<string> sourceList, string zipFilePath,
             string comment = null, string password = null, int compressionLevel = 6)
        {
            bool result = false;

            try
            {
                //检测目标文件所属的文件夹是否存在，如果不存在则建立
                string zipFileDirectory = Path.GetDirectoryName(zipFilePath);
                if (!Directory.Exists(zipFileDirectory))
                {
                    Directory.CreateDirectory(zipFileDirectory);
                }

                Dictionary<string, string> dictionaryList = new Dictionary<string, string>();
                string[] str = Directory.GetFiles(filePath);

                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    zipStream.Password = password;//设置密码
                    zipStream.SetComment(comment);//添加注释
                    zipStream.SetLevel(compressionLevel);//设置压缩等级

                    foreach (string key in str)//从字典取文件添加到压缩文件
                    {
                        if (File.Exists(key))//判断是文件还是文件夹
                        {
                            FileInfo fileItem = new FileInfo(key);

                            using (FileStream readStream = fileItem.Open(FileMode.Open,
                                FileAccess.Read, FileShare.Read))
                            {
                                ZipEntry zipEntry = new ZipEntry(key + "_zip");
                                zipEntry.DateTime = fileItem.LastWriteTime;
                                zipEntry.Size = readStream.Length;
                                zipStream.PutNextEntry(zipEntry);
                                int readLength = 0;
                                byte[] buffer = new byte[4096];

                                do
                                {
                                    readLength = readStream.Read(buffer, 0, 4096);
                                    zipStream.Write(buffer, 0, readLength);
                                } while (readLength == 4096);
                                Console.WriteLine(key + "压缩完毕");
                                readStream.Close();
                            }
                        }
                        else//对文件夹的处理
                        {
                            ZipEntry zipEntry = new ZipEntry(dictionaryList[key] + "/");
                            zipStream.PutNextEntry(zipEntry);
                        }
                    }

                    zipStream.Flush();
                    zipStream.Finish();
                    zipStream.Close();
                }

                result = true;
            }
            catch (System.Exception ex)
            {
                throw new Exception("压缩文件失败", ex);
            }

            return result;
        }

        /// <summary>
        /// 解压文件到指定文件夹
        /// </summary>
        /// <param name="sourceFile">压缩文件</param>
        /// <param name="destinationDirectory">目标文件夹，如果为空则解压到当前文件夹下</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool DecomparessFile(string sourceFile, string destinationDirectory = null, string password = null)
        {
            bool result = false;

            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException("要解压的文件不存在", sourceFile);
            }

            if (string.IsNullOrWhiteSpace(destinationDirectory))
            {
                destinationDirectory = Path.GetDirectoryName(sourceFile);
            }

            try
            {
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                using (ZipInputStream zipStream = new ZipInputStream(File.Open(sourceFile, FileMode.Open,
                    FileAccess.Read, FileShare.Read)))
                {
                    zipStream.Password = password;
                    ZipEntry zipEntry = zipStream.GetNextEntry();

                    while (zipEntry != null)
                    {
                        if (zipEntry.IsDirectory)//如果是文件夹则创建
                        {
                            Directory.CreateDirectory(Path.Combine(destinationDirectory,
                                Path.GetDirectoryName(zipEntry.Name)));
                        }
                        else
                        {
                            string fileName = Path.GetFileName(zipEntry.Name);
                            if (!string.IsNullOrEmpty(fileName) && fileName.Trim().Length > 0)
                            {
                                FileInfo fileItem = new FileInfo(Path.Combine(destinationDirectory, zipEntry.Name.Split('/').Last()));

                                zipEntry.Name.Split('/').Last();
                                using (FileStream writeStream = fileItem.Create())
                                {
                                    byte[] buffer = new byte[4096];
                                    int readLength = 0;

                                    do
                                    {
                                        readLength = zipStream.Read(buffer, 0, 4096);
                                        writeStream.Write(buffer, 0, readLength);
                                    } while (readLength == 4096);

                                    writeStream.Flush();
                                    writeStream.Close();
                                }
                                fileItem.LastWriteTime = zipEntry.DateTime;
                            }
                        }

                        zipEntry = zipStream.GetNextEntry();//获取下一个文件
                    }

                    zipStream.Close();
                }
                result = true;
            }
            catch (System.Exception ex)
            {
                throw new Exception("文件解压发生错误", ex);
            }

            return result;
        }
    }



    public class FileConvert
    {
        /// <summary>        /// base64 转 Image        /// </summary>        /// <param name="base64"></param>        
        public static void Base64ToImage(string base64,string path)
        {
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换         
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            Image mImage = Image.FromStream(memStream);
            Bitmap bp = new Bitmap(mImage);
            // bp.Save("C:/Users/Administrator/Desktop/" + DateTime.Now.ToString("yyyyMMddHHss") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//注意保存路径        }         /// <summary>        /// Image 转成 base64        /// </summary>        /// <param name="fileFullName"></param>        
            bp.Save(path);
        }
        public static string ImageToBase64(string fileFullName)
        {
            try
            {
                Bitmap bmp = new Bitmap(fileFullName);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length]; ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length); ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }


}
