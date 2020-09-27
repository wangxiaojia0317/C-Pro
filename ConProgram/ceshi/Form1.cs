using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ceshi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        int index = 10;

        private void button1_Click(object sender, EventArgs e)
        {
            index = int.Parse(textBox1.Text);
        }


        void Test()
        {
            for (int i = 0; i < index; i++)
            {
                Thread.Sleep(1000);
                label1.Text = i.ToString() ;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
}
