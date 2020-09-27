using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AttributeTest
{
    class Program
    {
        static void Main()
        {
            var a =new AopClass();
            a.Hello();

            var aop = new AopClassSub("梦在旅途");
            aop.Pro = "test";
            aop.Output("hlf");
            aop.ShowMsg();
            Console.ReadKey();
        }

    }


    #region 初识AOP

    /*
    C#实现AOP的几种方式
    AOP为Aspect Oriented Programming(面向切面编程)通过预编译方式和运行期动态代理实现程序功能中的统一业务逻辑的一种技术，比较常见的场景是日记记录，错误捕捉，性能监控等等
    */
    //class RealA
    //{
    //    public virtual string Pro { get; set; }

    //    public virtual void ShowHello(string name)
    //    {
    //        Console.WriteLine($"Hello!{name},Welcome");
    //    }

    //}

    //class ProxyRealA : RealA
    //{
    //    public override string Pro
    //    {
    //        get
    //        {
    //            return base.Pro;
    //        }

    //        set
    //        {
    //            ShowLog("设置Pro属性前的日志信息");
    //            base.Pro = value;
    //            ShowLog($"设置Pro属性后的日志信息{value}");

    //        }
    //    }

    //    public override void ShowHello(string name)
    //    {
    //        try
    //        {
    //            ShowLog("ShowHello执行前的日志信息");
    //            base.ShowHello(name);
    //            ShowLog("ShowHello执行后的日志信息");


    //        }
    //        catch (Exception ex)
    //        {

    //            ShowLog($"ShowHello执行出错日志信息：{ex.Message}");
    //        }
    //    }
    //    public void ShowLog(string log)
    //    {
    //        Console.WriteLine($"{DateTime.Now.ToString()}-{log}");
    //    }
    //}


    /*
    实现通用的AopProxy代理类，代理类必须继承自RealProxy类，在这个代理类里面重写Invoke方法，这是执行被代理的真实类的所有方法，属性，字段的出入口，我们只需要在方法中根据传入的IMessage进行判断并实现相应的拦截代码即可
    
        
        实现思路：
        1.在AOP中，
        
            
     */

    //ContextBoundObject
    // 摘要:
    //     定义所有上下文绑定类的基类。
    [AopAttribute]
    public class AopClass : ContextBoundObject
    {
        public string Hello()
        {
            return "welcome";
        }

    }


    public class AopClassSub : AopClass
    {
        public string Pro = null;
        private string Msg = null;

        public AopClassSub(string msg)
        {
            Msg = msg;
        }

        public void Output(string name)
        {
            Console.WriteLine(name + ",你好！-->P:" + Pro);
        }

        public void ShowMsg()
        {
            Console.WriteLine($"构造函数传的Msg参数内容是：{Msg}");
        }
    }


    // ProxyAttribute
    // 摘要:
    //     或者创建未初始化的 System.MarshalByRefObject，或者创建透明代理，具体取决于指定类型是否可以存在于当前上下文中。
    //
    // 参数:
    //   serverType:
    //     要创建其实例的对象类型。
    //

    /// <summary>
    /// 主要就是穿件代理类的实例
    /// </summary>
    public class AopAttribute : ProxyAttribute
    {
        //需要创建的实例类型
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            AopProxy realProxy = new AopProxy(serverType);
            return realProxy.GetTransparentProxy() as MarshalByRefObject;
        }
    }

    //RealProxy
    // 摘要:
    //     提供代理的基本功能。
    public class AopProxy : RealProxy
    {
        public AopProxy(Type serverType)
            : base(serverType)
        { }

        // 摘要:
        //     当在派生类中重写时，对当前实例所表示的远程对象调用在所提供的 System.Runtime.Remoting.Messaging.IMessage 中指定的方法。
        //
        // 参数:
        //   msg:
        //     System.Runtime.Remoting.Messaging.IMessage，包含有关方法调用的信息的 System.Collections.IDictionary。
        //
        // 返回结果:
        //     调用的方法所返回的消息，包含返回值和所有 out 或 ref 参数。
        public override IMessage Invoke(IMessage msg)
        {
            if (msg is IConstructionCallMessage)
            {
                //传递类型是构造函数
                IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
                IConstructionReturnMessage constructionReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
                RealProxy.SetStubData(this, constructionReturnMessage.ReturnValue);
                Console.WriteLine("Call constructor");
                return constructionReturnMessage;
            }
            else
            {
                
                //传递类型是方法
                IMethodCallMessage callMsg = msg as IMethodCallMessage;
                IMessage message;
                try
                {
                    Console.WriteLine(callMsg.MethodName + "执行前。。。");
                    object[] args = callMsg.Args;
                    object o = callMsg.MethodBase.Invoke(GetUnwrappedServer(), args);
                    Console.WriteLine(callMsg.MethodName + "执行后。。。");
                    message = new ReturnMessage(o, args, args.Length, callMsg.LogicalCallContext, callMsg);
                }
                catch (Exception e)
                {
                    message = new ReturnMessage(e, callMsg);
                }
                Console.WriteLine(message.Properties["__Return"]);

                return message;
            }
        }
    }

    #endregion


    //    第四种：反射+ 通过定义统一的出入口，并运用一些特性实现AOP的效果，比如：常见的MVC、WEB API中的过滤器 特性 ，我这里根据MVC的思路，实现了类似的MVC过滤器的AOP效果，只是中间用到了反射，可能性能不佳，但效果还是    成功实现了各种拦截，正如MVC一样，既支持过滤器特性，也支持Controller中的Action执行前，执行后，错误等方法   实现拦截

    // 实现思路如下：

    //A.过滤器及Controller特定方法拦截实现原理：

    //1.获取程序集中所有继承自Controller的类型； 

    //2.根据Controller的名称找到第1步中的对应的Controller的类型：FindControllerType

    //3.根据找到的Controller类型及Action的名称找到对应的方法：FindAction

    //4.创建Controller类型的实例；

    //5.根据Action方法找到定义在方法上的所有过滤器特性（包含：执行前、执行后、错误）

    //6.执行Controller中的OnActionExecuting方法，随后执行执行前的过滤器特性列表，如：    ActionExecutingFilter

    //7.执行Action方法，获得结果；

    //8.执行Controller中的OnActionExecuted方法，随后执行执行后的过滤器特性列表，如：ActionExecutedFilter

    //9.通过try catch在catch中执行Controller中的OnActionError方法，随后执行错误过滤器特性列表，如：    ActionErrorFilter

    //10.最后返回结果；

    //B.实现执行路由配置效果原理：

    //1.增加可设置路由模板列表方法：AddExecRouteTemplate，在方法中验证controller、action，并获取模板中的占 位符数组，最后保存到类全局对象中routeTemplates； 

    //2.增加根据执行路由执行对应的Controller中的Action方法的效果： Run，在该方法中主要遍历所有路由模板，然后    与实行执行的请求路由信息通过正则匹配，若匹配OK，并能正确找到Controller及Action，则说明正确，并最终统一调   用：Process方法，执行A中的所有步骤最终返回结果。

    //需要说明该模拟MVC方案并没有实现Action方法参数的的绑定功能，因为ModelBinding本身就是比较复杂的机制，所以    这里只是为了搞清楚AOP的实现原理，故不作这方面的研究，大家如果有空可以实现，最终实现MVC不仅是ASP.NET    MVC，还可以是 Console MVC,甚至是Winform MVC等。


    public abstract class Controller
    {
        public virtual void OnActionExecuting(MethodInfo action)
        {

        }

        public virtual void OnActionExecuted(MethodInfo action)
        {

        }

        public virtual void OnActionError(MethodInfo action,Exception ex)
        {

        }
    }

    /// <summary>
    /// 过滤器特性
    /// </summary>
    public abstract class FilterAttribute : Attribute
    {
        //过滤器类型
        public abstract string FilterType { get; }

        //
        public abstract void Execute(Controller ctrller,object extData);

    }

    public class ActionExecutingFilter : FilterAttribute
    {
        public override string FilterType => "BEFORE";
        

        public override void Execute(Controller ctrller, object extData)
        {
            Console.WriteLine($"我是在{ctrller.GetType().Name}.ActionExecutingFilter中拦截发出的消息--{DateTime.Now.ToString()}");
        }
    }

    public class ActionExecutedFilter : FilterAttribute
    {
        public override string FilterType => "AFTER";


        public override void Execute(Controller ctrller, object extData)
        {
            Console.WriteLine($"我是在{ctrller.GetType().Name}.ActionExecutedFilter中拦截发出的消息--{DateTime.Now.ToString()}");
        }
    }

    public class ActionErrorFilter : FilterAttribute
    {
        public override string FilterType => "EXCEPTION";


        public override void Execute(Controller ctrller, object extData)
        {
            Console.WriteLine($"我是在{ctrller.GetType().Name}.ActionErrorFilter中拦截发出的消息！-{DateTime.Now.ToString()}-Error Msg:{(extData as Exception).Message}");
        }
    }
}


