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
    public class Message : DependencyObject
    {
        public string message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("message",
            typeof(string), typeof(Message), new UIPropertyMetadata(""));
    }

    /// <summary>
    /// Interaction logic for AddRequestOldUserMainWindow.xaml
    /// </summary>
    public partial class AddRequestOldUserMainWindow : Window
    {
        public Message _myData;
        private GuestRequest guest = new GuestRequest();
        BL.IBL myBL = FactoryBL.getBL();
        private List<string> errorMessages;
        private IEnumerable<GuestRequest> mainList;
        public AddRequestOldUserMainWindow(GuestRequest guestRequest)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            guest.Code = guestRequest.Code;
            guest = guestRequest;
            
            //this.DataContext = guest;
            InitializeComponent();

            mainList = new List<GuestRequest>();
            //List<GuestRequest> list = new List<GuestRequest>();
            //foreach(var item in myBL.GetGuests())
            //{
            //    if (item.Code.CompareTo(guestRequest.Code) == 0)
            //    {
            //        list.Add(item);
            //    }
            //}
            var result = from item in myBL.GetGuests()
                         where item.Code.CompareTo(guestRequest.Code) == 0
                         select item;
            mainList = result;
            this.GuestRequestList.ItemsSource = mainList;
           // this.GuestRequestList.ItemsSource = list;

            guest.PrivateName = guestRequest.PrivateName;
            guest.FamilyName = guestRequest.FamilyName;
            guest.MailAddress = guestRequest.MailAddress;
            guest.Code = guestRequest.Code;
            this.newPass.Text = "";
            this.oldPass.Text = "";

            _myData = new Message()
            {
                message = ""
            };
            TextBlockReview.DataContext = _myData;
            errorMessages = new List<string>();
        }
        public void mouseDoubleClick(object sender, RoutedEventArgs e)
        {
            MoreInfo.IsEnabled = true;
            //Remove.IsEnabled = true;
            //Update.IsEnabled = true;
        }

        //public void updateClickButton(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (privateNameTextBox.Text=="" || familyNameTextBox.Text=="" || mailAddressTextBox.Text=="")
        //        {
        //            MessageBox.Show("Missing information", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        guest.PrivateName = privateNameTextBox.Text;
        //        guest.FamilyName = familyNameTextBox.Text;
        //        guest.MailAddress = mailAddressTextBox.Text;
        //        myBL.UpdateGuestRequestInfo(guest);

        //        Close();
        //        AddRequestOldUserMainWindow window = new AddRequestOldUserMainWindow(guest);
        //        window.ShowDialog();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Inner exception", "", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //}

        public void MoreInfoClick(object sender, RoutedEventArgs e)
        {
            MoreInfoGuestRequestWindow window = new MoreInfoGuestRequestWindow(GuestRequestList.SelectedItem);
            window.Show();
            //if (GuestRequestList.SelectedItem != null)
            //  MessageBox.Show(GuestRequestList.SelectedItem.ToString(), "Hosting unit information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
        }

        //public void RemoveClick(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        myBL.RemoveGuestRequest(guest);
        //        MessageBox.Show("Your request was successfuly removed", "", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch(System.Exception exp)
        //    {
        //      MessageBox.Show("There was a problem removeing your request", "", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        public void AddNewRequestButton(object sender, RoutedEventArgs e)
        {
            AddRequestOldUser window = new AddRequestOldUser(guest);
            window.ShowDialog();
            Close();
            AddRequestOldUserMainWindow window1 = new AddRequestOldUserMainWindow(guest);
            window1.ShowDialog();
        }

        //private void PrivateNameLostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        guest.PrivateName = privateNameTextBox.Text;
        //    }
        //    catch (Exception exp)
        //    {
        //        privateNameTextBox.BorderBrush = Brushes.CornflowerBlue;
        //    }
        //}
        //private void PrivateNameGotFocus(object sender, RoutedEventArgs e)
        //{
        //    privateNameTextBox.BorderBrush = Brushes.Black;
        //}

        //private void FamilyNameLostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        guest.FamilyName = familyNameTextBox.Text;
        //    }
        //    catch (Exception exp)
        //    {
        //        familyNameTextBox.BorderBrush = Brushes.CornflowerBlue;
        //    }
        //}
        //private void FamilyNameGotFocus(object sender, RoutedEventArgs e)
        //{
        //    familyNameTextBox.BorderBrush = Brushes.Black;
        //}

        //private void MailAddressLostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        guest.MailAddress = mailAddressTextBox.Text;
        //    }
        //    catch (Exception exp)
        //    {
        //        mailAddressTextBox.BorderBrush = Brushes.CornflowerBlue;
        //    }
        //}
        //private void MailAddressGotFocus(object sender, RoutedEventArgs e)
        //{
        //    mailAddressTextBox.BorderBrush = Brushes.Black;
        //}

        private void updateCodeButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (newPass.Text == "" || oldPass.Text == "")
                {
                    MessageBox.Show("Missing information", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (oldPass.Text != guest.Code)
                {
                    MessageBox.Show("Can't change the code because the old password isn't matching", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                guest.Code = newPass.Text;
                myBL.UpdateGuestRequestCode(guest);
                Close();
                AddRequestOldUserMainWindow window = new AddRequestOldUserMainWindow(guest);
                window.ShowDialog();

            }
            catch
            {
                MessageBox.Show("There was a problem updating the code", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OldPassGotFocus(object sender, RoutedEventArgs e)
        {
            oldPass.BorderBrush = Brushes.Black;
        }

        private void OldPassLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.Code = oldPass.Text;
            }
            catch (Exception exp)
            {
                oldPass.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void NewPassGotFocus(object sender, RoutedEventArgs e)
        {
            newPass.BorderBrush = Brushes.Black;
        }

        private void NewPassLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                guest.Code = newPass.Text;
            }
            catch (Exception exp)
            {
                newPass.BorderBrush = Brushes.CornflowerBlue;
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBlock_NoteToWebsiteManager.Text=="")
            {
                MessageBox.Show("Please add a review before submitting", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            messages review = new messages();
            review.message = TextBlock_NoteToWebsiteManager.Text;
            review.date = DateTime.Now;
            myBL.AddReviewFromGuest(review);
           // Configuration.reviewsFromGuests.Add(review);
            TextBlock_NoteToWebsiteManager.Text = "";
            _myData.message = "thank you for your review";

        }
        private void homePageClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
