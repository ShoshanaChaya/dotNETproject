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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MoreInfoHostingUnitWindow.xaml
    /// </summary>
    public partial class MoreInfoHostingUnitWindow : Window
    {
        IBL myBL = FactoryBL.getBL();
        HostingUnit hu;
        DateTime Start;
        DateTime End;
        public MoreInfoHostingUnitWindow(Object hostingUnit)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.hostingUnitGrid.DataContext = hostingUnit;
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(System.IO.Path.GetFullPath(myBL.getImagePath(hostingUnitNameTextBlock.Text)));
                bitmap.EndInit();
                hotelImage.Source = bitmap;
            }
            catch { } //doesnt have image
            long s = long.Parse(hostingUnitKeyTextBlock.Text);
            var result = from item in myBL.GetHostingUnitCopy()
                         where item.HostingUnitKey == s
                         select item;
            hu = result.FirstOrDefault();
            bool flag = false;
            DateTime temp = DateTime.Now;
            DateTime help = DateTime.Now;
            int k = 0;
            for (DateTime i = DateTime.Now; i < DateTime.Now.AddMonths(11); i = temp.AddDays(k))
            {
                k++;
                if (hu.Diary[i.Day - 1, i.Month - 1] == true && flag == false)
                {
                    Start = i;
                    flag = true;
                }
                if (hu.Diary[i.Day - 1, i.Month - 1] == true && i.Day != 31 && hu.Diary[i.Day, i.Month - 1] == false)
                {
                    End = i;
                    SetBlackOutDates(Start, End);
                    flag = false;
                }
                if (hu.Diary[i.Day - 1, i.Month - 1] == true && i.Day == 31 && hu.Diary[0, i.Month] == false)
                {
                    End = i;
                    SetBlackOutDates(Start, End);
                    flag = false;
                }
            }
            SetDisplayDates();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SetBlackOutDates(DateTime start, DateTime end)
        {
            Diary.BlackoutDates.Add(new CalendarDateRange(start, end));
        }
        private void SetDisplayDates()
        {
            Diary.DisplayDateStart = DateTime.Now;
            Diary.DisplayDateEnd = DateTime.Now.AddMonths(11);
        }
    }
}
