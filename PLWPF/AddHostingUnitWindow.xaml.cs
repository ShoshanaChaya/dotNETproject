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
using Microsoft.Win32;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddHostingUnitWindow.xaml
    /// </summary>
    public partial class AddHostingUnitWindow : Window
    {
        BL.IBL myBL = FactoryBL.getBL();
        BE.HostingUnit hostingunit;
        private List<string> errorMessages;
        Host _host = new Host();
        OpenFileDialog op;
        bool isImageChanged = false;

        public AddHostingUnitWindow(Host h)
        {
            InitializeComponent();
            hostingunit = new BE.HostingUnit();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.addHostingUnit.DataContext = hostingunit;
            myBL = FactoryBL.getBL();
            _host = h;

            this.areaComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Area));
            this.poolComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Pool));
            this.jacuzziComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Jacuzzi));
            this.spookyComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Spooky));
            this.typeComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ResortType));
            this.childrensAttractionsComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ChildrensAttractions));
            this.gardenComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Garden));

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
                if (hostingUnitNameTextBox.Text == "")
                {
                    MessageBox.Show($"Missing the hosting unit name", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    hostingUnitNameTextBox.BorderBrush = Brushes.CornflowerBlue;
                    return;
                }
                if (gardenComboBox.Text == null || jacuzziComboBox.Text == null || poolComboBox.Text == null || spookyComboBox.Text == null || typeComboBox.Text == null || childrensAttractionsComboBox.Text == null || areaComboBox.Text == null)
                {
                    MessageBox.Show($"Missing information, please check what's missing", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    hostingUnitNameTextBox.BorderBrush = Brushes.CornflowerBlue;
                    return;
                }

                hostingunit.HostingUnitName = hostingUnitNameTextBox.Text;
                hostingunit.Area = (BE.Enum.Area)System.Enum.Parse(typeof(BE.Enum.Area), areaComboBox.Text, true);
                hostingunit.Type = (BE.Enum.ResortType)System.Enum.Parse(typeof(BE.Enum.ResortType), typeComboBox.Text, true);
                hostingunit.Spooky = (BE.Enum.Spooky)System.Enum.Parse(typeof(BE.Enum.Spooky), spookyComboBox.Text, true);
                hostingunit.Pool = (BE.Enum.Pool)System.Enum.Parse(typeof(BE.Enum.Pool), poolComboBox.Text, true);
                hostingunit.Jacuzzi = (BE.Enum.Jacuzzi)System.Enum.Parse(typeof(BE.Enum.Jacuzzi), jacuzziComboBox.Text, true);
                hostingunit.Garden = (BE.Enum.Garden)System.Enum.Parse(typeof(BE.Enum.Garden), gardenComboBox.Text, true);
                hostingunit.ChildrensAttractions = (BE.Enum.ChildrensAttractions)System.Enum.Parse(typeof(BE.Enum.ChildrensAttractions), childrensAttractionsComboBox.Text, true);

                hostingunit.Owner = _host;
                //hostingunit.Diary = new bool[31, 12];
                myBL.AddHostingUnit(hostingunit);
                MessageBox.Show($"The hosting unit " + hostingunit.HostingUnitName + " was added to the system", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();

                gardenComboBox.Text = null;
                jacuzziComboBox.Text = null;
                poolComboBox.Text = null;
                spookyComboBox.Text = null;
                typeComboBox.Text = null;
                childrensAttractionsComboBox.Text = null;
                areaComboBox.Text = null;
                hostingUnitNameTextBox.Text = "";

                if (isImageChanged)
                {
                    try
                    {
                        myBL.AddImage(hostingunit.HostingUnitName, op.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("couldn't add the image" + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                    }
                }

                hostingunit = new BE.HostingUnit();
                this.addHostingUnit.DataContext = hostingunit;
            }
            catch (Exception exp)
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
            this.buttonAddHostingunit.IsEnabled = !errorMessages.Any();
        }


        private void GotFocusAddHostingUnit(object sender, RoutedEventArgs e)
        {
            hostingUnitLable.Content = "";
            HostingunitName.BorderBrush = Brushes.Black;
        }

        private void LostFocusAddHostingUnit(object sender, RoutedEventArgs e)
        {
            try
            {
                hostingunit.HostingUnitName = hostingUnitNameTextBox.Text;
            }
            catch (Exception exp)
            {
                hostingUnitNameTextBox.BorderBrush = Brushes.CornflowerBlue;
                hostingUnitLable.Content = exp.Message;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostViewSource")));
        }
        private void Button_Click_UploadImage(object sender, RoutedEventArgs e)
        {
            op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                isImageChanged = true;
                HotelImage.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}