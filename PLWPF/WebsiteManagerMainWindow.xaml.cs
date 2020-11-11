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
    ///<summary>
    /// Interaction logic for WebsiteManagerMainWindow.xaml
    /// </summary>
    public partial class WebsiteManagerMainWindow : Window
    {
        BL.IBL myBL = FactoryBL.getBL();
        messages message = new messages();
        messages m = new messages();
        List<HostingUnit> UnitsList = new List<HostingUnit>();
        public WebsiteManagerMainWindow()
        {
            InitializeComponent();
            try
            {
                this.HostingUnitsList.ItemsSource = myBL.GetHostingUnitCopy();
            }
            catch
            {
                this.HostingUnitsList.ItemsSource = UnitsList;
                return;
            }
            // this.HostingUnitsList.ItemsSource = myBL.GetHostingUnitCopy();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Owner.fullName");
            view.GroupDescriptions.Add(groupDescription);
            IEnumerable<messages> list = new List<messages>();
            list = myBL.GetReviewsFromGuests();
            this.reviews.ItemsSource = list;
            this.reviewsOptions.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.reviewsOptions));
            this.options.ItemsSource = System.Enum.GetValues(typeof(BE.Enum.managerOptions));

            gainsShowTextBlock.Text = " " + Configuration.gains;
            wantedAreaShowTextBlock.Text = myBL.TheMostWantedArea();
            wantedTypeShowTextBlock.Text = myBL.TheMostWantedTypeOfHostingUnit();

        }
        private void mouseMoreInfoClick(object sender, RoutedEventArgs e)
        {
            if (options.SelectedIndex == 0)
            {
                MoreInfoHostWindow window = new MoreInfoHostWindow(HostingUnitsList.SelectedItem);
                window.Show();
            }
            if (options.SelectedIndex == 1)
            {
                MoreInfoOrderWindow window = new MoreInfoOrderWindow(HostingUnitsList.SelectedItem);
                window.Show();
            }
            if (options.SelectedIndex == 2 || options.SelectedIndex == 3)
            {
                MoreInfoGuestRequestWindow window = new MoreInfoGuestRequestWindow(HostingUnitsList.SelectedItem);
                window.Show();
            }
            if (options.SelectedIndex == 4)
            {
                MoreInfoHostingUnitWindow window = new MoreInfoHostingUnitWindow(HostingUnitsList.SelectedItem);
                window.Show();
            }
        }

        private void mouseDoubleClick(object sender, RoutedEventArgs e)
        {
            mouseMoreInfo.IsEnabled = true;
        }
        private void mouseDoubleClickReviews(object sender, RoutedEventArgs e)
        {
            if (reviewsOptions.SelectedIndex == 0)
            {
                AddReview.IsEnabled = true;
                RemoveReview.IsEnabled = false;
                RemoveComment.IsEnabled = false;
                var item = (sender as ListView).SelectedItem;
                m = (messages)(item);
                m = (messages)reviews.SelectedItem;
                //if (item != null)
                //{
                //    Configuration.reviewsToGuests.Find(x => x.message == m.message && x.date == m.date);
                //}
            }
            else if (reviewsOptions.SelectedIndex == 1)
            {
                RemoveReview.IsEnabled = true;
                AddReview.IsEnabled = false;
                RemoveComment.IsEnabled = false;
                var item = (sender as ListView).SelectedItem;
                m = (messages)(item);
                m = (messages)reviews.SelectedItem;
                //if (item != null)
                //{
                //    Configuration.reviewsToGuests.Find(x => x.message == m.message && x.date == m.date);
                //}
            }
            else if (reviewsOptions.SelectedIndex == 2)
            {
                RemoveComment.IsEnabled = true;
                RemoveReview.IsEnabled = false;
                AddReview.IsEnabled = false;
                var item = (sender as ListView).SelectedItem;
                m = (messages)(item);
                m = (messages)reviews.SelectedItem;
                //if (item != null)
                //{
                //    Configuration.reviewsToGuests.Find(x => x.message == m.message && x.date == m.date);
                //}
            }
        }
        private void addReviewButton(object sender, RoutedEventArgs e)
        {
            myBL.AddReviewToGuest(m);
            myBL.RemoveReviewFromGuset(m);
            //Configuration.reviewsToGuests.Add(m);
            //Configuration.reviewsFromGuests.Remove(m);
            Close();
            WebsiteManagerMainWindow window = new WebsiteManagerMainWindow();
            window.ShowDialog();
        }
        private void removeReviewButton(object sender, RoutedEventArgs e)
        {
            myBL.RemoveReviewToGuset(m);
            //Configuration.reviewsToGuests.Remove(m);
            Close();
            WebsiteManagerMainWindow window = new WebsiteManagerMainWindow();
            window.ShowDialog();
        }

        private void removeCommentButton(object sender, RoutedEventArgs e)
        {
            myBL.RemoveCommentFromHost(m);
            //Configuration.commentsFromHost.Remove(m);
            Close();
            WebsiteManagerMainWindow window = new WebsiteManagerMainWindow();
            window.ShowDialog();
        }

        private void addHostButton(object sender, RoutedEventArgs e)
        {
            AddHostWindow window = new AddHostWindow();
            window.ShowDialog();
        }

        private void ManagerOptionsComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            switch (options.SelectedIndex)
            {
                case 0:
                    IEnumerable<IGrouping<int, Host>> mainList0;
                    List<Host> group0 = new List<Host>();
                    mainList0 = myBL.GetAllHostsGruopByAmountOfHostingUnits();

                    foreach (var item0 in mainList0)
                    {
                        foreach (var item1 in item0)
                        {
                            group0.Add(item1);
                        }
                    }
                    this.HostingUnitsList.ItemsSource = group0;
                    headerKey.DisplayMemberBinding = new Binding("HostKey");
                    headerName.DisplayMemberBinding = new Binding("fullName");
                    CollectionView view0 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view0.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription0 = new PropertyGroupDescription("amountOfHostingUnits");
                    view0.GroupDescriptions.Add(groupDescription0);
                    break;
                case 1:
                    this.HostingUnitsList.ItemsSource = myBL.GetOrders();
                    headerKey.DisplayMemberBinding = new Binding("OrderKey");
                    headerName.DisplayMemberBinding = new Binding("HostingUnitName");
                    CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view1.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription1 = new PropertyGroupDescription("HostingUnitName");
                    view1.GroupDescriptions.Add(groupDescription1);
                    break;
                case 2:
                    // var result = myBL.GetsAllGuestRequestsGroupsByArea();
                    IEnumerable<IGrouping<BE.Enum.Area, GuestRequest>> mainList;
                    List<GuestRequest> group = new List<GuestRequest>();
                    try
                    {
                        mainList = myBL.GetsAllGuestRequestsGroupsByArea();
                        foreach (var item in mainList)
                        {
                            foreach (var item2 in item)
                            {
                                group.Add(item2);
                            }
                        }
                    }
                    catch
                    {

                    }
                    this.HostingUnitsList.ItemsSource = group;
                    headerKey.DisplayMemberBinding = new Binding("GuestRequestKey");
                    headerName.DisplayMemberBinding = new Binding("PrivateName");
                    CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view2.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription2 = new PropertyGroupDescription("Area");
                    view2.GroupDescriptions.Add(groupDescription2);
                    break;
                case 3:
                    IEnumerable<IGrouping<int, GuestRequest>> mainList3;
                    List<GuestRequest> group3 = new List<GuestRequest>();
                    //List<int> dis = new List<int>();
                    try
                    {
                        mainList3 = myBL.GetAllGuestsRequestsGropuByAmountOfPeople();

                        foreach (var item3 in mainList3)
                        {
                            foreach (var item2 in item3)
                            {
                                group3.Add(item2);
                            }
                        }
                    }
                    catch { }
                    this.HostingUnitsList.ItemsSource = group3;
                    headerKey.DisplayMemberBinding = new Binding("GuestRequestKey");
                    headerName.DisplayMemberBinding = new Binding("PrivateName");
                    CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view3.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription3 = new PropertyGroupDescription("amountOFPeople");
                    view3.GroupDescriptions.Add(groupDescription3);
                    break;
                case 4:
                    this.HostingUnitsList.ItemsSource = myBL.GetHostingUnitCopy();
                    headerKey.DisplayMemberBinding = new Binding("HostingUnitKey");
                    headerName.DisplayMemberBinding = new Binding("HostingUnitName");
                    CollectionView view4 = (CollectionView)CollectionViewSource.GetDefaultView(HostingUnitsList.ItemsSource);
                    view4.GroupDescriptions.Clear();
                    PropertyGroupDescription groupDescription4 = new PropertyGroupDescription("Owner.fullName");
                    view4.GroupDescriptions.Add(groupDescription4);
                    break;
            }
        }
        private void ReviewsOptionsComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            switch (reviewsOptions.SelectedIndex)
            {
                case 0:
                    this.reviews.ItemsSource = myBL.GetReviewsFromGuests();
                    break;
                case 1:
                    this.reviews.ItemsSource = myBL.GetReviewsToGuests();
                    break;
                case 2:
                    this.reviews.ItemsSource = myBL.GetCommentsFromHosts();
                    break;
            }
        }
        private void homePageClick(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}