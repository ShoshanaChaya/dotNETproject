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

    // <summary>
    // Interaction logic for GuestWindow.xaml
    // </summary>
    public partial class GuestWindow : Window
    {
        public Message _myData;
        IBL myBL;
        BE.GuestRequest guestRequest;
        private List<string> errorMessages;
        public GuestWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SystemCommands.MaximizeWindow(this);
            guestRequest = new BE.GuestRequest();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.addGuestRequest.DataContext = guestRequest;
            myBL = FactoryBL.getBL();

            this.entryDateDatePicker.DisplayDateStart = DateTime.Now;
            this.entryDateDatePicker.DisplayDateEnd = DateTime.Now.AddMonths(11);
            this.releaseDateDatePicker.DisplayDateStart = DateTime.Now;
            this.releaseDateDatePicker.DisplayDateEnd = DateTime.Now.AddMonths(11);

            this.areaComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Area));
            this.poolComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Pool));
            this.jacuzziComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Jacuzzi));
            this.spookyComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Spooky));
            this.typeComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ResortType));
            this.childrensAttractionsComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ChildrensAttractions));
            this.gardenComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Garden));

            _myData = new Message()
            {
                message = ""
            };
            TextBlockReview.DataContext = _myData;
            errorMessages = new List<string>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

                myBL.AddGuestRequest(guestRequest);
                MessageBox.Show($"The order was added to the system, If there is a suitable unit you will receive an email", "", MessageBoxButton.OK, MessageBoxImage.Information);


                familyNameTextBox.Text = "";
                privateNameTextBox.Text = "";
                mailAddressTextBox.Text = "";
                CodeTextBox.Text = "";
                entryDateDatePicker.Text = "1/1/2020";
                releaseDateDatePicker.Text = "2/1/2020";
                adultsTextBox.Text = "";
                childrenTextBox.Text = "";
                gardenComboBox.Text = null;
                jacuzziComboBox.Text = null;
                poolComboBox.Text = null;
                spookyComboBox.Text = null;
                typeComboBox.Text = null;
                childrensAttractionsComboBox.Text = null;
                areaComboBox.Text = null;

                guestRequest = new BE.GuestRequest();
                this.addGuestRequest.DataContext = guestRequest;
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
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
            this.addOrder.IsEnabled = !errorMessages.Any();
        }

        private void AdultsTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guestRequest.Adults = int.Parse(adultsTextBox.Text);
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
                guestRequest.Children = int.Parse(childrenTextBox.Text);
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
                guestRequest.PrivateName = privateNameTextBox.Text;
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
                guestRequest.FamilyName = familyNameTextBox.Text;
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
                guestRequest.MailAddress = mailAddressTextBox.Text;
            }
            catch
            {
                mailAddressTextBox.BorderBrush = Brushes.CornflowerBlue;
            }
        }
        private void CodeGotFocus(object sender, RoutedEventArgs e)
        {
            CodeTextBox.BorderBrush = Brushes.Black;
        }
        private void CodeLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guestRequest.Code = CodeTextBox.Text;
            }
            catch
            {
                CodeTextBox.BorderBrush = Brushes.CornflowerBlue;
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
            string s = " " + (guestRequest.ReleaseDate - guestRequest.EntryDate).TotalDays;
            //string s = " " + myBL.amountOfVacationDays(guestRequest);
            this.amountOfVacationDaysTextBlock.Text = s;
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if(TextBlock_NoteToWebsiteManager.Text=="")
            {
                MessageBox.Show("Please enter a review before submitting", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            messages review = new messages();
            review.message = TextBlock_NoteToWebsiteManager.Text;
            review.date = DateTime.Now;
            myBL.AddReviewFromGuest(review);
            //Configuration.reviewsFromGuests.Add(review);
            TextBlock_NoteToWebsiteManager.Text = "";
            _myData.message = "thank you for your review";
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
