using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("CPU_ID:" + ClassComputerOp.Instance().CpuID);
            Console.WriteLine("MacAddress:" + ClassComputerOp.Instance().MacAddress);
            Console.WriteLine("DiskID:" + ClassComputerOp.Instance().DiskID);
            Console.WriteLine("IpAddress:" + ClassComputerOp.Instance().IpAddress);
            Console.WriteLine("LoginUserName:" + ClassComputerOp.Instance().LoginUserName);
            Console.WriteLine("ComputerName:" + ClassComputerOp.Instance().ComputerName);
            Console.WriteLine("SystemType:" + ClassComputerOp.Instance().SystemType);
            Console.WriteLine("TotalPhysicalMemory:" + ClassComputerOp.Instance().TotalPhysicalMemory);


            Console.Read();
        }
    }


    class ClassComputerOp
    {
        public string CpuID;
        public string MacAddress;
        public string DiskID;
        public string IpAddress;
        public string LoginUserName;
        public string ComputerName;
        public string SystemType;
        public string TotalPhysicalMemory; //单位：M
        private static ClassComputerOp _instance;
        public static ClassComputerOp Instance()
        {
            if (_instance == null)
                _instance = new ClassComputerOp();
            return _instance;
        }
        protected ClassComputerOp()
        {
            CpuID = GetCpuID();
            MacAddress = GetMacAddress();
            DiskID = GetDiskID();
            IpAddress = GetIPAddress();
            LoginUserName = GetUserName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }
        string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码
                string cpuInfo = "";//cpu序列号
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        string GetIPAddress()
        {
            try
            {
                //获取IP地址
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString();
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        string GetDiskID()
        {
            try
            {
                //获取硬盘ID
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        /// <summary>
        /// 操作系统的登录用户名
        /// </summary>
        /// <returns></returns>
        string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["UserName"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }


        /// <summary>
        /// PC类型
        /// </summary>
        /// <returns></returns>
        string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["SystemType"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }

        /// <summary>
        /// 物理内存
        /// </summary>
        /// <returns></returns>
        string GetTotalPhysicalMemory()
        {
            try
            {

                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["TotalPhysicalMemory"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }


        //
        // 取得设备硬盘的卷标号 此方法为取硬盘逻辑分区序列号，重新格式化会改变
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }



        // 取得设备硬盘的物理序列号    
        public static string GetDiskSerialNumber()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query = new SelectQuery("Win32_DiskDrive", "", new string[] { "PNPDeviceID", "Signature" });
            ManagementObjectCollection myCollection = mos.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject moo = em.Current;
            string id = moo.Properties["signature"].Value.ToString().Trim();
            return id;
        }



        public List<string> GetRemovableDeviceID()
        {
            List<string> deviceIDs = new List<string>();
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT  *  From  Win32_LogicalDisk ");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {

                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case (int)DriveType.Removable:   //可以移动磁盘       
                        {
                            //MessageBox.Show("可以移动磁盘");  
                            deviceIDs.Add(mo["DeviceID"].ToString());
                            break;
                        }
                    case (int)DriveType.Fixed:   //本地磁盘       
                        {
                            //MessageBox.Show("本地磁盘");  
                            deviceIDs.Add(mo["DeviceID"].ToString());
                            break;
                        }
                    case (int)DriveType.CDRom:   //CD   rom   drives       
                        {
                            //MessageBox.Show("CD   rom   drives ");  
                            break;
                        }
                    case (int)DriveType.Network:   //网络驱动     
                        {
                            //MessageBox.Show("网络驱动器 ");  
                            break;
                        }
                    case (int)DriveType.Ram:
                        {
                            //MessageBox.Show("驱动器是一个 RAM 磁盘 ");  
                            break;
                        }
                    case (int)DriveType.NoRootDirectory:
                        {
                            //MessageBox.Show("驱动器没有根目录 ");  
                            break;
                        }
                    default:   //defalut   to   folder       
                        {
                            //MessageBox.Show("驱动器类型未知 ");  
                            break;
                        }
                }

            }
            return deviceIDs;
        }


        //获取当前计算机逻辑磁盘名称列表
        String[] drives = Environment.GetLogicalDrives();
        //Console.WriteLine("GetLogicalDrives: {0}", String.Join(", ", drives));



    }
}



//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Management;
//using System.Text;
//using System.Threading.Tasks;

//namespace DeviceInfo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            ClassComputerOp cop = new ClassComputerOp();
//            cop.GetInfo();

//            Console.WriteLine();
//            Console.Read();
//        }
//    }


//    class ClassComputerOp
//    {

//        private string GetHardWareInfo(string item)
//        {
//            if (item == "" || item == null)
//            {
//                return "unknow";
//            }
//            string hardInfo = string.Empty;
//            string queryStr = string.Format("select * from {0}", item);

//            ManagementObjectSearcher objvide = new ManagementObjectSearcher(queryStr);
//            foreach (ManagementObject obj in objvide.Get())
//            {
//                hardInfo = obj["Name"].ToString();
//            }
//            return hardInfo;

//        }
//        List<string> infos = new List<string>() {
//            "Win32_Processor", // CPU 处理器 
//            "Win32_PhysicalMemory", // 物理内存条 
//            "Win32_Keyboard", // 键盘 
//            "Win32_PointingDevice", // 点输入设备，包括鼠标。 
//            "Win32_FloppyDrive", // 软盘驱动器 
//            "Win32_DiskDrive", // 硬盘驱动器 
//            "Win32_CDROMDrive", // 光盘驱动器 
//            "Win32_BaseBoard", // 主板 
//            "Win32_BIOS", // BIOS 芯片 
//            "Win32_ParallelPort", // 并口 
//            "Win32_SerialPort", // 串口 
//            "Win32_SerialPortConfiguration", // 串口配置 
//            "Win32_SoundDevice", // 多媒体设置，一般指声卡。 
//            "Win32_SystemSlot", // 主板插槽 (ISA & PCI & AGP) 
//            "Win32_USBController", // USB 控制器 
//            "Win32_NetworkAdapter", // 网络适配器 
//            "Win32_NetworkAdapterConfiguration", // 网络适配器设置 
//            "Win32_Printer", // 打印机 
//            "Win32_PrinterConfiguration", // 打印机设置 
//            "Win32_PrintJob", // 打印机任务 
//            "Win32_TCPIPPrinterPort", // 打印机端口 
//            "Win32_POTSModem", // MODEM 
//            "Win32_POTSModemToSerialPort", // MODEM 端口 
//            "Win32_DesktopMonitor", // 显示器 
//            "Win32_DisplayConfiguration", // 显卡 
//            "Win32_DisplayControllerConfiguration", // 显卡设置 
//            "Win32_VideoController", // 显卡细节。 
//            "Win32_VideoSettings", // 显卡支持的显示模式。 

//            // 操作系统 
//            "Win32_TimeZone", // 时区 
//            "Win32_SystemDriver", // 驱动程序 
//            "Win32_DiskPartition", // 磁盘分区 
//            "Win32_LogicalDisk", // 逻辑磁盘 
//            "Win32_LogicalDiskToPartition", // 逻辑磁盘所在分区及始末位置。 
//            "Win32_LogicalMemoryConfiguration", // 逻辑内存配置 
//            "Win32_PageFile", // 系统页文件信息 
//            "Win32_PageFileSetting", // 页文件设置 
//            "Win32_BootConfiguration", // 系统启动配置 
//            "Win32_ComputerSystem", // 计算机信息简要 
//            "Win32_OperatingSystem", // 操作系统信息 
//            "Win32_StartupCommand", // 系统自动启动程序 
//            "Win32_Service", // 系统安装的服务 
//            "Win32_Group", // 系统管理组 
//            "Win32_GroupUser", // 系统组帐号 
//            "Win32_UserAccount", // 用户帐号 
//            "Win32_Process", // 系统进程 
//            "Win32_Thread", // 系统线程 
//            "Win32_Share", // 共享 
//            "Win32_NetworkClient", // 已安装的网络客户端 
//            "Win32_NetworkProtoco" // 已安装的网络协议 
//        };


//        public void GetInfo()
//        {
//            foreach (var item in infos)
//            {
//                try
//                {
//                    Console.WriteLine(item + ":" + GetHardWareInfo(item));
//                }
//                catch
//                {
//                    Console.WriteLine(item + "获取失败");
//                }
//            }

//        }


//        //item参数为“Win32_Processor”，不同的参数对应着不通的信息，常用的对应关系如下：

//        //// 硬件 
//        //Win32_Processor, // CPU 处理器 
//        //Win32_PhysicalMemory, // 物理内存条 
//        //Win32_Keyboard, // 键盘 
//        //Win32_PointingDevice, // 点输入设备，包括鼠标。 
//        //Win32_FloppyDrive, // 软盘驱动器 
//        //Win32_DiskDrive, // 硬盘驱动器 
//        //Win32_CDROMDrive, // 光盘驱动器 
//        //Win32_BaseBoard, // 主板 
//        //Win32_BIOS, // BIOS 芯片 
//        //Win32_ParallelPort, // 并口 
//        //Win32_SerialPort, // 串口 
//        //Win32_SerialPortConfiguration, // 串口配置 
//        //Win32_SoundDevice, // 多媒体设置，一般指声卡。 
//        //Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP) 
//        //Win32_USBController, // USB 控制器 
//        //Win32_NetworkAdapter, // 网络适配器 
//        //Win32_NetworkAdapterConfiguration, // 网络适配器设置 
//        //Win32_Printer, // 打印机 
//        //Win32_PrinterConfiguration, // 打印机设置 
//        //Win32_PrintJob, // 打印机任务 
//        //Win32_TCPIPPrinterPort, // 打印机端口 
//        //Win32_POTSModem, // MODEM 
//        //Win32_POTSModemToSerialPort, // MODEM 端口 
//        //Win32_DesktopMonitor, // 显示器 
//        //Win32_DisplayConfiguration, // 显卡 
//        //Win32_DisplayControllerConfiguration, // 显卡设置 
//        //Win32_VideoController, // 显卡细节。 
//        //Win32_VideoSettings, // 显卡支持的显示模式。 

//        //// 操作系统 
//        //Win32_TimeZone, // 时区 
//        //Win32_SystemDriver, // 驱动程序 
//        //Win32_DiskPartition, // 磁盘分区 
//        //Win32_LogicalDisk, // 逻辑磁盘 
//        //Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。 
//        //Win32_LogicalMemoryConfiguration, // 逻辑内存配置 
//        //Win32_PageFile, // 系统页文件信息 
//        //Win32_PageFileSetting, // 页文件设置 
//        //Win32_BootConfiguration, // 系统启动配置 
//        //Win32_ComputerSystem, // 计算机信息简要 
//        //Win32_OperatingSystem, // 操作系统信息 
//        //Win32_StartupCommand, // 系统自动启动程序 
//        //Win32_Service, // 系统安装的服务 
//        //Win32_Group, // 系统管理组 
//        //Win32_GroupUser, // 系统组帐号 
//        //Win32_UserAccount, // 用户帐号 
//        //Win32_Process, // 系统进程 
//        //Win32_Thread, // 系统线程 
//        //Win32_Share, // 共享 
//        //Win32_NetworkClient, // 已安装的网络客户端 
//        //Win32_NetworkProtocol, // 已安装的网络协议 

//    }
//}
