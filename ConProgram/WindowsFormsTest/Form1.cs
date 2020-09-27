using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsTest
{

   


    public partial class Form1 : Form
    {

        Thread th;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image img = Image.FromFile(@"C:\Users\Victor\Desktop\revit插件-主界面_登录.png");

            BinaryFormatter binFormatter = new BinaryFormatter();

            MemoryStream memStream = new MemoryStream();

            binFormatter.Serialize(memStream, img);

            byte[] bytes = memStream.GetBuffer();

            string base64 = Convert.ToBase64String(bytes);

            File.WriteAllText(@"C:\Users\Victor\Desktop\revit插件-主界面_登录1222.txt", base64);
            MessageBox.Show("写入完毕");

        }


        private void ToBase64(object sender, EventArgs e)

        {

            

        }



        private void button1_Click(object sender, EventArgs e)
        {
            
               
     
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
