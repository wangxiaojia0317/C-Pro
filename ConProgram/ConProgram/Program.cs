using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace 正则表达式
{
    class Program
    {
      
        static void Main(string[] args)
        {

            while (true)
            {
                //Regex P_regex = new Regex("^[\u4E00-\u9FA5]{0,}$");

                //Console.WriteLine(P_regex.IsMatch(Console.ReadLine()) + "匹配到了中文"); ;


                //string regularExpression0 = @"^[\u4E00-\u9FA5]{0,}$";

                //Console.WriteLine(Regex.IsMatch(Console.ReadLine(), regularExpression0) + "匹配到了中文"); ;



                //string regularExpression1 = @"^[0-9]{0,}$";

                //Console.WriteLine(Regex.IsMatch(Console.ReadLine(), regularExpression1) + "匹配到了数字"); ;


                string regularExpression2 = @"^[a-zA-Z]{0,}$";
                string regularExpression3 = @"^[0-9 | .?]$";

                Console.WriteLine(Regex.IsMatch(Console.ReadLine(), "[^0-9]") + "匹配到了字母"); ;

            }
            //string path = @"C:\Users\liuxiong\Desktop\算法导论_原书第3版_CHS.pdf";


            /*字符描述
              
            \转义字符，将一个具有特殊功能的字符转义为一个普通字符，或反过来
            ^匹配输入字符串的开始位置

            $匹配输入字符串的结束位置

            *匹配前面的零次或多次的子表达式
             
            + 匹配前面的一次或多次的子表达式

            ?匹配前面的零次或一次的子表达式

            {n}n是一个非负整数，匹配前面的n次子表达式

            {n,}n是一个非负整数，至少匹配前面的n次子表达式

            {n,m}m和n均为非负整数，其中n<=m，最少匹配n次且最多匹配m次
           
            ?当该字符紧跟在其他限制符（*，+，?，{n}，{n,}，{n，m}）后面时，匹配模式尽可能少的匹配所搜索的字符串
            
            .匹配除“\n”之外的任何单个字符
            
            (pattern) 匹配pattern并获取这一匹配
            
            (?:pattern) 匹配pattern但不获取匹配结果
           
            (?=pattern)正向预查，在任何匹配pattern的字符串开始处匹配查找字符串
           
            (?!pattern) 负向预查，在任何不匹配pattern的字符串开始处匹配查找字符串

            x|y匹配x或y。例如，‘z|food’能匹配“z”或“food”。‘(z|f)ood’则匹配“zood”或“food”

            [xyz]字符集合。匹配所包含的任意一个字符。例如，‘[abc]’可以匹配“plain”中的‘a’

            [^xyz]负值字符集合。匹配未包含的任意字符。例如，‘[^abc]’可以匹配“plain”中的‘p’

            [a-z]匹配指定范围内的任意字符。例如，‘[a-z]’可以匹配'a'到'z'范围内的任意小写字母字符
           
            [^a-z] 匹配不在指定范围内的任意字符。例如，‘[^a-z]’可以匹配不在‘a’～‘z’'内的任意字符

            \b匹配一个单词边界，指单词和空格间的位置
            
            \B匹配非单词边界
            
            \d匹配一个数字字符，等价于[0-9]
            
            \D匹配一个非数字字符，等价于[^0-9]
           
            \f匹配一个换页符
           
            \n匹配一个换行符

            \r匹配一个回车符
            
            \s匹配任何空白字符，包括空格、制表符、换页符等
 
            \S匹配任何非空白字符

            \t匹配一个制表符
            \v匹配一个垂直制表符。等价于\x0b和\cK

            \w匹配包括下划线的任何单词字符。等价于‘'[A-Za-z0-9_]
            ’
            \W匹配任何非单词字符。等价于‘[^A-Za-z0-9_]’*/

            //Match a = Regex.Match("https://wenku.baidu.com/view/73d06daba5e9856a5712607e.html", "\\.\\S\\.");
            //Console.WriteLine(a.Value);
            Console.ReadKey();
        }
        
    }

    public class RegexClass
    {
        string url = "https://wenku.baidu.com/view/73d06daba5e9856a5712607e.html";

        /// <summary>
        ///  IsMatch() 方法
        /// </summary>
        public static void DataRegex_Fun()
        {
            // "028\\d{8}"
            // 028代表的是固定的数字
            // \\第一个\代表的是转义字符，第二个\是正则表达式的固定格式\d代表查找数字，{8}代表的是8个字符
            // Regex.IsMatch(t1, regexTest)  t1原内容，regexTest匹配项
            // 具体详情查看url
            string regexTest = "028\\d{8}";
            string t1 = "02845415698";
            string t2 = "02145415698";
            Console.WriteLine("是否是028开头：" + Regex.IsMatch(t1, regexTest));
            Console.WriteLine("是否是028开头：" + Regex.IsMatch(t2, regexTest));

        }

        /// <summary>
        /// "\\w{1,}@\\w{1,}\\."    “15515303878@qq.com”
        /// </summary>
        public static void Replace_Fun()
        {
            string regexTest = "\\w{1,}@\\w{1,}\\.";
            string t1 = "My Email Address is 15515303878@qq.com";
            if (Regex.IsMatch(t1, regexTest))
            {
                Console.WriteLine("替换后"+Regex.Replace(t1,"@","**"));
            }
            else
            {
                Console.WriteLine("未找到匹配项");
            }
           
        }



        /*
            正则表达式匹配符讲解
            1. \d 匹配的是数字  \d{n} 代表的是匹配几个字符
            2. \D 匹配的是非数字 同理{n}
            3. \w 匹配任意单字符
            4. \W 匹配非单字符 匹配的是@ 
            5. \s 匹配空白字符 “ ”
            6. \S 匹配非空字符
            7. .  匹配任意字符
            8. [....] 匹配括号总的任意字符
            9. [^..] 匹配非括号字符

        */





        /// <summary>
        /// 建立一个合法
        /// ISBN
        /// 验证格式；
        /// 分析：
        ///ISBN格式为X-XXXXX-XXX-X;
        ///"\\d-\\d{5}-\\d{3}-\\d";
        /// </summary>
        public static void Splite_Fun()
        {
            //基本形式
            //Regex(string pattern);   
            //重载形式
            //Regex(string pattern, RegexOptions)；
            //补充：
            //RegexOptions
            //属于枚举类型
            //包括IgnoreCase(忽略大小写)、ReghtToLeft(从右向左)、None（默认）、CultureInvariant(忽略区域)、
            //Multline(多行模式)和SingleLine（单行模式）；

            string regexTest = "\\d-\\d{6}-\\d{3}-\\d";
            Regex ISBNRegex = new Regex(regexTest, RegexOptions.None);

            Console.WriteLine(Regex.IsMatch("4-45789-333-4", regexTest));
        }

    }


    public class RegexTest
    {
        public void IsMatch_Fun()
        {

        }
    }

}
