using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Arithmetic a = new Arithmetic();
            a.LongestPalindrome_0("cabcba");
            Console.Read();
            
        }
    }

    public class Arithmetic
    {
        #region 两数之和
        public int[] TwoSum(int[] nums, int target)
        {
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Millisecond);
            int[] array = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = nums.Length - 1; j > i; j--)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        array[0] = nums[i];
                        array[1] = nums[j];
                        break;
                    }
                }
            }
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Millisecond);
            return array;
        }

        #endregion

        #region 无重复字符的最长子串
        public int LengthOfLongestSubstring(string s)
        {
            char[] arrayS = s.ToCharArray();
            List<char> tempList = new List<char>();
            List<char> currentList = new List<char>();
            foreach (var item in arrayS)
            {
                if (tempList.Contains(item))
                {
                    //检测到了碰撞
                    if (currentList.Count<tempList.Count)
                    {
                        currentList.Clear();
                        currentList.Add(item);
                        currentList.AddRange(tempList);
                        tempList.Clear();
                    }
                    tempList.Clear();
                }
                else
                {
                    tempList.Add(item);
                }


                //边界条件
                if (currentList.Count < tempList.Count)
                {
                    currentList.Clear();
                    currentList.Add(item);
                    currentList.AddRange(tempList);
                    if (currentList[0]==currentList[currentList.Count-1])
                    {
                        currentList.RemoveAt(currentList.Count-1);
                    }
                }

            }

            foreach (var item in currentList)
            {
                Console.WriteLine(item);
            }

            return currentList.Count;

        }
        #endregion


        #region 寻找两个有序数组的中位数
        public double findMedianSortedArrays(int[] A, int[] B)
        {
            int m = A.Length;
            int n = B.Length;
            if (m > n)
            { // to ensure m<=n
                int[] temp = A; A = B; B = temp;
                int tmp = m; m = n; n = tmp;
            }
            int iMin = 0, iMax = m, halfLen = (m + n + 1) / 2;
            while (iMin <= iMax)
            {
                int i = (iMin + iMax) / 2;
                int j = halfLen - i;
                if (i < iMax && B[j - 1] > A[i])
                {
                    iMin = i + 1; // i is too small
                }
                else if (i > iMin && A[i - 1] > B[j])
                {
                    iMax = i - 1; // i is too big
                }
                else
                { // i is perfect
                    int maxLeft = 0;
                    if (i == 0) { maxLeft = B[j - 1]; }
                    else if (j == 0) { maxLeft = A[i - 1]; }
                    else { maxLeft = Math.Max(A[i - 1], B[j - 1]); }
                    if ((m + n) % 2 == 1) { return maxLeft; }

                    int minRight = 0;
                    if (i == m) { minRight = B[j]; }
                    else if (j == n) { minRight = A[i]; }
                    else { minRight = Math.Min(B[j], A[i]); }

                    return (maxLeft + minRight) / 2.0;
                }
            }
            return 0.0;
        }

        #endregion


        #region 最长回文子串
        //给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。
        //。回文是一个正读和反读都相同的字符串
        public string LongestPalindrome_0(string s)//中心扩展法
        {
            int length = s.Length;

            if (length==1)
            {
                return s;
            }

            int start = 0, end = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int len1 = Palindrome(s,i,i);
                int len2 = Palindrome(s,i,i+1);
                int len = Math.Max(len1,len2);
                if (len>end-start)
                {
                    start = i - (len - 1) / 2;
                    end = i + len + 2;
                }
            }
            return s.Substring(start,end+1);
        }

        public int Palindrome(string str,int left,int right)
        {
            int l = left, R = right;
            while (l>=0&&R<str.Length&&str[left]==str[right])
            {
                l--;
                R++;
            }
            return R - l - 1;
        }

        #endregion




    }
}
