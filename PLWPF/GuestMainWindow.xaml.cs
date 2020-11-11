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
using System.Globalization;
using BE;
using BL;

namespace PLWPF
{
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for GuestMainWindow.xaml
    /// </summary>
    public partial class GuestMainWindow : Window
    {
        BL.IBL myBL = FactoryBL.getBL();
        public GuestMainWindow()
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SystemCommands.MaximizeWindow(this);
            this.Reviews.ItemsSource = myBL.GetReviewsToGuests();
        }
        private void OldUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddGuestRequestWindow window = new AddGuestRequestWindow();
            window.ShowDialog();
            Close();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (GoodJob.Visibility == Visibility.Visible)
            {
                GuestWindow window = new GuestWindow();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show($"You must agree first to the terms of use in order to continue", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TermsUserButton_Click(object sender, RoutedEventArgs e)
        {
            TermsOfUse window = new TermsOfUse();
            window.ShowDialog();
        }
        private void BackToHomePage_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}