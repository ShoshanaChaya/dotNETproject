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


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for WebsiteManagerWindow.xaml
    /// </summary>
    public partial class WebsiteManagerWindow : Window
    {
        public WebsiteManagerWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SystemCommands.MaximizeWindow(this);
        }

        private void Button_Click_EnterAsAdmin(object sender, RoutedEventArgs e)
        {
            if (PassBox_passAdmin.Password == Configuration.password)
            {
                WebsiteManagerMainWindow window = new WebsiteManagerMainWindow();
                Close();
                window.Show();
            }
            else
            {
                PassBox_passAdmin.Password = "";
                MessageBox.Show("Wrong Password", "warning for wrong manager password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
