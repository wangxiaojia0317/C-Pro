using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;//使用反射一定要加上这个！
using System.Configuration;

namespace IOC
{

    class Program
    {
        static void Main(string[] args)
        {
          
            Console.ReadLine();
        }
    }


    //#region 依赖注入

    //#region 构造器注入

    //public interface IUnderly
    //{
    //    void WriterLine();
    //}
    //public class Underly : IUnderly
    //{
    //    public void WriterLine()
    //    {
    //        Console.WriteLine("这只是一个底层类型的输出");
    //    }
    //}
    //#endregion

    //#region 属性注入
    //public class Top
    //{
    //    private IUnderly _Underly;
    //    public Top() { }
    //    public Top(IUnderly underly)
    //    {
    //        _Underly = underly;
    //    }

    //    public IUnderly Underly
    //    {
    //        get { return _Underly; }
    //        set { _Underly = value; }
    //    }

    //    public void Execution()
    //    {
    //        _Underly.WriterLine();
    //    }
    //}

    //#endregion

    //#endregion



    /*
        核心功能：生成依赖注入过程中的上层对象
        基础流程：
        1.需要向IoC容器中注册依赖注入过程中抽象、具体。
        2.在使用IoC的时候需向IoC中注册上层对象的类型。
        3.解析上层对象类型，并且执行生成对象操作
        4.返回上层对象实例
 
        功能对象定义：
        1.抽象、具体关系维护的对象，用以维护依赖注入过程中抽象、具体的对应关系。
        2.解析对象类型的对象，根据依赖注入的几种方式分析对象类型的构造和公共属性并且生成，（公共属性是符合IoC框架中定义的标准）。
        3.公共属性标准对象，用以通知IoC框架上层对象中哪些公共属性需要被注入。
        4.执行过程对象，用以表示框架执行流程，框架入口点。
     */


    //IoC框架入口点

    /// <summary>
    /// 对于IIoCKernel类型的定义，Bind和To两个方法用于绑定抽象、具体到关系维护的对象中，而GetValue()方法则是用以获取上层对象的实例，
    /// 对于这种入口点的使用方式我是模仿的Ninject框架，会在最后的示例中演示怎么使用。
    /// </summary>
    public interface IIocKernel
    {
        IIocKernel Bind<T>();

        IIocKernel To<U>() where U : class;

        V GetValue<V>() where V : class;
    }



    public class IocKernel : IIocKernel
    {
        public IocKernel()
        {

        }

        public IIocKernel Bind<T>()
        {
            throw new NotImplementedException();
        }

        public V GetValue<V>() where V : class
        {
            throw new NotImplementedException();
        }

        public IIocKernel To<U>() where U : class
        {
            throw new NotImplementedException();
        }
    }


}
