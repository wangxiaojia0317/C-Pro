using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace Linq
{
    class Program
    {
       
        static void Main(string[] args)
        {

            //List<StudentScore> lst = new List<StudentScore>() {
            //    new StudentScore(){ID=1,Name="张三",Term="第一学期",Course="Math",Score=80},
            //    new StudentScore(){ID=1,Name="张三",Term="第一学期",Course="Chinese",Score=90},
            //    new StudentScore(){ID=1,Name="张三",Term="第一学期",Course="English",Score=70},
            //    new StudentScore(){ID=2,Name="李四",Term="第一学期",Course="Math",Score=60},
            //    new StudentScore(){ID=2,Name="李四",Term="第一学期",Course="Chinese",Score=70},
            //    new StudentScore(){ID=2,Name="李四",Term="第一学期",Course="English",Score=30},
            //    new StudentScore(){ID=3,Name="王五",Term="第一学期",Course="Math",Score=100},
            //    new StudentScore(){ID=3,Name="王五",Term="第一学期",Course="Chinese",Score=80},
            //    new StudentScore(){ID=3,Name="王五",Term="第一学期",Course="English",Score=80},
            //    new StudentScore(){ID=4,Name="赵六",Term="第一学期",Course="Math",Score=90},
            //    new StudentScore(){ID=4,Name="赵六",Term="第一学期",Course="Chinese",Score=80},
            //    new StudentScore(){ID=4,Name="赵六",Term="第一学期",Course="English",Score=70},
            //    new StudentScore(){ID=1,Name="张三",Term="第二学期",Course="Math",Score=100},
            //    new StudentScore(){ID=1,Name="张三",Term="第二学期",Course="Chinese",Score=80},
            //    new StudentScore(){ID=1,Name="张三",Term="第二学期",Course="English",Score=70},
            //    new StudentScore(){ID=2,Name="李四",Term="第二学期",Course="Math",Score=90},
            //    new StudentScore(){ID=2,Name="李四",Term="第二学期",Course="Chinese",Score=50},
            //    new StudentScore(){ID=2,Name="李四",Term="第二学期",Course="English",Score=80},
            //    new StudentScore(){ID=3,Name="王五",Term="第二学期",Course="Math",Score=90},
            //    new StudentScore(){ID=3,Name="王五",Term="第二学期",Course="Chinese",Score=70},
            //    new StudentScore(){ID=3,Name="王五",Term="第二学期",Course="English",Score=80},
            //    new StudentScore(){ID=4,Name="赵六",Term="第二学期",Course="Math",Score=70},
            //    new StudentScore(){ID=4,Name="赵六",Term="第二学期",Course="Chinese",Score=60},
            //    new StudentScore(){ID=4,Name="赵六",Term="第二学期",Course="English",Score=70},
            //};

            ////分组，根据姓名，统计Sum的分数，统计结果放在匿名对象中。两种写法。 
            ////第一种写法 
            //Console.WriteLine("---------第一种写法");
            //var studentSumScore_1 = (from l in lst
            //                         group l by l.Name into grouped
            //                         orderby grouped.Sum(m => m.Score)
            //                         select new { Name = grouped.Key, Scores = grouped.Sum(m => m.Score) }).ToList();
            //foreach (var l in studentSumScore_1)
            //{
            //    Console.WriteLine("{0}:总分{1}", l.Name, l.Scores);
            //}
            ////第二种写法和第一种其实是等价的。 
            ////第二种写法 
            //Console.WriteLine("---------第二种写法");
            //var studentSumScore_2 = lst.GroupBy(m => m.Name)
            //    .Select(k => new { Name = k.Key, Scores = k.Sum(l => l.Score) })
            //    .OrderBy(m => m.Scores).ToList();
            //foreach (var l in studentSumScore_2)
            //{
            //    Console.WriteLine("{0}:总分{1}", l.Name, l.Scores);
            //}




            //List<string> resuList = new List<string>();
            //List<List<string>> zipList = new List<List<string>>{
            //    new List<string>{"a","b","c","d"},
            //    new List<string>{"1","2","3","4"},
            //    new List<string>{"A","B","C","D"},
            //    new List<string>{"一","二","三","四"},

            //};
            //for (int i = 0; i < zipList.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        resuList = zipList[i];
            //    }
            //    else
            //    {
            //        resuList = resuList.Zip(zipList[i], (first, second) => first + second).ToList();
            //    }
            //}
            //foreach (var item in resuList)
            //    Console.WriteLine(item);

            //int[] numbers = { 3, 5, 7 };
            //string[] words = { "three", "five", "seven", "ignored" };
            //IEnumerable<string> zip = numbers.Zip(words, (n, w) => n + "=" + w);
            //foreach (var z in zip)
            //{
            //    Console.WriteLine(z);
            //}

            double x = 6.50000000000;
            float y = float.Parse(x.ToString());
            Console.WriteLine(y.ToString("F2"));


            Console.WriteLine(5>>1);
            Console.Read();


           





            //        //分组，根据2个条件学期和课程，统计各科均分,统计结果放在匿名对象中。两种写法。 
            //        Console.WriteLine("---------第一种写法"); 
            //var TermAvgScore_1 = (from l in lst
            //                      group l by new { Term = l.Term, Course = l.Course } into grouped
            //                      orderby grouped.Average(m => m.Score) ascending
            //                      orderby grouped.Key.Term descending
            //                      select new { Term = grouped.Key.Term, Course = grouped.Key.Course, Scores = grouped.Average(m => m.Score) }).ToList(); 
            //foreach (var l in TermAvgScore_1) 
            //{ 
            //    Console.WriteLine("学期:{0},课程{1},均分{2}", l.Term, l.Course, l.Scores); 
            //}
            //    Console.WriteLine("---------第二种写法"); 
            //var TermAvgScore_2 = lst.GroupBy(m => new { Term = m.Term, Course = m.Course })
            //    .Select(k => new { Term = k.Key.Term, Course = k.Key.Course, Scores = k.Average(m => m.Score) })
            //    .OrderBy(l => l.Scores).OrderByDescending(l => l.Term); 
            //foreach (var l in TermAvgScore_2) 
            //{ 
            //    Console.WriteLine("学期:{0},课程{1},均分{2}", l.Term, l.Course, l.Scores); 
            //}












            //            //分组，带有Having的查询，查询均分>80的学生 
            //            Console.WriteLine("---------第一种写法");
            //            var AvgScoreGreater80_1 = (from l in lst
            //                                       group l by new { Name = l.Name, Term = l.Term } into grouped
            //                                       where grouped.Average(m => m.Score) >= 80
            //                                       orderby grouped.Average(m => m.Score) descending
            //                                       select new { Name = grouped.Key.Name, Term = grouped.Key.Term, Scores = grouped.Average(m => m.Score) }).ToList();
            //            foreach (var l in AvgScoreGreater80_1)
            //            {
            //                Console.WriteLine("姓名:{0},学期{1},均分{2}", l.Name, l.Term, l.Scores);
            //            }
            //            Console.WriteLine("---------第二种写法");
            //            //此写法看起来较为复杂，第一个Groupby，由于是要对多个字段分组的，www.it165.net 因此构建一个匿名对象，
            //            对这个匿名对象分组，分组得到的其实是一个IEnumberable < IGrouping < 匿名类型，StudentScore >> 这样一个类型。
            //Where方法接受，和返回的都同样是IEnumberable < IGrouping < 匿名类型，StudentScore >> 类型，
            //其中Where方法签名Func委托的类型也就成了Func < IGrouping < 匿名类型，StudentScore >,bool>,
            //之前说到，IGrouping <out TKey, out TElement > 继承了IEnumerable<TElement>,
            //因此这种类型可以有Average，Sum等方法。 
            //var AvgScoreGreater80_2 = lst.GroupBy(l => new { Name = l.Name, Term = l.Term })
            //    .Where(m => m.Average(x => x.Score) >= 80)
            //    .OrderByDescending(l => l.Average(x => x.Score))
            //    .Select(l => new { Name = l.Key.Name, Term = l.Key.Term, Scores = l.Average(m => m.Score) }).ToList();
            //            foreach (var l in AvgScoreGreater80_2)
            //            {
            //                Console.WriteLine("姓名:{0},学期{1},均分{2}", l.Name, l.Term, l.Scores);
            //            }
        }













        static void Other()
        {
            List<int> list1 = Enumerable.Range(1, 20).ToList();
            List<int> list2 = Enumerable.Range(10, 30).ToList();
            foreach (var item in list1.Except(list2))
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            foreach (var item in list2.Except(list1))
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            var result = list1.Aggregate((a, b) => (a + b));

            Console.WriteLine(result);

            list1.Cast<string>().ToList();//(只是在同类型转换)
            
            Console.WriteLine(list1.Any(x => x == 10)); ;

        }

    }

    public class StudentScore
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Course { set; get; }
        public int Score { set; get; }
        public string Term { set; get; }
    }
    
}
