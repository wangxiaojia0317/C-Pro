using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoftRegister
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            while (true)
            {
                a++;
                if (a>50)
                {
                    break;
                }
                RegisterClass rc = new RegisterClass();
                string cpuId = rc.getCpu();
                Console.WriteLine("CPUID:" + cpuId);
                string diskId = rc.GetDiskVolumeSerialNumber();
                Console.WriteLine("硬盘ID:" + diskId);
                string macCode = rc.GetMacCode();
                Console.WriteLine("机器码:" + macCode);
                string regCode = rc.GetRegisterCode(macCode);
                Console.WriteLine("注册码:"+regCode);
                Thread.Sleep(3000);
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.Read();
        }

        #region 硬件信息
        /// <summary>
        /// 获取传感器ID（16位HASH代码）
        /// </summary>
        /// <returns></returns>
        private static string GetSensorID()
        {
            string sensorID = string.Empty;
            string basicID = GetCPUID() + GetMotherboardID() + GetPhysicalMemoryID();
            System.Security.Cryptography.MD5CryptoServiceProvider mD5CryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBuff = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(basicID));
            for (int i = 4; i < 12; i++)
            {
                sensorID += hashedBuff[i].ToString("X2");
            }
            return sensorID;
        }


        /// <summary>
        /// 获得cpu序列号
        /// </summary>
        /// <returns></returns>
        private static string GetCPUID()
        {
            var myCpu = new ManagementClass("win32_Processor").GetInstances();
            var serial = "";
            foreach (ManagementObject cpu in myCpu)
            {
                var val = cpu.Properties["Processorid"].Value;
                serial += val == null ? "" : val.ToString();
            }
            return serial;
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        private static string GetMotherboardID()
        {
            var myMb = new ManagementClass("Win32_BaseBoard").GetInstances();
            var serial = "";
            foreach (ManagementObject mb in myMb)
            {
                var val = mb.Properties["SerialNumber"].Value;
                serial += val == null ? "" : val.ToString();
            }
            return serial;
        }

        /// <summary>
        /// 获取所有内存信息，参考 CPUID 软件
        /// </summary>
        /// <returns></returns>
        private static string GetPhysicalMemoryID()
        {
            string memoryID = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (var prop in mos.Get())
            {
                memoryID = memoryID + prop["PartNumber"].ToString().Trim() + prop["SerialNumber"].ToString().Trim();
            }
            return memoryID;
        }
        #endregion



    }
    class RegisterClass
    {

        #region 获取注册码
        //获得CUP序列号和硬盘序列号的实现代码如下
        public string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        public string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"d:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }



        //获取机器码
        public string GetMacCode()
        {
            string code = getCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
            string[] strid = new string[24];//
            for (int i = 0; i < 24; i++)//把字符赋给数组
            {
                strid[i] = code.Substring(i, 1);
            }
            code = "";
            Random rdid = new Random();
            for (int i = 0; i < 24; i++)//从数组随机抽取24个字符组成新的字符生成机器三
            {
                code += strid[rdid.Next(0, 24)];
            }
            return code;
        }



        public int[] intCode = new int[127];//用于存密钥
        public void setIntCode()//给数组赋值个小于10的随机数
        {
            Random ra = new Random();
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = ra.Next(0, 9);
            }
        }
        public int[] intNumber = new int[25];//用于存机器码的Ascii值
        public char[] Charcode = new char[25];//存储机器码字

        //生成注册码
        public string GetRegisterCode(string macCode)
        {
            if (macCode != "")
            {
                //把机器码存入数组中
                setIntCode();//初始化127位数组
                for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
                {
                    Charcode[i] = Convert.ToChar(macCode.Substring(i - 1, 1));
                }//
                for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
                {
                    intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);

                }
                string strAsciiName = null;//用于存储机器码
                for (int j = 1; j < intNumber.Length; j++)
                {
                    //MessageBox.Show((Convert.ToChar(intNumber[j])).ToString());
                    if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                    {
                        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                    }
                    else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                    {
                        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                    }
                    else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                    {
                        strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                    }
                    else//判断字符ASCII值不在以上范围内
                    {
                        if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                        { strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString(); }
                        else
                        {
                            strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                        }

                    }


                }
                return strAsciiName;//得到注册码
            }
            else
            {
                return "请生成机器码";
            }
        }
        #endregion



        #region 获取当前是否被注册

        #endregion


        #region 获取当前使用的次数

        #endregion


        #region 获取

        #endregion


    }

}
