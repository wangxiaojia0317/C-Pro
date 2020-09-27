using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO.Compression;

namespace Weather
{

  
    #region MyRegion
    public class resp
    {
        public string city { get; set; }
        public string updatetime { get; set; }
        public string wendu { get; set; }
        public string fengli { get; set; }
        public string shidu { get; set; }
        public string fengxiang { get; set; }
        public environment environment { get; set; }
        public alarm alarm { get; set; }
        public List<weather> forecast { set; get; }
    }
    public class environment
    {
        public string aqi { get; set; }
        public string pm25 { get; set; }
        public string suggest { get; set; }
        public string quality { get; set; }
        public string MajorPollutants { get; set; }
        public string time { get; set; }
    }
    public class alarm
    {
        public string cityName { get; set; }
        public string alarmType { get; set; }
        public string alarmDegree { get; set; }
        public string alarmText { get; set; }
        public string alarm_details { get; set; }
        public string standard { get; set; }
        public string suggest { get; set; }
    }
    public class weather
    {
        public string date { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public climate day { get; set; }
        public climate night { get; set; }
    }
    public class climate
    {
        public string type { get; set; }
        public string fengxiang { get; set; }
        public string fengli { get; set; }
    }

    #endregion




    class Program
    {

        private static string getHtml2(string url)
        {
            StringBuilder s = new StringBuilder(102400);
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
            HttpWebResponse response = (HttpWebResponse)wr.GetResponse(); head(response);
            GZipStream g = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
            byte[] d = new byte[20480];
            int l = g.Read(d, 0, 20480);
            while (l > 0)
            {
                s.Append(Encoding.UTF8.GetString(d, 0, l));
                l = g.Read(d, 0, 20480);
            }
            return s.ToString();
        }
        private static void head(HttpWebResponse r)
        {
            string[] keys = r.Headers.AllKeys; for (int i = 0; i < keys.Length; ++i)
            {
                Console.WriteLine(keys[i] + "   " + r.Headers[keys[i]]);
            }
        }
        public static T XmlDeSeralizer<T>(string xmlStr) where T : class, new()
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlStr))
            {
                return xs.Deserialize(reader) as T;
            }
        }

        static void Main(string[] args) {
            string weatherInfoUrl = "http://wthrcdn.etouch.cn/WeatherApi?citykey=" + "101181701";
            string weatherstr = getHtml2(weatherInfoUrl);
            resp tempInfo = XmlDeSeralizer<resp>(weatherstr);

            //Console.WriteLine(tempInfo.city);
            //Console.WriteLine(tempInfo.alarm);
            Console.WriteLine(tempInfo.alarm);
            Console.WriteLine(tempInfo.city);
            Console.WriteLine(tempInfo.environment);
            Console.WriteLine(tempInfo.fengli);
            Console.WriteLine(tempInfo.fengxiang);
            foreach (var item in tempInfo.forecast)
            {
                Console.WriteLine(item.date);
                Console.WriteLine(item.low+"-"+item.high);
            }
            Console.WriteLine(tempInfo.shidu);
            Console.WriteLine(tempInfo.updatetime);
            Console.WriteLine(tempInfo.wendu);
           
            Console.Read();

        }

      

       
    }
}
