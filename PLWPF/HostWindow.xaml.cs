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
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {
        HostingUnit hostingUnit;
        IEnumerable<HostingUnit> mainList;
        Host host = new Host();
        BL.IBL myBL = FactoryBL.getBL();
        string number;
        Order order = new Order();
        List<Order> o;
        List<HostingUnit> hostingUnitList = new List<HostingUnit>();//all the hosting units of the corrent host
        public HostWindow(Host h)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            SystemCommands.MaximizeWindow(this);
            hostingUnit = new HostingUnit();
            host = h;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (h == null)
            {
                mainList = new List<HostingUnit>();
                this.HostingUnitsList.ItemsSource = mainList;
                return;
            }
            number = h.HostKey;
            mainList = new List<HostingUnit>();
            o = new List<Order>();
            try
            {
                mainList = myBL.GetHostingUnitCopy();
                foreach (var item in myBL.GetHostingUnitCopy())//adds to the list all the hosting units of a host
                {
                    if (item.Owner.HostKey.CompareTo(number) == 0)
                    {
                        hostingUnitList.Add(item);
                    }
                }
                var result1 = from item in myBL.GetHostingUnitCopy()
                              where item.Owner.HostKey == host.HostKey
                              select item;
                foreach (var item2 in result1)
                {
                    foreach (var item3 in myBL.GetOrders())
                    {
                        if (item2.HostingUnitKey == item3.HostingUnitKey)
                        {
                            o.Add(item3);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message);
                mainList = new List<HostingUnit>();
            }



            //this.HostingUnitsList.ItemsSource = mainList;
            this.HostingUnitsList.ItemsSource = hostingUnitList;
            this.OrdersList.ItemsSource = o;
            this.FinalizeComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.typeOfOrders));
            this.GroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.GroupOptions));

        }

        public void update()
        {
            foreach (var item in myBL.GetHostingUnitCopy())//adds to the list all the hosting units of a host
            {
                if (item.Owner.HostKey.CompareTo(number) == 0)
                {
                    hostingUnitList.Add(item);
                }
            }
            this.HostingUnitsList.ItemsSource = hostingUnitList;
        }
        private void SubGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupComboBox.SelectedIndex == 0)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByArea("North", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Area");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByArea("South", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Area");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByArea("West", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Area");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                    case 3:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByArea("East", hostingUnitList);
                        CollectionView view4 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view4.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription4 = new PropertyGroupDescription("Area");
                        view4.GroupDescriptions.Add(groupDescription4);
                        break;
                    case 4:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByArea("Center", hostingUnitList);
                        CollectionView view5 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view5.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription5 = new PropertyGroupDescription("Area");
                        view5.GroupDescriptions.Add(groupDescription5);
                        break;
                }
            }
            else if (GroupComboBox.SelectedIndex == 1)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByType("Tzimer", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Type");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByType("AccommodationApartment", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Type");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByType("Hotel", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Type");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                    case 3:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByType("Tent", hostingUnitList);
                        CollectionView view4 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view4.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription4 = new PropertyGroupDescription("Type");
                        view4.GroupDescriptions.Add(groupDescription4);
                        break;
                }
            }
            else if (GroupComboBox.SelectedIndex == 2)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupBySpooky("NoWay", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Spooky");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupBySpooky("ALittileBit", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Spooky");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupBySpooky("TerrifyMe", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Spooky");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                }
            }
            else if (GroupComboBox.SelectedIndex == 3)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByPool("Necessary", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Pool");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByPool("Optional", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Pool");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByPool("NotInterested", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Pool");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                }
            }
            else if (GroupComboBox.SelectedIndex == 4)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByJacuzzi("Necessary", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Jacuzzi");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByJacuzzi("Optional", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Jacuzzi");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByJacuzzi("NotInterested", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Jacuzzi");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                }
            }
            else if (GroupComboBox.SelectedIndex == 5)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByGarden("Necessary", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Garden");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByGarden("Optional", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Garden");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByGarden("NotInterested", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Garden");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                }
            }
            else if (GroupComboBox.SelectedIndex == 6)
            {
                switch (SubGroupComboBox.SelectedIndex)
                {
                    case 0:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByChildrenAttractions("Necessary", hostingUnitList);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("ChildrensAttractions");
                        view.GroupDescriptions.Add(groupDescription);
                        break;
                    case 1:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByChildrenAttractions("Optional", hostingUnitList);
                        CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view2.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("ChildrensAttractions");
                        view2.GroupDescriptions.Add(groupDescription2);
                        break;
                    case 2:
                        this.HostingUnitsList.ItemsSource = myBL.GetAllHostingUnitsGroupByChildrenAttractions("NotInterested", hostingUnitList);
                        CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                        view3.GroupDescriptions.Clear();
                        PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("ChildrensAttractions");
                        view3.GroupDescriptions.Add(groupDescription3);
                        break;
                }
            }
        }
        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<HostingUnit> mainList = new List<HostingUnit>();
            mainList = myBL.GetHostingUnitCopy();

            //this.HostingUnitsList.ItemsSource = mainList;
            this.HostingUnitsList.ItemsSource = hostingUnitList;

            switch (GroupComboBox.SelectedIndex)
            {
                case 0://area
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.Area>, HostingUnit>> mainList0;
                    mainList0 = myBL.orderByHostingUnit(hostingUnitList, x => x.Area);
                    List<HostingUnit> list = new List<HostingUnit>();
                    foreach (var item0 in mainList0)
                    {
                        foreach (var item01 in item0)
                        {
                            list.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list;
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("Area");
                    view.GroupDescriptions.Add(groupDescription);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Area));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding Area}";
                    break;

                case 1://type
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.ResortType>, HostingUnit>> mainList1;
                    mainList1 = myBL.orderByHostingUnit(hostingUnitList, x => x.Type);
                    List<HostingUnit> list1 = new List<HostingUnit>();
                    foreach (var item0 in mainList1)
                    {
                        foreach (var item01 in item0)
                        {
                            list1.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list1;
                    CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view2.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Type");
                    view2.GroupDescriptions.Add(groupDescription2);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ResortType));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding ResortType}";
                    break;

                case 2://spooky  
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.Spooky>, HostingUnit>> mainList2;
                    mainList2 = myBL.orderByHostingUnit(hostingUnitList, x => x.Spooky);
                    List<HostingUnit> list2 = new List<HostingUnit>();
                    foreach (var item0 in mainList2)
                    {
                        foreach (var item01 in item0)
                        {
                            list2.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list2;
                    CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view3.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("Spooky");
                    view3.GroupDescriptions.Add(groupDescription3);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Spooky));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding Spooky}";
                    break;

                case 3://pool     
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.Pool>, HostingUnit>> mainList3;
                    mainList3 = myBL.orderByHostingUnit(hostingUnitList, x => x.Pool);
                    List<HostingUnit> list3 = new List<HostingUnit>();
                    foreach (var item0 in mainList3)
                    {
                        foreach (var item01 in item0)
                        {
                            list3.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list3;
                    CollectionView view4 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view4.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription4 = new PropertyGroupDescription("Pool");
                    view4.GroupDescriptions.Add(groupDescription4);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Pool));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding Pool}";
                    break;

                case 4://jacuzzi    
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.Jacuzzi>, HostingUnit>> mainList4;
                    mainList4 = myBL.orderByHostingUnit(hostingUnitList, x => x.Jacuzzi);
                    List<HostingUnit> list4 = new List<HostingUnit>();
                    foreach (var item0 in mainList4)
                    {
                        foreach (var item01 in item0)
                        {
                            list4.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list4;
                    CollectionView view5 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view5.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription5 = new PropertyGroupDescription("Jacuzzi");
                    view5.GroupDescriptions.Add(groupDescription5);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Jacuzzi));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding Jacuzzi}";
                    break;

                case 5://Garden    
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.Garden>, HostingUnit>> mainList5;
                    mainList5 = myBL.orderByHostingUnit(hostingUnitList, x => x.Garden);
                    List<HostingUnit> list5 = new List<HostingUnit>();
                    foreach (var item0 in mainList5)
                    {
                        foreach (var item01 in item0)
                        {
                            list5.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list5;
                    CollectionView view6 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view6.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription6 = new PropertyGroupDescription("Garden");
                    view6.GroupDescriptions.Add(groupDescription6);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.Garden));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding Garden}";
                    break;

                case 6://childrenAttraction  
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    IEnumerable<IGrouping<Func<HostingUnit, BE.Enum.ChildrensAttractions>, HostingUnit>> mainList6;
                    mainList6 = myBL.orderByHostingUnit(hostingUnitList, x => x.ChildrensAttractions);
                    List<HostingUnit> list6 = new List<HostingUnit>();
                    foreach (var item0 in mainList6)
                    {
                        foreach (var item01 in item0)
                        {
                            list6.Add(item01);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = list6;
                    CollectionView view7 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view7.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription7 = new PropertyGroupDescription("ChildrensAttractions");
                    view7.GroupDescriptions.Add(groupDescription7);
                    this.SubGroupComboBox.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.ChildrensAttractions));
                    this.SubGroupComboBox.IsEnabled = true;
                    SubGroupComboBox.SelectedItem = "{Binding ChildrensAttractions}";
                    break;

                case 7://all        
                    this.SubGroupComboBox.IsEnabled = false;
                    this.SubGroupComboBox.Text = "";
                    this.HostingUnitsList.ItemsSource = hostingUnitList;
                    CollectionView view8 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view8.GroupDescriptions.Clear();
                    break;

            }
        }



        public void AddNewUnit_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hostingUnitList.FirstOrDefault() == null)
                {
                    var result = from item in myBL.GetHosts()
                                 where item.HostKey.CompareTo(number) == 0
                                 select item;
                    AddHostingUnitWindow HostingUnit = new AddHostingUnitWindow(result.FirstOrDefault());
                    HostingUnit.ShowDialog();
                    Close();
                    HostWindow window1 = new HostWindow(host);
                    window1.ShowDialog();
                    return;
                }
                AddHostingUnitWindow newHostingUnit = new AddHostingUnitWindow(hostingUnitList.FirstOrDefault().Owner);
                newHostingUnit.ShowDialog();
                Close();

                HostWindow window = new HostWindow(host);
                window.ShowDialog();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void mouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Remove.IsEnabled = true;
            Update.IsEnabled = true;
            More_information.IsEnabled = true;
        }

        private void mouseMoreInfoClick(object sender, RoutedEventArgs e)
        {
            MoreInfoHostingUnitWindow window = new MoreInfoHostingUnitWindow(HostingUnitsList.SelectedItem);
            window.Show();
            //    if (HostingUnitsList.SelectedItem != null)
            //        MessageBox.Show(HostingUnitsList.SelectedItem.ToString(), "Hosting unit information", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RtlReading);
        }

        private void mouseRemoveClick(object sender, RoutedEventArgs e)
        {
            try
            {

                hostingUnit.Owner = hostingUnitList.FirstOrDefault().Owner;
                hostingUnit.HostingUnitKey = hostingUnitList.FirstOrDefault().HostingUnitKey;
                hostingUnit.HostingUnitName = hostingUnitList.FirstOrDefault().HostingUnitName;
                myBL.RemoveHostingUnit(hostingUnit.HostingUnitKey);
                MessageBox.Show("The hosting unit was removed successfuly", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                HostWindow w = new HostWindow(host);
                w.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Inner exception", "exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void UpdateOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                order = (Order)OrdersList.SelectedItem;
                //order.OrderKey = o.FirstOrDefault().OrderKey;
                //order.GuestRequestKey = o.FirstOrDefault().GuestRequestKey;
                //order.HostingUnitKey = o.FirstOrDefault().HostingUnitKey;
                //order.HostingUnitName = o.FirstOrDefault().HostingUnitName;
                //order.Status = o.FirstOrDefault().Status;
                //order.OrderDate = o.FirstOrDefault().OrderDate;
                //order.CreateDate = o.FirstOrDefault().CreateDate;
                myBL.UpdateOrder(order);
                Close();
                HostWindow window = new HostWindow(host);
                window.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Can't update the order", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private void mouseUpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                hostingUnit = (HostingUnit)HostingUnitsList.SelectedItem;
                //hostingUnit.Owner = hostingUnitList.FirstOrDefault().Owner;
                //hostingUnit.HostingUnitKey = hostingUnitList.FirstOrDefault().HostingUnitKey;
                //hostingUnit.HostingUnitName = hostingUnitList.FirstOrDefault().HostingUnitName;
                UpdateHostingUnit window = new UpdateHostingUnit(hostingUnit);
                Close();
                window.ShowDialog();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_NoteToWebsiteManager.Text=="")
            {
                MessageBox.Show("Please enter a review before submitting");
                return;
            }
            messages comment = new messages();
            comment.message = TextBox_NoteToWebsiteManager.Text;
            comment.date = DateTime.Now;
            myBL.AddCommentFromHost(comment);
            //Configuration.commentsFromHost.Add(comment);
            TextBox_NoteToWebsiteManager.Text = "";
            AddNewUnit.IsEnabled = true;
        }

        private void FinalizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Order> o = new List<Order>();
            List<Order> listFinalized = new List<Order>();
            List<Order> listNotFinalized = new List<Order>();
            var result1 = from item in myBL.GetHostingUnitCopy()
                          where item.Owner.HostKey == host.HostKey
                          select item;
            foreach (var item2 in result1)
            {
                foreach (var item3 in myBL.GetOrders())
                {
                    if (item2.HostingUnitKey == item3.HostingUnitKey)
                    {
                        o.Add(item3);
                    }
                }
            }

            foreach (var item4 in o)
            {
                if (item4.Status == BE.Enum.OrderStatus.ClosedDueToCostumerResponse)
                {
                    listFinalized.Add(item4);
                }
                else if (item4.Status == BE.Enum.OrderStatus.AnEmailWasSent)
                {
                    listNotFinalized.Add(item4);
                }
            }
            switch (FinalizeComboBox.SelectedIndex)
            {
                case 0:
                    this.OrdersList.ItemsSource = listFinalized;
                    CollectionView view0 = (CollectionView)CollectionViewSource.GetDefaultView(OrdersList.ItemsSource);
                    view0.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription0 = new PropertyGroupDescription("HostingUnitName");
                    view0.GroupDescriptions.Add(groupDescription0);
                    Update_order.IsEnabled = false;
                    break;
                case 1:
                    this.OrdersList.ItemsSource = listNotFinalized;
                    CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(OrdersList.ItemsSource);
                    view1.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription1 = new PropertyGroupDescription("HostingUnitName");
                    view1.GroupDescriptions.Add(groupDescription1);
                    break;
            }
        }

        private void mouseDoubleClickOrders(object sender, RoutedEventArgs e)
        {
            if (((Order)(OrdersList.SelectedItem)).Status == BE.Enum.OrderStatus.AnEmailWasSent)
            {
                Update_order.IsEnabled = true;
            }
        }
        private void BackToHomePage_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}