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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL myBL;
        public MainWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SystemCommands.MaximizeWindow(this);
            myBL = FactoryBL.getBL();
            List<BankBranch> lst = new List<BankBranch>();
            lst = myBL.GetBankBranches();
            myBL.startThread();
            InitializeComponent();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Window guestWindow = new GuestMainWindow();
            //Close();
            guestWindow.ShowDialog();
        }

        private void HostButton_Click(object sender, RoutedEventArgs e)
        {
            HostPasswordWindow window = new HostPasswordWindow();
            //Close();
            window.Show();
        }

        private void WebsiteManagerButton_Click(object sender, RoutedEventArgs e)
        {
            Window websiteManagerWindow = new WebsiteManagerWindow();
            //Close();
            websiteManagerWindow.Show();
        }
    }
}