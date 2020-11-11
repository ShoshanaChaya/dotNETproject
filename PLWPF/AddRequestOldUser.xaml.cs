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
    /// Interaction logic for AddRequestOldUser.xaml
    /// </summary>
    public partial class AddRequestOldUser : Window
    {
        private GuestRequest guest = new GuestRequest();
        BL.IBL myBL = FactoryBL.getBL();
        private List<string> errorMessages;
        private IEnumerable<GuestRequest> mainList;

        public AddRequestOldUser(GuestRequest guestRequest)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            guest = guestRequest;
            InitializeComponent();
            //this.addGuestRequest.DataContext = guestRequest;
            //this.DataContext = guest;

            mainList = new List<GuestRequest>();

            this.areaComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Area));
            this.poolComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Pool));
            this.jacuzziComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Jacuzzi));
            this.spookyComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Spooky));
            this.typeComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ResortType));
            this.childrensAttractionsComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ChildrensAttractions));
            this.gardenComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Garden));
            guestRequest.EntryDate = DateTime.Now;
            guestRequest.ReleaseDate = DateTime.Now;
            this.addGuestRequest.DataContext = guestRequest;
            guest.PrivateName = guestRequest.PrivateName;
            guest.FamilyName = guestRequest.FamilyName;
            guest.MailAddress = guestRequest.MailAddress;
            guest.Code = guestRequest.Code;
            entryDateDatePicker.DisplayDateStart = DateTime.Now;
            entryDateDatePicker.DisplayDateEnd = DateTime.Now.AddMonths(11);
            releaseDateDatePicker.DisplayDateStart = DateTime.Now;
            releaseDateDatePicker.DisplayDateEnd = DateTime.Now.AddMonths(11);
            errorMessages = new List<string>();


        }

        private void ButtonClick(object sender, RoutedEventArgs e)
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
                int number;
                if (!int.TryParse(adultsTextBox.Text, out number) || !int.TryParse(childrenTextBox.Text, out number))
                {
                    MessageBox.Show("Please check you entered an invalid input", "warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (familyNameTextBox.Text == "" || privateNameTextBox.Text == "" || mailAddressTextBox.Text == "" || mailAddressTextBox.Text == "" || entryDateDatePicker.Text == "1/1/0001" || releaseDateDatePicker.Text == "1/1/0001" || adultsTextBox.Text == "" || childrenTextBox.Text == "")
                {
                    MessageBox.Show($"Missing information", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                guest.Adults = int.Parse(adultsTextBox.Text);
                guest.amount = int.Parse(amountOfVacationDaysTextBlock.Text);
                guest.amountOFPeople = int.Parse(adultsTextBox.Text + childrenTextBox.Text);
                guest.Area = (BE.Enum.Area)System.Enum.Parse(typeof(BE.Enum.Area), areaComboBox.Text, true);
                guest.Children = int.Parse(childrenTextBox.Text);
                guest.ChildrensAttractions = (BE.Enum.ChildrensAttractions)System.Enum.Parse(typeof(BE.Enum.ChildrensAttractions), childrensAttractionsComboBox.Text, true);
                guest.EntryDate = DateTime.Parse(entryDateDatePicker.Text);
                guest.FamilyName = familyNameTextBox.Text;
                guest.Garden = (BE.Enum.Garden)System.Enum.Parse(typeof(BE.Enum.Garden), gardenComboBox.Text, true);
                guest.Jacuzzi = (BE.Enum.Jacuzzi)System.Enum.Parse(typeof(BE.Enum.Jacuzzi), jacuzziComboBox.Text, true);
                guest.MailAddress = mailAddressTextBox.Text;
                guest.Pool = (BE.Enum.Pool)System.Enum.Parse(typeof(BE.Enum.Pool), poolComboBox.Text, true);
                guest.PrivateName = privateNameTextBox.Text;
                guest.RegistrationDate = DateTime.Now;
                guest.ReleaseDate = DateTime.Parse(releaseDateDatePicker.Text);
                guest.Spooky = (BE.Enum.Spooky)System.Enum.Parse(typeof(BE.Enum.Spooky), spookyComboBox.Text, true);
                guest.Status = BE.Enum.CustomerOrderStatus.Open;
                guest.Type = (BE.Enum.ResortType)System.Enum.Parse(typeof(BE.Enum.ResortType), typeComboBox.Text, true);


                myBL.AddGuestRequest(guest);
                MessageBox.Show($"The order was added to the system, If there is a suitable unit you will receive an email", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

                //AddRequestOldUser window = new AddRequestOldUser(guest);
                //window.ShowDialog();

                //familyNameTextBox.Text = "";
                //privateNameTextBox.Text = "";
                //mailAddressTextBox.Text = "";
                entryDateDatePicker.Text = "1/1/2020";
                releaseDateDatePicker.Text = "1/2/2020";
                adultsTextBox.Text = "";
                childrenTextBox.Text = "";
                gardenComboBox.Text = null;
                jacuzziComboBox.Text = null;
                poolComboBox.Text = null;
                spookyComboBox.Text = null;
                typeComboBox.Text = null;
                childrensAttractionsComboBox.Text = null;
                areaComboBox.Text = null;
                this.amountOfVacationDaysTextBlock.Text = "0";

                guest = new BE.GuestRequest();
                this.addGuestRequest.DataContext = guest;
            }
            catch (System.Exception exp)
            {
                 MessageBox.Show(exp.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Close();
            AddRequestOldUser w = new AddRequestOldUser(guest);
            w.Show();
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
            this.Button_Click.IsEnabled = !errorMessages.Any();
        }

        private void AdultsTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.Adults = int.Parse(adultsTextBox.Text);
            }
            catch
            {
                adultsTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void AdultsTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            adultsTextBox.BorderBrush = Brushes.Black;
        }

        private void ChildrenTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.Children = int.Parse(childrenTextBox.Text);
            }
            catch
            {
                childrenTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void ChildrenTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            childrenTextBox.BorderBrush = Brushes.Black;
        }
        private void PrivateNameLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.PrivateName = privateNameTextBox.Text;
            }
            catch
            {
                privateNameTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }
        private void PrivateNameGotFocus(object sender, RoutedEventArgs e)
        {
            privateNameTextBox.BorderBrush = Brushes.Black;
        }
        private void FamilyNameLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.FamilyName = familyNameTextBox.Text;
            }
            catch
            {
                familyNameTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }
        private void FamilyNameGotFocus(object sender, RoutedEventArgs e)
        {
            familyNameTextBox.BorderBrush = Brushes.Black;
        }
        private void MailAddressLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.MailAddress = mailAddressTextBox.Text;
            }
            catch
            {
                mailAddressTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }
        private void MailAddressGotFocus(object sender, RoutedEventArgs e)
        {
            mailAddressTextBox.BorderBrush = Brushes.Black;
        }

        private void releaseDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!entryDateDatePicker.SelectedDate.HasValue)
            {
                amountOfVacationDaysTextBlock.Text = "Select dates";
                return;
            }
            string s = " " + myBL.amountOfVacationDays(guest);
            this.amountOfVacationDaysTextBlock.Text = s;
        }
    }
}


