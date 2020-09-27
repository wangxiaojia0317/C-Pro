using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF测试
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }


        #region Max&Normal
        bool isMax = false;
        private void button_Click(object sender, RoutedEventArgs e)
        {
           
        }
        #endregion

        //private void ScreenAdaptation()
        //{
        //    if (isMax == false)
        //    {
        //        this.Width = SystemParameters.PrimaryScreenWidth;
        //        this.Height = SystemParameters.PrimaryScreenHeight;
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        //        this.Top = 0;
        //        this.Left = 0;
        //        isMax = true;
        //    }
        //    else
        //    {
        //        this.Width = 525;//应该使用原始的数值
        //        this.Height = 350;
        //        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        //        this.Top = 200;
        //        this.Left = 300;
        //        isMax = false;
        //    }

        //    UIElementCollection collection = ViewboxGrid.Children;
           
        //    if (isMax)
        //    {
        //        foreach (var item in collection)
        //        {
        //            ((Button)item).Margin = new Thickness();
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in collection)
        //        {
        //            ((Button)item).Margin = new Thickness();
        //        }
        //    }
        //}

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                comboBox.Items.Add(i);
            }



        }
    }




}
