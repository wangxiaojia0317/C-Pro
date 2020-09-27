using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{

    class shareRes
    {
        public static int count = 0;
        public static Mutex mutex = new Mutex();
    }

    class IncThread
    {
        int number;
        public Thread thrd;
        public IncThread(string name, int n)
        {
            thrd = new Thread(this.run);
            number = n;
            thrd.Name = name;
            thrd.Start();
        }
        void run()
        {
            Console.WriteLine(thrd.Name + "正在等待 the mutex");
            //申请
            shareRes.mutex.WaitOne();
            Console.WriteLine(thrd.Name + "申请到 the mutex");
            do
            {
                Thread.Sleep(1000);
                shareRes.count++;
                Console.WriteLine("In " + thrd.Name + "ShareRes.count is " + shareRes.count);
                number--;
            } while (number > 0);
            Console.WriteLine(thrd.Name + "释放 the nmutex");
            //  释放
            shareRes.mutex.ReleaseMutex();
            Console.Read();
        }
    }
    //class DecThread
    //{
    //    int number;
    //    public Thread thrd;
    //    public DecThread(string name, int n)
    //    {
    //        thrd = new Thread(this.run);
    //        number = n;
    //        thrd.Name = name;
    //        thrd.Start();
    //    }
    //    void run()
    //    {
    //        Console.WriteLine(thrd.Name + "正在等待 the mutex");
    //        //申请
    //        shareRes.mutex.WaitOne();
    //        Console.WriteLine(thrd.Name + "申请到 the mutex");
    //        do
    //        {
    //            Thread.Sleep(1000);
    //            shareRes.count--;
    //            Console.WriteLine("In " + thrd.Name + "ShareRes.count is " + shareRes.count);
    //            number--;
    //        } while (number > 0);
    //        Console.WriteLine(thrd.Name + "释放 the nmutex");
    //        //  释放
    //        shareRes.mutex.ReleaseMutex();
    //    }
    //}

    class Program
    {
       static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
       static Thread t1;
       static Thread t2;
       static List<int> list = new List<int>();
       public static ManualResetEvent mre = new ManualResetEvent(false);
        static void Main(string[] args)
        {

            List<List<int>> l = new List<List<int>>();

            for (int i = 0; i < 5; i++)
            {
                List<int> ll = new List<int>();
                for (int j = 0; j < new Random().Next(0, 10); j++)
                {
                    ll.Add(new Random().Next(0, 10));
                }
                l.Add(ll);
            }


            List<List<int>> lll = new List<List<int>>();
            lll = l;
            l.RemoveAt(0);
            Console.Read();

            //t1 = new Thread(delegate (){
            //    try
            //    {
            //        rw.EnterReadLock();
            //        while (true)
            //        {
            //            Console.WriteLine(list.Count);
            //        }
            //    }
            //    finally
            //    {
            //        rw.ExitReadLock();
            //    }


            //});

            //t2 = new Thread(delegate () {
            //    try
            //    {
            //        rw.EnterWriteLock();
            //        while (true)
            //        {

            //            Console.WriteLine(list.Count);
            //        }
            //    }
            //    finally
            //    {
            //        rw.ExitWriteLock();
            //    }


            //});
        }

        static void OtherThreadA()
        {
            Thread currentThread = Thread.CurrentThread;
            Console.WriteLine("threadA: waiting for an event");
            mre.WaitOne();
            Console.WriteLine("threadA: got an event");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("ThreadA: " + i.ToString());
            }
        }

        static void OtherThreadB()
        {
            Thread currentThread = Thread.CurrentThread;
            Console.WriteLine("threadB: waiting for an event");
            mre.WaitOne();
            Console.WriteLine("threadB: got an event");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("ThreadB: " + i.ToString());
            }
        }

    }


    //class IntEventArgs : System.EventArgs
    //{
    //    public int IntValue { get; set; }
    //    public IntEventArgs() { }
    //    public IntEventArgs(int value)
    //    { this.IntValue = value; }
    //}

    //class StringEventArgs : System.EventArgs
    //{
    //    public string StringValue { get; set; }
    //    public StringEventArgs() { }
    //    public StringEventArgs(string value)
    //    { this.StringValue = value; }
    //}

    //class Program
    //{
    //    static void PrintInt(object sender, IntEventArgs e)
    //    {
    //        Console.WriteLine(e.IntValue);
    //    }

    //    static void PrintString(object sender, StringEventArgs e)
    //    {
    //        Console.WriteLine(e.StringValue);
    //    }



    //    static void Main(string[] args)
    //    {
    //        //Dictionary<int, char> dic = new Dictionary<int, char>();
    //        //for (int i = 0; i < 10; i++)
    //        //{
    //        //    dic.Add(i,char.Parse(i.ToString()));
    //        //    Console.WriteLine(i.ToString()+":"+char.Parse(i.ToString()));
    //        //}
    //        //Console.WriteLine("-------------------------------");
    //        //foreach (var item in dic)
    //        //{
    //        //    Console.WriteLine(item);
    //        //}


    //        Test();


    //        Console.Read();



    //    }


    //    static void Test()
    //    {
    //        EventHandler<IntEventArgs> ihandler =
    //           new EventHandler<IntEventArgs>(PrintInt);
    //        EventHandler<StringEventArgs> shandler =
    //            new EventHandler<StringEventArgs>(PrintString);

    //        ihandler(null, new IntEventArgs(100));
    //        shandler(null, new StringEventArgs("Hello World"));
    //    }
    //}

    #region 信号量
    public class Sam
    {
        static Semaphore sema = new Semaphore(1, 5);
        const int cycleNum = 9;

        public static void testFun(object obj)
        {
            sema.WaitOne();
            Console.WriteLine(obj.ToString() + "进洗手间：" + DateTime.Now.ToString());
            Thread.Sleep(2000);
            Console.WriteLine(obj.ToString() + "出洗手间：" + DateTime.Now.ToString());
            sema.Release();
        }

        public void Show
            ()
        {


        }

    }
    #endregion
}
