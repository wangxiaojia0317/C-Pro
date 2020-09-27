using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 排序散发
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Victor\Desktop\dltest";
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i]);
                Console.WriteLine(Path.GetFileName(files[i]));
                Console.WriteLine("*****************************");
            }
            Console.ReadLine();
        }

       
}


    public class SortArithmetic
    {
        #region 快速排序

        /*
         算法思想
         1.找到哨兵（一般以第一个数据为基准,基准被当做空位）
         2.从有向左寻找，找到第一个小于基准的数据
         3.然后从左向右寻找，找到第一个大于基准的数据
         4.交换
         5.重复这个过程知道左右的索引相同或者挨着
         6.第一个过程结束
         7.开启下一个基准重复

         a[]={6,1,2,7,9,3,4,5,10,8}
         
         1.pivort=a[0]=6

           NO:1
           height=a[7]=5
           right=7
           {5,1,2,7,9,3,4,{},10,8}
           
           low=a[3]=7
           left=3
           {5,1,2,{},9,3,4,7,10,8}


           NO:2
           height=a[6]=4
           right=6
           {5,1,2,4,9,3,{},7,10,8}

           low=a[4]=9
           left=4
           {5,1,2,4,{},3,9,7,10,8}

           NO:3
            height=a[5]=3
            right=5
            {5,1,2,4,3,{},9,7,10,8}
            left+1=right
            break;
           基准上位
           {5,1,2,4,3,6,9,7,10,8}
         2. pivort=a[0]=5
            .............
         
         */
        private static int sortUnit(int[] array, int low, int high)
        {
            int key = array[low];
            while (low < high)
            {
                /*从后向前搜索比key小的值*/
                while (array[high] >= key && high > low)
                    --high;
                /*比key小的放左边*/
                array[low] = array[high];
                /*从前向后搜索比key大的值，比key大的放右边*/
                while (array[low] <= key && high > low)
                    ++low;
                /*比key大的放右边*/
                array[high] = array[low];
            }
            /*左边都比key小，右边都比key大。//将key放在游标当前位置。//此时low等于high */
            array[low] = key;


            Console.WriteLine(string.Join(" ",array));
            Console.WriteLine();
            return high;
        }

        public static void sort(int[] array, int low, int high)
        {
            if (low >= high)
                return;
            /*完成一次单元排序*/
            int index = sortUnit(array, low, high);
            /*对左边单元进行排序*/
            sort(array, low, index - 1);
            /*对右边单元进行排序*/
            sort(array, index + 1, high);
        }
        #endregion

        #region 插入排序

        /*
         算法思想
         1. 将第一个元素与第二个元素比较大小，如果第一个元素小于等于第二个元素，不做处理，继续比较第二个元素和第三个元素。 
        */



        public static void ReSort(ref int[] array)
        {
            for (int i = 0; i < array.Length; i++)　　　　//要将第几位数进行插入
            {
                for (int j = i; j > 0; j--)
                {
                    if (array[j] > array[j - 1]) break;　　//如果要排序的数大于已排序元素的最大值，就不用比较了。不然就要不断比较找到合适的位置
                    else
                    {
                        int sap = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = sap;
                    }
                }
            }
        }



        #endregion
    }

}
