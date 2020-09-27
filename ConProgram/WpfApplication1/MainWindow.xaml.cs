using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = System.Windows.ResizeMode.CanMinimize;
        }
        

        private void button_run_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("asfjo");
        }
    }


    
}
