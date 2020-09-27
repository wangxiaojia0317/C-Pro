using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;


namespace SpringFamily
{
    class Program
    {
        static void Main(string[] args)
        {

            IOCDemo ioc = new IOCDemo();

            Console.ReadKey();

        }
    }


    #region IOC
    /// <summary>
    /// 注意配合XML文档，节点的配置
    /// 
    /// </summary>
    public class IOCDemo
    {
        public IOCDemo()
        {
            //控制权没有反转
            //IUserInfoDal infoDal = new EFUserInfoDal();

            //Spring.Net 创建实例的方式转为容器帮我们创建
            //第一步，引用Spring.Net程序集 Spring.Core.dll 和 Common.Logging.dll
            //第二步，添加Spring.Net配置节点
            //第三步，配置object节点
            //第四步，创建spring容器上下文
            IApplicationContext context = ContextRegistry.GetContext();
            //第五步，通过容器创建对象

           Mobile tool = context.GetObject("mobile") as Mobile;
           
            //Mobile mobile = context.GetObject("mobile") as Mobile;
            //mobile.GetSendTool().WeChat();

            //RealOp op = context.GetObject("realOp") as RealOp;
            //Console.WriteLine(op.Buy("面包"));

            //Door door = context.GetObject("door") as Door;
            //door.OnOpen("Opening");


            //IUserInfoDal adoDal = ctx.GetObject("UserInfoDal2") as IUserInfoDal;
            //adoDal.Show();
        }
    }



    public interface IUserInfoDal
    {
        void Show();
    }

    public class EFUserInfoDal : IUserInfoDal
    {
        public EFUserInfoDal()
        {
            Console.WriteLine("AKJDJKA");
        }
        public UserInfo UserInfo { get; set; }
        public string Name { get; set; }
        public IDictionary<int, string> PrintSetting { get; set; }

        public IList<int> list { get; set; }



        public void Show()
        {
            Console.WriteLine("我是EF Dal,属性注入：Name=" + Name);
            Console.WriteLine("UserInfo ,Name=" + UserInfo.Name + " Age=" + UserInfo.Age);
            foreach (var item in PrintSetting)
            {
                Console.WriteLine(item);
            }
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }



    public class UserInfo
    {
        public string Name { get; set; }

        public int Age { set; get; }
    }


    public class AdoNetUserInfoDal : IUserInfoDal
    {
        public AdoNetUserInfoDal(string name, UserInfo userInfo)
        {
            Name = name;
            UserInfo = userInfo;
        }

        public AdoNetUserInfoDal()
        {
            Console.WriteLine("ASADKNDK");

        }



        public UserInfo UserInfo { get; set; }
        public string Name { get; set; }

        public void Show()
        {
            Console.WriteLine("我是 AdoNet Dal ,属性注入：Name=" + Name);
            Console.WriteLine("UserInfo ,Name=" + UserInfo.Name + " Age=" + UserInfo.Age);
        }
    }


    public class SendTool
    {
        public void WeChat()
        {
            Console.WriteLine("我是Kimisme，正在用微信发信息");
        }
    }

    public class RealOp
    {
        public virtual string Buy(string goods)
        {
            return goods + "是昨天的剩下的";
        }
    }
    public class SuperMarket
    {
        public object Implement(object target, System.Reflection.MethodInfo method, object[] arguments)
        {
            string value = arguments[0].ToString();
            return value + "是今天的面包";
        }
    }

    public abstract class Mobile
    {
        //可以是virtual
        public abstract SendTool GetSendTool();
    }


    public delegate string OpenHandler(string arg);

    public class Door
    {
        public event OpenHandler OpenTheDoor;

        public void OnOpen(string arg)
        {

            if (OpenTheDoor != null)
            {
                Console.WriteLine(OpenTheDoor(arg));
            }
        }
    }

    #endregion




}



