using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace HttpHelper
{

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 保存成功！
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //string str1 = UploadImage("https://erpbim.justtruth.cn/yuanzhu_project_bim/upload_file", @"C:\Users\Victor\Desktop\c542c8f75e554c8693d11462c502dda5.jpg", "6d1951d2bd93f73f50d2f419757b4bef0609ac3c");
            string str1 = UploadImage("http://192.168.31.166:8069/yuanzhu_project_bim/upload_file", @"C:\Users\Victor\Desktop\c542c8f75e554c8693d11462c502dda5.jpg", "cb3ce2d3fc08cf216ab17177f225e54d431e56e4");




            Console.WriteLine(str1);

            Root r = JsonConvert.DeserializeObject<Root>(str1);

            Dictionary<string, object> dic = new Dictionary<string, object>();


            //{
            //    "task_id":40,
            //    "date": "2020-03-22",
            //    "weather": "晴",
            //    "description": "进度描述",
            //    "photos": [1329],
            //    "is_delay": true,
            //    "delay_type": "party_a",
            //    "delay_reason": "延期说明",
            //    "delay_solution": "解决措施"
            //}

            Root1 r1 = new Root1();
            r1.task_id =62;
            r1.date = "2020-03-22";
            r1.weather = "晴";
            r1.description = "进度描述";
            r1.photos =new List<int> { r.data.id};
            r1.is_delay = "true";
            r1.delay_type = "party_a";
            r1.delay_reason = "延期说明";
            r1.delay_solution = "解决措施";


            //地址
            string _url = "http://192.168.31.166:8069/yuanzhu_project_bim_construction_task/diary/store";
            //        //json参数
            //        string jsonParam = JsonConvert.SerializeObject(r1);
            //        var request = (HttpWebRequest)WebRequest.Create(_url);
            //        request.Method = "POST";
            //        request.ContentType = "text/html,application/xml,application/json";
            //        request.Headers.Add("Token", "cb3ce2d3fc08cf216ab17177f225e54d431e56e4");
            //        byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            //        int length = byteData.Length;
            //        request.ContentLength = length;
            //        Stream writer = request.GetRequestStream();
            //        writer.Write(byteData, 0, length);
            //        writer.Close();
            //        var response = (HttpWebResponse)request.GetResponse();
            //        var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            string jsonParam = JsonConvert.SerializeObject(r1);
           string responseString = Post(_url, jsonParam, "cb3ce2d3fc08cf216ab17177f225e54d431e56e4");

            Console.WriteLine(responseString);




          //  dic.Add("task_id", 60);
          //  dic.Add("date", "2020-03-22");
          //  dic.Add("weather", "晴");
          //  dic.Add("description", "进度描述");
          //  dic.Add("photos", r.data.id);
          //  dic.Add("is_delay", true);
          //  dic.Add("delay_reason", "延期说明");
          //  dic.Add("delay_solution", "解决措施");


          ////  string str = Post("http://192.168.31.166:8069/yuanzhu_project_bim_construction_task/diary/store", dic);
          //  Console.WriteLine(str);
            Console.Read();
        }
        
      

        public static string Get(string url, string Token)
        {
            string result = "";

            HttpWebRequest req;
            HttpWebResponse resp;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);

            }
            catch (Exception e)
            {
                string ss = e.ToString();
                throw;
            }


            if (Token != null)
            {
                req.Headers.Add("Token", Token);
            }



            try
            {
                resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                try
                {
                    //获取内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }

                }
                finally
                {
                    stream.Close();
                }
            }
            catch (Exception)
            {

                return "网络故障";
            }

            finally
            {
                req.Abort();


            }


            return result;
        }


        public static string Post(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }


        public static string Post(string url, string data1, string Token)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            if (Token != null)
            {
                req.Headers.Add("Token", Token);
            }


            req.Method = "POST";
            req.ContentType = "text/html,application/xml,application/json";
            #region 添加Post 参数  
           
            byte[] data = Encoding.UTF8.GetBytes(data1);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }






        static public string UploadImage(string uploadUrl, string imgPath, string token)
        {

            HttpWebRequest request = WebRequest.Create(uploadUrl) as HttpWebRequest;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            if (token != null)
            {
                request.Headers.Add("Token", token);
            }

            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            int pos = imgPath.LastIndexOf("\\");
            string fileName = imgPath.Substring(pos + 1);

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            return sr.ReadToEnd();
        }










        static public byte[] GetBufffers(string url, string Token)
        {

            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);


            if (Token != null)
            {
                req.Headers.Add("Token", Token);
            }



            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();


            Stream stream = resp.GetResponseStream();











            byte[] bytes = new byte[stream.Length];




            stream.Read(bytes, 0, bytes.Length);

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);

            }



            // 设置当前流的位置为流的开始 


            return bytes;
        }

        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary> 
        /// 将 Stream 写入文件 
        /// </summary> 
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public static Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }





    public class Root1
    {
        /// <summary>
        /// 
        /// </summary>
        public int task_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 晴
        /// </summary>
        public string weather { get; set; }
        /// <summary>
        /// 进度描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> photos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_delay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string delay_type { get; set; }
        /// <summary>
        /// 延期说明
        /// </summary>
        public string delay_reason { get; set; }
        /// <summary>
        /// 解决措施
        /// </summary>
        public string delay_solution { get; set; }
    }
}
