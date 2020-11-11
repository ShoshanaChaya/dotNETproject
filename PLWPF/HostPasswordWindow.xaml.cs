using System;
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
using System.Windows.Shapes;
using BE;
using BL;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostPasswordWindow.xaml
    /// </summary>
    public partial class HostPasswordWindow : Window
    {
        IBL myBL;
        BE.Host host;
        public HostPasswordWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SystemCommands.MaximizeWindow(this);
            myBL = FactoryBL.getBL();
            host = new Host();
        }
        private void Button_Click_EnterAsHost(object sender, RoutedEventArgs e)
        {
            if (HostKey.Text == "")
            {
                MessageBox.Show("Enter a host key", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = from item in myBL.GetHosts()
                         where item.HostKey.CompareTo(HostKey.Text) == 0
                         select item;

            if (result.Count() == 0)
            {
                HostKey.Text = "";
                MessageBox.Show("There is no host with this key", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //var result2 = from item in myBL.GetHostingUnitCopy()
                //              where item.Owner.HostKey.CompareTo(HostKey.Text) == 0
                //              select item;
                HostWindow window = new HostWindow(result.Single());
                Close();
                window.ShowDialog();
                Close();
            }
        }
        //private void Button_Click_Enter(object sender, RoutedEventArgs e)
        //{
        //    int s = int.Parse(HostKey.Text);
        //    var result = from item in myBL.GetHosts()
        //                 where item.HostKey == s
        //                 select item;

        //    if (result.Count() == 0)
        //    {
        //        MessageBox.Show("There is no host with this key", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    else
        //    {
        //        HostWindow window = new HostWindow(s);
        //        window.ShowDialog();
        //    }
        ////  MouseEnter="Button_Click_Enter"
        //}
    }
}
