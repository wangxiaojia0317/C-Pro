using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinYinRegex
{
    class Program
    {
        static void Main(string[] args)
        {

            #region MyRegion
            List<string> list = new List<string> {
    "呼保义","宋江",
    "玉麒麟","卢俊义",
    "智多星","吴用",
    "入云龙","公孙胜",
    "大刀","关胜",
    "豹子头","林冲",
    "霹雳火","秦明",
    "双鞭","呼延灼",
    "小李广","花荣",
    "小旋风","柴进",
    "扑天雕","李应",
    "美髯公","朱仝",
    "花和尚","鲁智深",
    "行者","武松",
    "双枪将","董平",
    "没羽箭","张清",
    "青面兽","杨志",
    "金枪手","徐宁",
    "急先锋","索超",
    "神行太保","戴宗",
    "赤发鬼","刘唐",
    "黑旋风","李逵",
    "九纹龙","史进",
    "没遮拦","穆弘",
    "挿翅虎","雷横",
    "混江龙","李俊",
    "立地太岁","阮小二",
    "船火儿","张横 ",
    "短命二郎","阮小五",
    "浪里白跳","张顺",
    "活阎罗","阮小七",
    "病关索","杨雄",
    "拼命三郎","石秀",
    "两头蛇","解珍 ",
    "双尾蝎","解宝",
    "浪子","燕青",
    "神机军师","朱武",
    "镇三山","黄信",
    "病尉遅","孙立",
    "丑郡马","宣赞",
    "井木犴","郝思文",
    "百胜将","韩滔",
    "天目将","彭玘",
    "圣水将","单廷圭",
    "神火将","魏定国",
    "圣手书生","萧让",
    "铁面孔目","裴宣",
    "摩云金翅","欧鹏",
    "火眼","邓飞",
    "锦毛虎","燕顺",
    "锦豹子","杨林",
    "轰天雷","凌振",
    "神算子","蒋敬",
    "小温侯","吕方",
    "赛仁贵","郭盛",
    "神医","安道全",
    "紫髯伯","皇甫端",
    "矮脚虎","王英",
    "一丈青","扈三娘",
    "丧门神","鲍旭",
    "混世魔王","樊瑞 ",
    "毛头星","孔明",
    "独火星","孔亮",
    "八臂哪吒","项充",
    "飞天大圣","李衮",
    "玉臂匠","金大坚",
    "铁笛仙","马麟",
    "出洞蛟","童威",
    "翻江蜃","童猛",
    "玉幡竿","孟康",
    "通臂猿","侯健",
    "跳涧虎","陈达",
    "白花蛇","杨春",
    "白面郎君","郑天寿",
    "九尾亀","陶宗旺",
    "铁扇子","宋清",
    "铁叫子","乐和",
    "花项虎","龚旺",
    "中箭虎","丁得孙",
    "小遮拦","穆春",
    "操刀鬼","曹正",
    "云里金刚","宋万" ,
    "摸着天","杜迁" ,
    "病大虫","薛永" ,
    "打虎将","李忠" ,
    "小霸王","周通" ,
    "金钱豹子","汤隆",
    "鬼睑儿","杜兴",
    "出林龙","邹渊",
    "独角龙","邹润",
    "旱地忽律","朱贵",
    "笑面虎","朱富",
    "金眼彪","施恩",
    "铁臂膊","蔡福",
    "一枝花","蔡庆",
    "催命判官","李立",
    "青眼虎","李云",
    "没面目","焦挺",
    "石将军","石勇",
    "小尉遅","孙新",
    "母大虫","顾大嫂",
    "菜园子","张青",
    "母夜叉","孙二娘",
    "活闪婆","王定六",
    "除道神","郁保四",
    "白日鼠","白胜",
    "鼓上蚤","时迁",
    "金毛犬","段景住"
};



            #endregion





            Console.Read();
        }


        public static List<string> Connect(List<string> list)
        {


            List<string> pinYin = new List<string>();
            list.ForEach(x => x.Trim());
            int length = list.Select(x => x.Length).Max();
            string ss = string.Empty;
            int offset = 0;
            foreach (var item in list)
            {
                ss = GetFirstPinyin(item);
                offset = length - ss.Length;
                for (int i = 0; i < offset; i++)
                {
                    ss += "*";
                }
                pinYin.Add(ss + "_" + item);
            }


            List<string> tempList = pinYin.OrderBy(x => x.Split('_')[0]).ToList();


            //for (int i = 0; i < tempList.Count; i++)
            //{
            //    tempList[i] = tempList[i][0]+ tempList[i].Split('_')[1];
            //}

            return tempList;
        }


        public static string GetPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

        /// <summary> 
                /// 汉字转化为拼音首字母
                /// </summary> 
                /// <param name="str">汉字</param> 
                /// <returns>首字母</returns> 
        public static string GetFirstPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }




    }
}
