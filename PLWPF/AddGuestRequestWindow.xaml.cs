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
    /// Interaction logic for AddGuestRequestWindow.xaml
    /// </summary>
    public partial class AddGuestRequestWindow : Window
    {
        BL.IBL myBL = FactoryBL.getBL();
        IEnumerable<GuestRequest> guest = new List<GuestRequest>();
        GuestRequest oneGuest = new GuestRequest();
        private List<string> errorMessages;

        public AddGuestRequestWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CodePassBox.Password == "")
            {
                MessageBox.Show("Please enter a code", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            var result = from item in myBL.GetGuests()//result saves a list of all the guest requests with the code that was entered
                         where item.Code == CodePassBox.Password
                         select item;
            oneGuest = result.FirstOrDefault();
            guest = result;
            //result.FirstOrDefault().Code = CodePassBox.Password;
            if (result.Count() == 0)
            {
                CodePassBox.Password = "";
                MessageBox.Show("There is no guest with this code", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                //return;
            }
            else
            {
                AddRequestOldUserMainWindow window = new AddRequestOldUserMainWindow(oneGuest);
                Close();
                window.Show();
            }
        }

        private void LostFocsTextBox(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.FirstOrDefault().Code = CodePassBox.Password;
            }
            catch
            {
                CodePassBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void GotFocusTextBox(object sender, RoutedEventArgs e)
        {
            CodePassBox.BorderBrush = Brushes.Black;
        }
    }
}
