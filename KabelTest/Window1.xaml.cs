using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Shapes;

namespace KabelTest
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string[] ports;

        public Window1()
        {
            InitializeComponent();
            ports = SerialPort.GetPortNames();
            COM_port_choice.ItemsSource = ports;
        }
        private void COM_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void COM_OK_Click(object sender, RoutedEventArgs e)
        {
            COM_Info.Content = SerialPort_class.ConnectingSerialPort(COM_port_choice.Text);
        }

        private void Refresh_SerialList_Click(object sender, RoutedEventArgs e)
        {
            ports = SerialPort.GetPortNames();           
            COM_port_choice.ItemsSource = ports;
            COM_Info.Content = "Список обновлён";
        }

        private void COM_port_choice_GotFocus(object sender, RoutedEventArgs e)
        {
            COM_Info.Content = ("Выбран: " + COM_port_choice.Text);
        }
    }
}
