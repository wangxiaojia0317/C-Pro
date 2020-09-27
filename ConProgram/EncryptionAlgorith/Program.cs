using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAlgorith
{
    class Program
    {
        static void Main(string[] args)
        {
            //8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
            string s1 = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";

           string s2 = sha256("123456");

            Console.WriteLine(s2.ToLower());

            if (s1.Equals(s2, StringComparison.OrdinalIgnoreCase) == true)
            {
                Console.WriteLine("校验成功");
            }
            Console.Read();
        }

        public static string sha256(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            } return builder.ToString();
        }

    }
}
