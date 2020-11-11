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
    /// Interaction logic for UpdateHostingUnit.xaml
    /// </summary>
    public partial class UpdateHostingUnit : Window
    {
        private HostingUnit HostingUnit;
        BL.IBL myBL = FactoryBL.getBL();
        Host host = new Host();
        private List<string> errorMessages;

        public UpdateHostingUnit(HostingUnit temp)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //HostingUnit.Diary = new bool[31, 12];
            HostingUnit = temp;
            this.DataContext = HostingUnit;
            host = temp.Owner;
            InitializeComponent();

            this.areaComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Area));
            this.poolComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Pool));
            this.jacuzziComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Jacuzzi));
            this.spookyComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Spooky));
            this.typeComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ResortType));
            this.childrensAttractionsComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ChildrensAttractions));
            this.gardenComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Garden));
            this.UpdateHostingUnitGrid.DataContext = temp;

            errorMessages = new List<string>();
            // setHostingUnitFields();
        }

        private bool IsMissingDetails()
        {
            if (areaComboBox.Text == null) { return false; }
            if (childrensAttractionsComboBox.Text == null) { return false; }
            if (gardenComboBox.Text == null) { return false; }
            if (hostingUnitNameTextBox.Text == "") { return false; }
            if (jacuzziComboBox.Text == null) { return false; }
            if (poolComboBox.Text == null) { return false; }
            if (spookyComboBox.Text == null) { return false; }
            if (typeComboBox.Text == null) { return false; }
            return true;
        }

        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);

            this.buttonUpdateHostingunit.IsEnabled = !errorMessages.Any();
        }

        //private void setHostingUnitFields()
        //{
        //    this.UpdateHostingUnitGrid.DataContext = HostingUnit;
        //    this.hostingUnitNameTextBox.Text = HostingUnit.HostingUnitName;
        //}
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsMissingDetails() == false)
            {
                MessageBox.Show("Missing Information ", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Close();
            try
            {
                myBL.UpdateHostingUnit(HostingUnit);
                MessageBox.Show("Hosting unit update successfully", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                Close();
                HostWindow w = new HostWindow(host);
                w.ShowDialog();
                // gardenComboBox.Text = null;
                // jacuzziComboBox.Text = null;
                // poolComboBox.Text = null;
                // spookyComboBox.Text = null;
                // typeComboBox.Text = null;
                // childrensAttractionsComboBox.Text = null;
                // areaComboBox.Text = null;
                // hostingUnitNameTextBox.Text = "";

                //HostingUnit = new BE.HostingUnit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Inner exception", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                //setHostingUnitFields();
                return;
            }
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
                HostingUnit.HostingUnitName = hostingUnitNameTextBox.Text;
            }
            catch (Exception exp)
            {
                hostingUnitNameTextBox.BorderBrush = Brushes.CornflowerBlue;
                hostingUnitLable.Content = exp.Message;
            }
        }

        private void updateHostingUnit(object sender, RoutedEventArgs e)
        {
            this.UpdateHostingUnitGrid.DataContext = HostingUnit;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource hostingUnitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("hostingUnitViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // hostingUnitViewSource.Source = [generic data source]
        }
    }
}