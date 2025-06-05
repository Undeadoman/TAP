using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Калькулятор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



            foreach(UIElement el in sus.Children)
            {
                if(el is Button)
                {
                    ((Button)el).Click += Button_click;
                }
            }
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content;
            if (str == "C")
                окновывода.Text = "";
            else if(str == "=")
            {
                string ответ = new DataTable().Compute(окновывода.Text, null).ToString();
                                    окновывода.Text = ответ;
                                            }
            else
            окновывода.Text += str;
        }
    }
}
