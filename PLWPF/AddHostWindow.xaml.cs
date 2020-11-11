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
    /// Interaction logic for AddHostWindow.xaml
    /// </summary>
    public partial class AddHostWindow : Window
    {
        BL.IBL myBL;
        private List<string> errorMessages;
        Host host;

        public AddHostWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SystemCommands.MaximizeWindow(this);
            myBL = FactoryBL.getBL();
            IEnumerable<BankBranch> lst = new List<BankBranch>();
            lst = myBL.GetBankBranches();
            InitializeComponent();
            host = new Host();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;


            BankDetailsComboBox.ItemsSource = lst;
            this.grid1.DataContext = host;
            errorMessages = new List<string>();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (errorMessages.Any())
                {
                    string err = "Exception: ";
                    foreach (var item in errorMessages)
                        err += item + "\n";

                    MessageBox.Show(err);
                    return;
                }
                if (privateNameTextBox.Text == "" || familyNameTextBox.Text == "" || phoneNumberTextBox.Text == "" || mailAddressTextBox.Text == "")
                {
                    int phoneNumber;
                    if (!int.TryParse(phoneNumberTextBox.Text, out phoneNumber))
                    {
                        MessageBox.Show("Please enter for the phone number digits", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    MessageBox.Show("Missing information", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                host.BankBranchDetails = (BankBranch)BankDetailsComboBox.SelectedItem;
                myBL.AddHost(host);
                MessageBox.Show($"The host was added to the system", "", MessageBoxButton.OK, MessageBoxImage.Information);

                privateNameTextBox.Text = "";
                familyNameTextBox.Text = "";
                phoneNumberTextBox.Text = "";
                mailAddressTextBox.Text = "";
                bankAccountNumberTextBox.Text = "";

                host = new Host();
                this.grid1.DataContext = host;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void validationError(object sender, ValidationErrorEventArgs err)
        {
            if (err.Action == ValidationErrorEventAction.Added)
            {
                errorMessages.Add(err.Error.Exception.Message);
            }
            else
            {
                errorMessages.Remove(err.Error.Exception.Message);
            }
            this.AddHost.IsEnabled = !errorMessages.Any();
        }

        private void LostFocusFamilyName(object sender, RoutedEventArgs e)
        {
            try
            {
                host.FamilyName = familyNameTextBox.Text;
            }
            catch
            {
                familyNameTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void GotFocusFamilyName(object sender, RoutedEventArgs e)
        {
            familyNameTextBox.BorderBrush = Brushes.Black;
        }

        private void LostFocusHostKey(object sender, RoutedEventArgs e)
        {
            try
            {
                host.HostKey = hostKeyTextBox.Text;
            }
            catch
            {
                hostKeyTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void GotFocusHostKey(object sender, RoutedEventArgs e)
        {
            hostKeyTextBox.BorderBrush = Brushes.Black;
        }

        private void LostFocusMailAddress(object sender, RoutedEventArgs e)
        {
            try
            {
                host.MailAddress = mailAddressTextBox.Text;
            }
            catch
            {
                mailAddressTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void GotFocusMailAddress(object sender, RoutedEventArgs e)
        {
            mailAddressTextBox.BorderBrush = Brushes.Black;
        }

        private void LostFocusFirstName(object sender, RoutedEventArgs e)
        {
            try
            {
                host.PrivateName = privateNameTextBox.Text;
            }
            catch
            {
                privateNameTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void GotFocusFirstName(object sender, RoutedEventArgs e)
        {
            privateNameTextBox.BorderBrush = Brushes.Black;
        }

        private void LostFocusPhoneNumber(object sender, RoutedEventArgs e)
        {
            try
            {
                host.PhoneNumber = (phoneNumberTextBox.Text);
            }
            catch
            {
                phoneNumberTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void GotFocusPhoneNumber(object sender, RoutedEventArgs e)
        {
            phoneNumberTextBox.BorderBrush = Brushes.Black;
        }
        private void GotFocusBank(object sender, RoutedEventArgs e)
        {
            bankAccountNumberTextBox.BorderBrush = Brushes.Black;
        }

        private void LostFocsBank(object sender, RoutedEventArgs e)
        {
            try
            {
                host.BankAccountNumber = int.Parse(bankAccountNumberTextBox.Text);
            }
            catch
            {
                bankAccountNumberTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }
    }


}
