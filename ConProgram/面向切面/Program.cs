using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace 面向切面
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "";
            Console.WriteLine(new Calc().add(1, 0));
            Console.WriteLine(new Calc().add(2, 3));
            Console.WriteLine(new Calc().add(1, 1));
            Console.ReadKey(true);
        }
    }



    /// <summary>
    /// AOP方法处理类,实现了IMessageSink接口
    /// </summary>
    public sealed class MyAopHandler : IMessageSink
    {
        /// <summary>
        /// 下一个接收器
        /// </summary>
        public IMessageSink NextSink { get; private set; }
        public MyAopHandler(IMessageSink nextSink)
        {
            this.NextSink = nextSink;
        }

        /// <summary>
        /// 同步处理方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public IMessage SyncProcessMessage(IMessage msg)
        {
            //方法调用消息接口
            var call = msg as IMethodCallMessage;
            

       

            //只拦截指定方法,其它方法原样释放
            if (call == null || (Attribute.GetCustomAttribute(call.MethodBase, typeof(AOPMethodAttribute))) == null || call.MethodName != "add") return NextSink.SyncProcessMessage(msg);

            //判断第2个参数,如果是0,则强行返回100,不调用方法了
            if (((int)call.InArgs[1]) == 0) return new ReturnMessage(100, call.Args, call.ArgCount, call.LogicalCallContext, call);
            

            //获取接收器的处理方法
            var retMsg = NextSink.SyncProcessMessage(call);

            //判断返回值,如果是5,则强行改为500
            if (((int)(retMsg as IMethodReturnMessage).ReturnValue) == 5)

                return new ReturnMessage(500, call.Args, call.ArgCount, call.LogicalCallContext, call);

            if (((int)call.InArgs[1]) == 0) return new ReturnMessage(100, call.Args, call.ArgCount, call.LogicalCallContext, call);


            return retMsg;
        }

        /// <summary>
        /// 异步处理方法(暂不处理)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="replySink"></param>
        /// <returns></returns>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink) => null;
    }


    /// <summary>
    /// 贴在方法上的标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class AOPMethodAttribute : Attribute { }

    /// <summary>
    /// 贴在类上的标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class AOPAttribute : ContextAttribute, IContributeObjectSink
    {

        
        public AOPAttribute() : base("AOP") { }


        public AOPAttribute(int a,int b) : base("AOP") { }


        /// <summary>
        /// 实现消息接收器接口
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink next) => new MyAopHandler(next);
    }

    

    [AOP]
    public class Calc : ContextBoundObject
    {
        [AOPMethod]
        public int add(int a, int b)
        {
            return a + b;
        }



    }


}
