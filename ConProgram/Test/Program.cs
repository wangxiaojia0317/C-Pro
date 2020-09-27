using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Concurrent;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.IO;
using Microsoft.International.Converters.PinYinConverter;//导入拼音相关
using Newtonsoft.Json;
using Newtonsoft;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Test
{
    public class Subcontractor
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

    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine(Guid.NewGuid());
            Console.Read();
        }
       static StringBuilder sb = new StringBuilder();
        static void FileReadByFileStream()
        {
            string filePath = @"C:\Users\Victor\Desktop\llll\MaterialNode_3_100.js";
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
          
                //尚未读取的文件内容长度  
                long left = fs.Length;
                //存储读取结果  
                byte[] bytes = new byte[240000];
                //每次读取长度  
                int maxLength = bytes.Length;
                //读取位置  
                int start = 0;
                //实际返回结果长度  
                int num = 0;
                //当文件未读取长度大于0时，不断进行读取  
                while (left > 0)
                {
                    fs.Position = start;
                    num = 0;
                    if (left < maxLength)
                        num = fs.Read(bytes, 0, Convert.ToInt32(left));
                    else
                        num = fs.Read(bytes, 0, maxLength);
                    if (num == 0)
                        break;
                    start += num;
                    left -= num;
                    //sb.Append(Encoding.UTF8.GetString(bytes));
                }
            fs.Close();
            
        }

        [JsonConverter(typeof(MyConverter))]
        public class Model
        {
            public int ID { get; set; }
        }

        public class MyConverter : JsonConverter
        {
            //是否开启自定义反序列化，值为true时，反序列化时会走ReadJson方法，值为false时，不走ReadJson方法，而是默认的反序列化
            public override bool CanRead => true;
            //是否开启自定义序列化，值为true时，序列化时会走WriteJson方法，值为false时，不走WriteJson方法，而是默认的序列化
            public override bool CanWrite => true;

            public override bool CanConvert(Type objectType)
            {
                return typeof(Model) == objectType;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var model = new Model();
                //获取JObject对象，该对象对应着我们要反序列化的json

                try
                {
                    var jobj = serializer.Deserialize<JObject>(reader);



                }
                catch
                {

                    model.ID = -1;
                }
               
               
                return model;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                //new一个JObject对象,JObject可以像操作对象来操作json
                var jobj = new JObject();
                //value参数实际上是你要序列化的Model对象，所以此处直接强转
                var model = value as Model;
                if (model.ID != 1)
                {
                    //如果ID值为1，添加一个键位"ID"，值为false
                    jobj.Add("ID", false);
                }
                else
                {
                    jobj.Add("ID", false);
                }
                //通过ToString()方法把JObject对象转换成json
                var jsonstr = jobj.ToString();
                //调用该方法，把json放进去，最终序列化Model对象的json就是jsonstr，由此，我们就能自定义的序列化对象了
                writer.WriteValue(jsonstr);
            }
        }



    }

}
