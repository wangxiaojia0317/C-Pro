
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 配置文件设置
{
    class Program
    {
        static void Main(string[] args)
        {
        }

#if false
        #region MyRegion
                  /*
              <appSettings>
                <!-- add key="ConnectionString" value="Server=192.168.18.87;Database=db_SmartFactory;Integrated Security=false;uid=sa;pwd=!QAZ2wsx;Connect Timeout=800;" / -->
                <add key="ConnectionString"
                     value="Data Source=localhost; Initial Catalog=db_TestHoumt; Integrated Security=True; Connect Timeout=800;" />
                <!-- <add key="ConnectionString" value="Data Source=localhost; Initial Catalog=db_TestZhangsq; Integrated Security=True; Connect Timeout=800;" />-->
                <!-- <add key="ConnectionString" value="Data Source=localhost; Initial Catalog=db_TestLiqi2; Integrated Security=True; Connect Timeout=800;" /> -->
                <add key="ClientSettingsProvider.ServiceUri"
                     value="" />
                <add key="RunningMode"
                     value="0" />
                <!-- 1: factory 0: simulate -->
              </appSettings>*/
            /*AppSettings作为内置函数，以属性的思想介入，注意配置文件中的格式，<add key=""
             value=""/>*/
            string mode =  ConfigurationManager.AppSettings["RunningMode"];
            Console.WriteLine(mode);


        #endregion

           
        #region MyRegion
          //< RedisConfig WriteServerList = "192.168.3.22:6379"
            //ReadServerList = "192.168.3.22:6379"
            //MaxWritePoolSize = "25"
            //MaxReadPoolSize = "25"
            //AutoStart = "true"
            //LocalCacheTime = "36000"
            //RecordeLog = "false" >
            //</ RedisConfig >

            /*作为一个section（部分），需要作为最高的节点优先级，以=为分界线，有多少行就有多少个数据，以键值对的格式存储*/
            IDictionary IDRedis = (IDictionary)ConfigurationManager.GetSection("RedisConfig");

            foreach (DictionaryEntry item in IDRedis)
            {
                Console.WriteLine(item.Key+":"+item.Value);
            }
        #endregion

        #region MyRegion
         //配置文件的一组组关节
            ConfigurationSectionGroup hardwareDesList = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["HardwareConfiguration"];
            foreach (ConfigurationSection sect in hardwareDesList.Sections)
            {
                IDictionary dic =  (IDictionary)ConfigurationManager.GetSection(sect.SectionInformation.SectionName);
               
                foreach (DictionaryEntry item in dic)
                {
                    Console.WriteLine($"{item.Key}:{item.Value}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        #endregion

#endif
    }
}
