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
            Custom_Decorator c = new Custom_Decorator();
            c.Demo();
            Console.Read();
        }
    }

    
    
    #region 单例
    public class Singleton
    {
        private static Singleton instance;
        private static readonly object locker = new object();
        private Singleton()
        { }


        public static Singleton GetInstance()
        {
            if (instance==null)
            {
                lock (locker)
                {
                    if (instance==null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }

    }
    #endregion


    #region 简单工厂
    /// <summary>
    /// 需要创建的类
    /// 工厂是单一的
    /// </summary>
    public abstract class Food
    {
        public abstract void Print();
    }

    /// <summary>
    /// 西红柿炒鸡蛋这道菜
    /// </summary>
    public class TomatoScrambledEggs : Food
    {
        public override void Print()
        {
            Console.WriteLine("一份西红柿炒蛋！");
        }
    }

    /// <summary>
    /// 土豆肉丝这道菜
    /// </summary>
    public class ShreddedPorkWithPotatoes : Food
    {
        public override void Print()
        {
            Console.WriteLine("一份土豆肉丝");
        }
    }

    /// <summary>
    /// 工厂类
    /// </summary>
    public class FoodSimpleFactory
    {
        public static Food CreateFood(string type)
        {
            Food food = null;
            if (type.Equals("土豆肉丝"))
            {
                food = new ShreddedPorkWithPotatoes();
            }
            else if (type.Equals("西红柿炒蛋"))
            {
                food = new TomatoScrambledEggs();
            }

            return food;
        }

    }

    /// <summary>
    /// 测试
    /// </summary>
    class Customer_FoodSimpleFactory
    {
        void Demo()
        {
            Food food1 = FoodSimpleFactory.CreateFood("");
            food1.Print();
        }
    }





    #endregion


    #region 工厂方法模式
    /// <summary>
    /// 工厂分类
    /// 
    /// </summary>
    public abstract class Food_SmartFactoryMethod
    {
        public abstract void Print();
    }

    /// <summary>
    /// 西红柿炒鸡蛋这道菜
    /// </summary>
    public class TomatoScrambledEggs_SmartFactoryMethod : Food_SmartFactoryMethod
    {
        public override void Print()
        {
            Console.WriteLine("西红柿炒蛋好了！");
        }
    }

    /// <summary>
    /// 土豆肉丝这道菜
    /// </summary>
    public class ShreddedPorkWithPotatoes_SmartFactoryMethod : Food_SmartFactoryMethod
    {
        public override void Print()
        {
            Console.WriteLine("土豆肉丝好了");
        }
    }






    public abstract class Creator_SmartFactoryMethod
    {
        public abstract Food_SmartFactoryMethod CreatorFoodFactory();
    }

    /// <summary>
    /// 西红柿炒蛋工厂类
    /// </summary>
    public class TomatoScrambledEggsFactory_SmartFactoryMethod : Creator_SmartFactoryMethod
    {
        public override Food_SmartFactoryMethod CreatorFoodFactory()
        {
            return new TomatoScrambledEggs_SmartFactoryMethod();
        }
    }

    /// <summary>
    /// 土豆肉丝工厂类
    /// </summary>
    public class ShreddedPorkWithPotatoesFactory : Creator_SmartFactoryMethod
    {
        public override Food_SmartFactoryMethod CreatorFoodFactory()
        {
            return new ShreddedPorkWithPotatoes_SmartFactoryMethod();
        }
    }

    class Customer__SmartFactoryMethod
    {
        void Demo()
        {
            Creator_SmartFactoryMethod shreddedPorkWithPotatoesFactory = new ShreddedPorkWithPotatoesFactory();
            Food_SmartFactoryMethod shareFood = shreddedPorkWithPotatoesFactory.CreatorFoodFactory();
            shareFood.Print();
        }
      
    }


    #endregion


    #region 抽象工场
    /// <summary>
    /// 工厂分类，功能分类
    /// </summary>
    public abstract class AbstarctFactory
    {
        public abstract YaBo CreateYaBo();
        public abstract YaJia CreateYaJia();
    }

    public class NanChangFactory : AbstarctFactory
    {
        public override YaBo CreateYaBo()
        {
            return new NanJingYaBo();
        }

        public override YaJia CreateYaJia()
        {
            return new NanJingYaJia();
        }
    }


    public class ShangHaiFactory : AbstarctFactory
    {
        public override YaBo CreateYaBo()
        {
            return new ShangHaiJingYaBo();
        }

        public override YaJia CreateYaJia()
        {
            return new ShangHaiJingYaJia();
        }
    }


    public abstract class YaBo
    {
        public abstract void Print();
    }
    public abstract class YaJia
    {
        public abstract void Print();
    }

    public class NanJingYaBo : YaBo
    {
        public override void Print()
        {
            Console.WriteLine("这是南京鸭脖");
        }
    }

    public class ShangHaiJingYaBo : YaBo
    {
        public override void Print()
        {
            Console.WriteLine("这是上海鸭脖");
        }
    }

    public class NanJingYaJia : YaJia
    {
        public override void Print()
        {
            Console.WriteLine("这是南京鸭架");
        }
    }

    public class ShangHaiJingYaJia : YaJia
    {
        public override void Print()
        {
            Console.WriteLine("这是上海鸭架");
        }
    }



    class Customer_AbstractFactory
    {
        void Demo()
        {
            AbstarctFactory nanJingFactory = new NanChangFactory();
            YaBo nanJingYaBo =  nanJingFactory.CreateYaBo();
            nanJingYaBo.Print();
            YaJia nanJingYaJia = nanJingFactory.CreateYaJia();
            nanJingYaJia.Print();
        }
    }

    #endregion

    #region 建造者模式
    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartCPU();
            builder.BuildPartMainBoard();
        }
    }

    public abstract class Builder
     {
         // 装CPU
         public abstract void BuildPartCPU();
         // 装主板
         public abstract void BuildPartMainBoard();
         
         // 当然还有装硬盘，电源等组件，这里省略
 
         // 获得组装好的电脑
         public abstract Computer GetComputer();
     }



    public class Computer
    {
        public List<string> paras = new List<string>();
        public void Add(string part)
        {
            paras.Add(part);
        }


        public void Show()
        {
            foreach (string part in paras)
            {
                Console.WriteLine("组件" + part + "已装好");
            }
            Console.WriteLine("电脑组装好了");
        }


       

    }
    public class ConcreteBuilder1 : Builder
    {
        Computer computer = new Computer();
        public override void BuildPartCPU()
        {
            computer.Add("CPU1");
        }

        public override void BuildPartMainBoard()
        {
            computer.Add("MainBoard1");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }

    public class ConcreteBuilder2 : Builder
    {
        Computer computer = new Computer();
        public override void BuildPartCPU()
        {
            computer.Add("CPU2");
        }

        public override void BuildPartMainBoard()
        {
            computer.Add("MainBoard2");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }
    public class Custom_Constructor
    {
        void Demo()
        {
            Director director = new Director();
            Builder b1 = new ConcreteBuilder1();
            Builder b2 = new ConcreteBuilder2();
           
             // 老板叫员工去组装第一台电脑
             director.Construct(b1);
         
             // 组装完，组装人员搬来组装好的电脑
             Computer computer1 = b1.GetComputer();
             computer1.Show();
        
             // 老板叫员工去组装第二台电脑
             director.Construct(b2);
             Computer computer2 = b2.GetComputer();
             computer2.Show();
           
        }
    }

    #endregion

    #region 适配器模式
    public interface IThreeHole
    {
        void Request();
    }

    public abstract class TwoHole
    {
        public void SpecificRequest()
        {
            Console.WriteLine("我是两个头的插孔");
        }
    }


    public class PowerAdapter : TwoHole, IThreeHole
    {
        public void Request()
        {
            this.SpecificRequest();
        }
    }

    #endregion


    #region 桥接模式
    public class RemoteControl
    {
        private TV implementor;

        public TV Implementor
        {
            get
            {
                return implementor;
            }

            set
            {
                implementor = value;
            }
        }


        public virtual void On()
        {
            implementor.On();
        }

        public virtual void Off()
        {
            implementor.Off();
        }

        public virtual void SetChannel()
        {
            implementor.TurnChannel();
        }
    }

    public abstract class TV
    {
        public abstract void On();
        public abstract void Off();
        public abstract void TurnChannel();

    }
    public class ConcreteRemote : RemoteControl
    {
        public override void SetChannel()
        {
            base.SetChannel();
        }
    }

    public class ChangHong : TV
    {
        public override void Off()
        {
            Console.WriteLine("长虹电视关闭了");
        }

        public override void On()
        {
            Console.WriteLine("长虹电视打开了");
        }

        public override void TurnChannel()
        {
            Console.WriteLine("长虹电视换频道了");
        }
    }
    public class Samsung : TV
    {
        public override void Off()
        {
            Console.WriteLine("三星电视关闭了");
        }

        public override void On()
        {
            Console.WriteLine("三星电视打开了");
        }

        public override void TurnChannel()
        {
            Console.WriteLine("三星电视换频道了");
        }
    }


    public class Custom_Remote
    {
        public void Demo()
        {
            RemoteControl r = new RemoteControl();
            r.Implementor = new ChangHong();
            r.On();
            r.SetChannel();
            r.Off();
            // 三星牌电视机
            r.Implementor = new Samsung();
            r.On();
            r.SetChannel();
            r.Off();
        }
    }


    #endregion

    #region 装饰者模式
    public abstract class Phone
    {
        public abstract void Print();
    }

    public class ApplePhone : Phone
    {
        public override void Print()
        {
            Console.WriteLine("开始执行具体的对象——苹果手机");
        }
    }

    public abstract class Decorator : Phone
    {
        private Phone phone;
        public Decorator(Phone p)
        {
            this.phone = p;
        }

        public override void Print()
        {
            if (phone!=null)
            {
                phone.Print();
            }
        }
    }

    public class Sticker : Decorator
    {
        public Sticker(Phone p) : base(p)
        {

        }

        public override void Print()
        {
            base.Print();
            Addsticker();

        }
        public void Addsticker()
        {
            Console.WriteLine("现在苹果手机贴膜了");
        }
    }

    public class Accessroies : Decorator
    {
        public Accessroies(Phone p) : base(p)
        {

        }

        public override void Print()
        {
            base.Print();
            AddAccessories();
        }

        public void AddAccessories()
        {
            Console.WriteLine("现在苹果手机有漂亮的挂件了");
        }


    }


    public class Custom_Decorator
    {
        public void Demo()
        {
            // 我买了个苹果手机
            Phone phone = new ApplePhone();

            //// 现在想贴膜了
            //Decorator applePhoneWithSticker = new Sticker(phone);
            //// 扩展贴膜行为
            //applePhoneWithSticker.Print();
            //Console.WriteLine("----------------------\n");

            //// 现在我想有挂件了
            //Decorator applePhoneWithAccessories = new Accessroies(phone);
            //// 扩展手机挂件行为
            //applePhoneWithAccessories.Print();
            //Console.WriteLine("----------------------\n");

            // 现在我同时有贴膜和手机挂件了
            Sticker  sticker = new Sticker(phone);
            Accessroies applePhoneWithAccessoriesAndSticker = new Accessroies(sticker);
            applePhoneWithAccessoriesAndSticker.Print();
         
        }
    }


    #endregion

    #region 组合模式
    public abstract class Graphics
    {
        public string Name { get; set; }

        public Graphics(string name)
        {
            this.Name = name;
        }
        public abstract void Draw();

        public abstract void Add(Graphics g);

        public abstract void Remove(Graphics g);
    }


    public class Line : Graphics
    {
        public Line(string name) : base(name)
        {
        }

        // 重写父类抽象方法
        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }
        // 因为简单图形在添加或移除其他图形，所以简单图形Add或Remove方法没有任何意义
        // 如果客户端调用了简单图形的Add或Remove方法将会在运行时抛出异常
        // 我们可以在客户端捕获该类移除并处理
        public override void Add(Graphics g)
        {
            throw new Exception("不能向简单图形Line添加其他图形");
        }
        public override void Remove(Graphics g)
        {
            throw new Exception("不能向简单图形Line移除其他图形");
        }
    }

    public class Circle : Graphics
    {
        public Circle(string name)
            : base(name)
        { }

        // 重写父类抽象方法
        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }

        public override void Add(Graphics g)
        {
            throw new Exception("不能向简单图形Circle添加其他图形");
        }
        public override void Remove(Graphics g)
        {
            throw new Exception("不能向简单图形Circle移除其他图形");
        }
    }


    /// <summary>
    /// 复杂图形，由一些简单图形组成,这里假设该复杂图形由一个圆两条线组成的复杂图形
    /// </summary>
    public class ComplexGraphics : Graphics
    {
        private List<Graphics> complexGraphicsList = new List<Graphics>();

        public ComplexGraphics(string name)
            : base(name)
        { }

        /// <summary>
        /// 复杂图形的画法
        /// </summary>
        public override void Draw()
        {
            foreach (Graphics g in complexGraphicsList)
            {
                g.Draw();
            }
        }

        public override void Add(Graphics g)
        {
            complexGraphicsList.Add(g);
        }
        public override void Remove(Graphics g)
        {
            complexGraphicsList.Remove(g);
        }
    }




    #endregion

    #region 外观模式
    public class FacadePattern
    {
        public void Demo()
        {
        }
    }

    public class RegistrationFacade
    {
        private RegisterCourse registerCourse;
        private NotifyStudent notifyStu;
        public RegistrationFacade()
        {
            registerCourse = new RegisterCourse();
            notifyStu = new NotifyStudent();
        }

        public bool RegisterCourse(string courseName, string studentName)
        {
            if (!registerCourse.CheckAvailable(courseName))
            {
                return false;
            }

            return notifyStu.Notify(studentName);
        }
    }

    //相当于子系统
    public class RegisterCourse
    {
        public bool CheckAvailable(string courseName)
        {
            Console.WriteLine("正在验证课程{0}是否人数已满",courseName);
            return true;
        }
    }
    public class NotifyStudent
    {
        public bool Notify(string studentName)
        {
            Console.WriteLine("正在向{0}发生通知", studentName);
            return true;
        }
    }





    #endregion

    #region 享元模式
    //类似字典查找，对象复用
    #endregion

    #region 代理模式
    public abstract class Person
    {
        public abstract void BuyProduct();
    }

    public class RealBuyPerson : Person
    {
        public override void BuyProduct()
        {
            Console.WriteLine("帮我买一个IPhone和一台苹果电脑");
        }
    }

    public class Friend : Person
    {
        RealBuyPerson realSubject;
        public override void BuyProduct()
        {
            Console.WriteLine("通过代理类访问真是实体对象的方法");
            if (realSubject==null)
            {
                realSubject = new RealBuyPerson();
            }
            PreBuyProduct();
            PostBuyProduct();
        }
        public void PreBuyProduct()
        {
            Console.WriteLine("我怕弄糊涂了，需要列一张清单，张三：要带相机，李四：要带Iphone...........");
        }
        // 买完东西之后，代理角色需要针对每位朋友需要的对买来的东西进行分类
        public void PostBuyProduct()
        {
            Console.WriteLine("终于买完了，现在要对东西分一下，相机是张三的；Iphone是李四的..........");
        }
    }


    #endregion

    #region 模板方法模式
    public abstract class Vegetbale
    {
        public void CookVegetable()
        {
            Console.WriteLine("抄蔬菜的一般做法");
            this.PourOil();
            this.HeatOil();
            this.PourGegetable();
            this.stir_fry();
        }

        public void PourOil()
        {
            Console.WriteLine("倒油");
        }

        public void HeatOil()
        {
            Console.WriteLine("把油烧热");
        }

        public abstract void PourGegetable();

        public void stir_fry()
        {
            Console.WriteLine("翻炒");
        }
    }
    #endregion


    #region 命令模式


    /// <summary>
    /// 负责调用命令对象
    /// </summary>
    public class Invoke
    {
        public Command _command;

        public Invoke(Command c)
        {
            this._command = c;
        }

        public void ExecuteCommand()
        {
            _command.Action();
        }
    }


    /// <summary>
    /// 命令抽象类
    /// </summary>
    public abstract class Command
    {
        protected Receiver _receiver;
        public Command(Receiver r)
        {
            this._receiver = r;
        }

        public abstract void Action();
    }


    /// <summary>
    /// 
    /// </summary>
    public class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiver) : base(receiver)
        {

        }

        public override void Action()
        {
            _receiver.Run();
        }
    }

    /// <summary>
    /// 命令接收者
    /// </summary>
    public class Receiver
    {
        public void Run()
        {
            Console.WriteLine("跑一千米");
        }
    }


    /// <summary>
    /// 命令发布者
    /// </summary>
    public class Custom_Command
    {
        public void Demo()
        {
            Receiver r = new Receiver();
            Command c = new ConcreteCommand(r);
            Invoke i = new Invoke(c);
            i.ExecuteCommand();
        }
    }


    #endregion

    #region 迭代器模式
    //抽象聚合类
    public interface IListCollection
    {
        Iterator GetIterator();
    }


    //迭代器抽象类
    public interface Iterator
    {
        bool MoveNext();
        Object GetCurrent();
        void Next();
        void Reset();
    }

    //具体的聚合类
    public class ConcreteList : IListCollection
    {
        int[] collection;
        public ConcreteList()
        {
            collection = new int[] { 2,4,6,8};
        }
        public Iterator GetIterator()
        {
            return new ConcreteIterator(this);
        }
    }
    //具体的迭代器类
    public class ConcreteIterator : Iterator
    {
        private ConcreteList _list;
         private int _index;
 
         public ConcreteIterator(ConcreteList list)
         {
             _list = list;
             _index = 0;
         }
    public object GetCurrent()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
    #endregion


}
