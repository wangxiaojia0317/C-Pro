using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignMode
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.Read();
        }
    }


    #region 23种设计模式
    /*
     23种设计模式，简单demo实现，只作为理解的基础，后期会写入框架中
     本次以加强理解为基础，一加强理解为主，该有的抽象还是要有的，会作为common组件运用到实例中，一定要熟悉其中的原里并且会运用
     */

    #region 单例模式
    public class SingleTon<T> where T : class
    {
        private static T instance;
        private static object locker = new object();
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            Type t = typeof(T);
                            //确保不会被new到
                            System.Reflection.ConstructorInfo[] ctors = t.GetConstructors();
                            if (ctors.Length > 0)
                            {
                                throw new InvalidOperationException(String.Format("{0} has at least one accesible ctor making it impossibleto enforce DyhSingleton behaviour", t.Name));
                            }
                            instance = (T)Activator.CreateInstance(t, true);
                        }
                    }
                }
                return instance;
            }
        }

    }

    public class A : SingleTon<A>
    {
        public void Read()
        {
            Console.WriteLine("KSJFHK");
        }
        //添加私有构造，防止被实例化
        private A()
        {

        }
    }
    #endregion

    #region 抽像工厂模式

    /*
    所谓的抽象工厂其实就是抽象出来一个工厂类，这个抽象的工厂只是提供抽象的产品作为一个产品的产品族 
         
         
    */
    public abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }

    public interface IAbstractProduct
    {

    }
    public abstract class AbstractProductA : IAbstractProduct
    {
        public abstract void Interact(IAbstractProduct product);
    }

    public abstract class AbstractProductB : IAbstractProduct
    {
        public abstract void Interact(IAbstractProduct product);
    }



    public class ProductA : AbstractProductA
    {
        public override void Interact(IAbstractProduct product)
        {
            Console.WriteLine("具体的产品A逻辑");
        }
    }

    public class ProductB : AbstractProductB
    {
        public override void Interact(IAbstractProduct product)
        {
            Console.WriteLine("具体的产品B逻辑");
        }
    }

    //不同的工厂生产的产品的类型是一样的，但是规格是不一样的，这也就是差异化
    public class FactoryA : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA();
        }

        public override AbstractProductB CreateProductB()
        {
            return new ProductB();
        }
    }

    public class FactoryB : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA();
        }

        public override AbstractProductB CreateProductB()
        {
            return new ProductB();
        }
    }




    #endregion

    #region 工厂方法模式
    /*
     不需要知道它的具体的实现产品，但是必须知道谁生产的，并且可以创建的对象
     由 中间者去执行相关的操作
     */

    public abstract class CarFactory
    {
        public abstract Car CreateCar();
    }

    public abstract class Car
    {
        public abstract void StartUp();

        public abstract void Run();

        public abstract void Stop();
    }


    public class HongQiFactory : CarFactory
    {
        public override Car CreateCar()
        {
            return new HongQiCar();
        }
    }

    public class HongQiCar : Car
    {
        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void StartUp()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region 建造者模式
    /*
     实际上就是系统并不是一成不变的，但是也要尽量避免改变导致大的系统的变动，需要的变动只需要在子系统中去变动即可
     */

    //可以用在资源加载等有顺序要求的地方
    public abstract class Builder
    {
        public abstract void BuildDoor();

        public abstract void BuildWall();

        public abstract void BuildWindows();

        public abstract void BuildFollr();

    }

    public class Director
    {
        //按照顺序执行，一定是有严格的顺序
        public Director(Builder builder)
        {
            builder.BuildDoor();
            builder.BuildFollr();
            builder.BuildWall();
            builder.BuildWindows();
        }
    }

    public class ChineseBuilder : Builder
    {
        public override void BuildDoor()
        {
            throw new NotImplementedException();
        }

        public override void BuildFollr()
        {
            throw new NotImplementedException();
        }

        public override void BuildWall()
        {
            throw new NotImplementedException();
        }

        public override void BuildWindows()
        {
            throw new NotImplementedException();
        }
    }

    public class DemoBuilder
    {
        public void DemoTest()
        {
            ChineseBuilder cBuilder = new ChineseBuilder();
            Director d = new Director(cBuilder);

        }

    }

    #endregion

    #region 原型模式
    /*
     将模板保存，然后复制成新的对象
     创建抽象类，使用其拷贝
     */

    public abstract class NormalActor
    {
        public abstract NormalActor Clone();
    }

    public class NormalActorA : NormalActor
    {
        public override NormalActor Clone()
        {
            return (NormalActor)this.MemberwiseClone();
        }
    }


    #endregion

    #region 适配器模式
    /*
     使用现有的类进行复用，
     */

    public interface IMediaPlayer
    {
        void Play();
    }

    public interface IAdvanceMediaPlayer
    {
        void PlayVcr();
        void PlayMp4();
    }

    public class VcrPlayer : IAdvanceMediaPlayer
    {
        public void PlayMp4()
        {
            throw new NotImplementedException();
        }

        public void PlayVcr()
        {
            throw new NotImplementedException();
        }
    }

    public class Mp4Player : IAdvanceMediaPlayer
    {
        public void PlayMp4()
        {
            throw new NotImplementedException();
        }

        public void PlayVcr()
        {
            throw new NotImplementedException();
        }
    }


    public class MediaAdapter : IMediaPlayer
    {
        IAdvanceMediaPlayer advanceMediaPlayer;
        int f = 0;
        public MediaAdapter(int flag)
        {
            f = flag;
            switch (flag)
            {
                case 0:
                    advanceMediaPlayer = new VcrPlayer();
                    break;
                case 1:
                    advanceMediaPlayer = new Mp4Player();
                    break;
            }

        }

        public void Play()
        {
            switch (f)
            {
                case 0:
                    advanceMediaPlayer.PlayVcr();
                    break;
                case 1:
                    advanceMediaPlayer.PlayMp4();
                    break;
            }

        }
    }


    public class AudioPlayer : IMediaPlayer
    {
        MediaAdapter mediaAdapter;
        int a = 0;
        public void Play()
        {
            if (a == 0)
            {
                Console.WriteLine("skfhdsklfhk");
            }
            else if (a == 1)
            {
                mediaAdapter = new MediaAdapter(0);
                mediaAdapter.Play();
            }
            else
            {

            }
        }
    }

    #endregion

    #region 桥接模式
    /*
     所谓的桥接模式其实就是不同的对象之间的排列组合，可以参考抽象工厂模式生产的产品与其他的场子生产的产品都被放在同一个商场里面
     
     */

    interface IDrawingAPI
    {
        void DrawCircle(double x, double y, double radius);
    }  /** "ConcreteImplementor" 1/2 */
    class DrawingAPI1 : IDrawingAPI
    {
        public void DrawCircle(double x, double y, double radius)
        {
            System.Console.WriteLine("API1.circle at {0}:{1} radius {2}", x, y, radius);
        }
    }  /** "ConcreteImplementor" 2/2 */
    class DrawingAPI2 : IDrawingAPI
    {
        public void DrawCircle(double x, double y, double radius)
        {
            System.Console.WriteLine("API2.circle at {0}:{1} radius {2}", x, y, radius);
        }
    }  /** "Abstraction" */
    interface IShape
    {
        void Draw();                             // low-level (i.e. Implementation-specific) 
        void ResizeByPercentage(double pct);     // high-level (i.e. Abstraction-specific) 
    }  /** "Refined Abstraction" */
    class CircleShape : IShape
    {
        private double x, y, radius;
        private IDrawingAPI drawingAPI;
        public CircleShape(double x, double y, double radius, IDrawingAPI drawingAPI)
        {
            this.x = x; this.y = y; this.radius = radius;
            this.drawingAPI = drawingAPI;
        }    // low-level (i.e. Implementation-specific)   
        public void Draw()
        {
            drawingAPI.DrawCircle(x, y, radius);
        }    // high-level (i.e. Abstraction-specific)  
        public void ResizeByPercentage(double pct)
        {
            radius *= pct;
        }
    }

    #endregion

    #region 装饰者模式

    public abstract class Tank
    {
        public abstract void Shot();
        public abstract void Run();
    }

    public abstract class Decorator : Tank
    {
        protected Tank tank;
        public Decorator(Tank t)
        {
            this.tank = t;
        }

        public override void Shot()
        {
            tank.Shot();
        }

        public override void Run()
        {
            tank.Run();
        }
    }

    public class DecoratorA : Decorator
    {


        public override void Shot()
        {
            //扩展的功能
            tank.Shot();
        }

        public override void Run()
        {
            //扩展的功能
            tank.Run();
        }

        public DecoratorA(Tank t) : base(t)
        {
        }
    }






    #endregion

    #region 组合模式

    #endregion

    #region 享元模式


    public abstract class Charactor
    {
        //Fields
        protected char _symbol;

        protected int _width;

        protected int _height;

        protected int _ascent;

        protected int _descent;

        protected int _pointSize;

        public abstract void SetPointSize(int size);
        public abstract void Display();
    }



    public class CharactorA : Charactor
    {
        public CharactorA()
        {
            this._symbol = 'A';
            this._height = 100;
            this._width = 120;
            this._ascent = 70;
            this._descent = 0;
        }
        public override void SetPointSize(int size)
        {
            this._pointSize = size;
        }

        public override void Display()
        {
            Console.WriteLine();
        }
    }
    public class CharactorB : Charactor
    {
        public CharactorB()
        {
            this._symbol = 'B';
            this._height = 100;
            this._width = 120;
            this._ascent = 70;
            this._descent = 0;
        }
        public override void SetPointSize(int size)
        {
            this._pointSize = size;
        }

        public override void Display()
        {
            Console.WriteLine();
        }
    }

    public class CharactorC : Charactor
    {
        public CharactorC()
        {
            this._symbol = 'C';
            this._height = 100;
            this._width = 120;
            this._ascent = 70;
            this._descent = 0;
        }
        public override void SetPointSize(int size)
        {
            this._pointSize = size;
        }

        public override void Display()
        {
            Console.WriteLine();
        }

    }

    public class CharactorFactory : SingleTon<CharactorFactory>
    {
        private Hashtable charactors = new Hashtable();

        private CharactorFactory()
        {
            charactors.Add("A", new CharactorA());
            charactors.Add("B", new CharactorB());
            charactors.Add("C", new CharactorC());
        }


        public Charactor GetCharactor(string key)
        {
            Charactor c = charactors[key] as Charactor;
            if (c == null)
            {
                switch (key)
                {
                    case "A":
                        c = new CharactorA(); break;

                    case "B":
                        c = new CharactorB(); break;

                    case "C": c = new CharactorC(); break;
                }
                charactors.Add(key, c);
            }
            return c;
        }



    }


    #endregion









    #endregion



}
