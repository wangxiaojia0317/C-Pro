using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 装饰者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            Phone applePhone = new ApplePhone();

            Decorator appleWithSticker = new Sticker(applePhone);

            appleWithSticker.Print();

            Decorator applewithAcc = new Accessories(applePhone);
            applewithAcc.Print();
            Console.Read();

        }
    }


    /// <summary>
    /// 需要对咖啡进行装饰
    /// </summary>
    public abstract class Phone
    {
        public abstract void Print();
    }

    public class ApplePhone : Phone
    {
        public override void Print()
        {
            Console.WriteLine("开始具体的执行对象—苹果手机");
        }
    }


    /// <summary>
    /// 装饰抽象类,要让装饰完全取代抽象组件，所以必须继承自Photo
    /// </summary>

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


    /// <summary>
    /// 贴膜，即具体装饰者
    /// </summary>

    public class Sticker : Decorator
    {
        public Sticker(Phone p) : base(p)
        {
        }

        public override void Print()
        {
            base.Print();
            AddSticker();
        }
        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddSticker()
        {
            Console.WriteLine("现在苹果手机有贴膜了");
        }

    }


    /// <summary>
    /// 手机挂件
    /// </summary>
    public class Accessories : Decorator
    {
        public Accessories(Phone p)
            : base(p)
        {
        }

        public override void Print()
        {
            base.Print();

            // 添加新的行为
            AddAccessories();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddAccessories()
        {
            Console.WriteLine("现在苹果手机有漂亮的挂件了");
        }
    }
}
