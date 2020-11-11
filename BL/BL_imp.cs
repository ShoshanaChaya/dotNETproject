using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Net.Mail;
using DAL;
using BE;

namespace BL
{
    public class BL_imp : IBL
    {
        Idal d = FactoryDal.getDal();

        public void startThread()
        {
            var runAt = DateTime.Today + TimeSpan.FromHours(7);
            if (runAt < DateTime.Now)
            {
                ResetOrders();
            }
            else
            {
                var dueTime = runAt - DateTime.Now;
                var timer = new System.Threading.Timer(_ => ResetOrders(), null, dueTime, TimeSpan.Zero);
            }
        }

        public void ResetOrders()
        {
            Idal d = FactoryDal.getDal();
            //AmountOfOrders(d.UpdateStatus);
            d.UpdateStatus();
        }

        #region Guest Request
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            guestRequest.Status = BE.Enum.CustomerOrderStatus.Open;
            if ((guestRequest.ReleaseDate - guestRequest.EntryDate).TotalDays <= 1)
            {
                guestRequest.Status = BE.Enum.CustomerOrderStatus.CannotAcceptTheOrder;
                throw new InvalidTimeZoneException("Dal: Entry dates cannot be after release date");
            }
            bool flag = true;
            List<HostingUnit> hu = new List<HostingUnit>();
            foreach (var item in d.GetHostingUnitCopy())
            {
                if (item.Area == guestRequest.Area && item.ChildrensAttractions == guestRequest.ChildrensAttractions && item.Garden == guestRequest.Garden && item.Pool == guestRequest.Pool && item.Jacuzzi == guestRequest.Jacuzzi && item.Owner.CollectionClearance == true)
                {
                    for (DateTime j = guestRequest.EntryDate; j < guestRequest.ReleaseDate; j = j.AddDays(1))
                    {
                        if (item.Diary[j.Day - 1, j.Month - 1] == true)
                        {
                            flag = false;
                        }
                    }
                }
                else
                {
                    flag = false;
                }
                if (flag == true)
                {
                    hu.Add(item);
                }
                flag = true;
            }
            if (hu.Count() == 0)
            {
                guestRequest.Status = BE.Enum.CustomerOrderStatus.CannotAcceptTheOrder;
                //throw new KeyNotFoundException("BL: There isn't a host which is acceptable to the request");
            }
            if (!InvalidDates(guestRequest))
            {
                throw new InvalidDataException("BL: The dates are further than 11 months from now");
            }
            try
            {
                guestRequest.RegistrationDate = DateTime.Now;
                guestRequest.amountOFPeople = guestRequest.Adults + guestRequest.Children;
                guestRequest.amount = (int)(guestRequest.ReleaseDate - guestRequest.EntryDate).TotalDays;
                d.AddGuestRequest(guestRequest);
                foreach (var item2 in hu)
                {
                    Order order = new Order();
                    order.HostingUnitKey = item2.HostingUnitKey;
                    order.GuestRequestKey = guestRequest.GuestRequestKey;
                    AddOrder(order);
                }
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public bool InvalidDates(GuestRequest guestRequest)
        {
            DateTime outOfDate = (DateTime.Now).AddMonths(11);
            if (outOfDate < guestRequest.ReleaseDate)
            {
                return false;
            }
            return true;
        }

        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            Idal d = FactoryDal.getDal();
            guestRequest.Status = BE.Enum.CustomerOrderStatus.ClosedThroughWebsite;
            try
            {
                d.UpdateGuestRequest(guestRequest);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public void UpdateGuestRequestInfo(GuestRequest guestRequest)
        {
            try
            {
                d.UpdateGuestRequestInfo(guestRequest);
            }
            catch
            {
                throw new InvalidDataException("BL: error in updating guest information");
            }
        }

        public void UpdateGuestRequestCode(GuestRequest guestRequest)
        {
            try
            {
                d.UpdateGuestRequestCode(guestRequest);
            }
            catch
            {
                throw new InvalidDataException("BL: error in updating guest code");
            }
        }

        public void RemoveGuestRequest(GuestRequest guestRequest)
        {
            Idal d = FactoryDal.getDal();
            try
            {
                d.RemoveGuestRequest(guestRequest);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public string TheMostWantedArea()
        {
            Idal d = FactoryDal.getDal();
            int North = 0, South = 0, West = 0, East = 0, Center = 0;
            foreach (var item in d.GetGuests())
            {
                if (item.Area == BE.Enum.Area.North)
                {
                    North++;
                }
                if (item.Area == BE.Enum.Area.South)
                {
                    South++;
                }
                if (item.Area == BE.Enum.Area.West)
                {
                    West++;
                }
                if (item.Area == BE.Enum.Area.East)
                {
                    East++;
                }
                if (item.Area == BE.Enum.Area.Center)
                {
                    Center++;
                }
            }
            int most = North;
            string name = "North";
            if (South > most)
            {
                most = South;
                name = "South";
            }
            if (West > most)
            {
                most = West;
                name = "West";
            }
            if (East > most)
            {
                most = East;
                name = "East";
            }
            if (Center > most)
            {
                most = Center;
                name = "Center";
            }
            return name;
        }

        public string TheMostWantedTypeOfHostingUnit()
        {
            Idal d = FactoryDal.getDal();
            int Tzimer = 0, AccommodationApartment = 0, Hotel = 0, Tent = 0;
            foreach (var item in d.GetGuests())
            {
                if (item.Type == BE.Enum.ResortType.Tzimer)
                {
                    Tzimer++;
                }
                if (item.Type == BE.Enum.ResortType.AccommodationApartment)
                {
                    AccommodationApartment++;
                }
                if (item.Type == BE.Enum.ResortType.Hotel)
                {
                    Hotel++;
                }
                if (item.Type == BE.Enum.ResortType.Tent)
                {
                    Tent++;
                }
            }
            int most = Tzimer;
            string name = "Tzimer";
            if (AccommodationApartment > most)
            {
                most = AccommodationApartment;
                name = "AccommodationApartment";
            }
            if (Hotel > most)
            {
                most = Hotel;
                name = "Hotel";
            }
            if (Tent > most)
            {
                most = Tent;
                name = "Tent";
            }
            return name;
        }

        public int AmountOfPeople(GuestRequest guestRequest)
        {
            return guestRequest.Adults + guestRequest.Children;
        }

        public int amountOfVacationDays(GuestRequest guestRequest)
        {
            return (int)(guestRequest.ReleaseDate - guestRequest.EntryDate).TotalDays;
        }
        #endregion

        #region Hosting Unit
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            Idal d = FactoryDal.getDal();
            try
            {
                hostingUnit.Diary = new bool[12, 31];
                hostingUnit.Owner.amountOfHostingUnits++;
                d.AddHostingUnit(hostingUnit);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public void RemoveHostingUnit(long hostingUnitKey)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetOrders()
                         where item.HostingUnitKey == hostingUnitKey
                         select item;
            if (result.Count() == 0)
            {
                foreach (var item in result)
                {
                    if (item.Status == BE.Enum.OrderStatus.AnEmailWasSent)
                    {
                        throw new InvalidDataException("BL: Can't remove the host unit because it's currnetly open to an order");
                    }
                }
            }
            try
            {
                d.RemoveHostingUnit(hostingUnitKey);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetOrders()
                         where item.HostingUnitKey == hostingUnit.HostingUnitKey
                         select item;
            if (hostingUnit.Owner.CollectionClearance && result.Count() != 0)
            {
                throw new InvalidDataException("BL: Can't update the host unit because it's currnetly open to an order");
            }
            d.UpdateHostingUnit(hostingUnit);
        }

        public void UpdateDiary()
        {
            Idal d = FactoryDal.getDal();
            d.UpdateDiary();
        }
        #endregion

        #region Order
        public void AddOrder(Order order)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetGuests()
                         let help = order.GuestRequestKey
                         where item.GuestRequestKey == help
                         select item;
            //result.FirstOrDefault().amount= (int)(result.Single().ReleaseDate - result.Single().EntryDate).TotalDays;
            // int amountOfTotalDays = (int)(result.Single().ReleaseDate - result.Single().EntryDate).TotalDays;
            order.OrderDate = DateTime.Now;
            orderEmail.GuestRequestKey = order.GuestRequestKey;
            orderEmail.HostingUnitKey = order.HostingUnitKey;
            var result0 = from item in d.GetHostingUnitCopy()
                          where item.HostingUnitKey == order.HostingUnitKey
                          select item;
            if (result0.FirstOrDefault().Owner.CollectionClearance)
            {
                //try
                //{
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += sendAnEmail;
                System.Threading.Thread.Sleep(2000);
                worker.RunWorkerAsync();
                order.Status = BE.Enum.OrderStatus.AnEmailWasSent;
                Console.WriteLine("An email was sent");
                d.AddOrder(order);
                orderEmail = order;
                //}
                //catch
                //{
                //    throw new InvalidProgramException("Thre was a problem sending the eamil");
                //}
            }
            else
            {
                throw new DataException("The host didn't authrize the collection clearance, therefor can't add orders to his units");
            }
        }
        public Order orderEmail = new Order();

        private void sendAnEmail(object sender, DoWorkEventArgs e)
        {
            Idal d = FactoryDal.getDal();
            HostingUnit hostingUnit;
            var result = from item in d.GetHostingUnitCopy()
                         where item.HostingUnitKey == orderEmail.HostingUnitKey
                         select item;
            hostingUnit = result.FirstOrDefault();
            GuestRequest guestRequest;
            var result2 = from item in d.GetGuests()
                          where item.GuestRequestKey == orderEmail.GuestRequestKey
                          select item;
            guestRequest = result2.FirstOrDefault();
            MailMessage mail = new MailMessage();
            mail.To.Add(guestRequest.MailAddress);
            mail.From = new MailAddress("websitemanager1234@gmail.com");
            mail.Subject = "We found you a match for your request";
            mail.Body = "Hello " + guestRequest.PrivateName + " " + guestRequest.FamilyName + ",<br>" + "We are glad to tell you we found a match for your request." + "<br>" + "The hosting unit that was found matched is: " + hostingUnit.HostingUnitName + " number:" + hostingUnit.HostingUnitKey + "." + "Thank you for ordering through our website" + "<br>" + "The website team";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("websitemanager1234@gmail.com", "website1234");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrder(Order order)
        {
            if (order.Status == BE.Enum.OrderStatus.AnEmailWasSent)
            {
                Idal d = FactoryDal.getDal();
                var result2 = from item in d.GetGuests()
                              let help = order.GuestRequestKey
                              where item.GuestRequestKey == help
                              select (GuestRequest)item;
                int amountOfTotalDays = (int)(result2.Single().ReleaseDate - result2.Single().EntryDate).TotalDays;
                Configuration.gains = Configuration.gains + amountOfTotalDays * Configuration.fee;
                bool flag = true;
                DateTime temp = result2.Single().EntryDate;
                int k = result2.Single().EntryDate.Month;
                int t = 0;
                for (DateTime l = result2.Single().EntryDate; l < result2.Single().ReleaseDate; l = l.AddDays(1))
                {
                    if (l.Day != 31 && (d.GetHostingUnitCopy().First(x => x.HostingUnitKey == order.HostingUnitKey)).Diary[l.Day - 1, k - 1] == true)
                    {
                        flag = false;
                    }
                    if (l.Day == 31 && (d.GetHostingUnitCopy().First(x => x.HostingUnitKey == order.HostingUnitKey)).Diary[0, k] == true)
                    {
                        t++;
                        flag = false;
                        l = temp.AddDays(k);
                        k++;
                    }
                }
                if (flag == false)
                {
                    throw new InvalidDataException("BL: Couldn't update the dates because the dates were already occupied");
                }
                d.GetGuests().First(x => x.GuestRequestKey == order.GuestRequestKey).Status = BE.Enum.CustomerOrderStatus.ClosedThroughWebsite;
                HostingUnit hostingUnit = d.GetHostingUnitCopy().First(x => x.HostingUnitKey == order.HostingUnitKey);
                DateTime temp2 = result2.Single().EntryDate;
                int j = 0;
                for (DateTime n = temp; j <= amountOfTotalDays; n = n.AddDays(1), j++) // updates the array with the required days
                {
                    hostingUnit.Diary[n.Day - 1, k - 1] = true;
                }
                d.UpdateHostingUnit(hostingUnit);
                d.GetOrders().First(x => x.OrderKey == order.OrderKey).Status = BE.Enum.OrderStatus.ClosedDueToCostumerResponse;
                try
                {
                    d.UpdateOrder(d.GetOrders().First(x => x.OrderKey == order.OrderKey));
                }
                catch (System.Exception)
                {
                    throw new DataException("BL: Cannot update the order due to an error in the information");
                }
            }
            else
            {
                throw new InvalidDataException("BL: Can't update the order because it's already closed");
            }
        }

        public void UpdateStatus()
        {
            Idal d = FactoryDal.getDal();
            d.UpdateStatus();
        }
        #endregion

        #region Get Lists
        public IEnumerable<HostingUnit> GetHostingUnitCopy()
        {
            Idal d = FactoryDal.getDal();
            if (d.GetHostingUnitCopy().Count() == 0)
            {
                throw new ArgumentNullException("BL: Couldn't return hosting unit because its list is empty");
            }
            return d.GetHostingUnitCopy();
        }

        public IEnumerable<GuestRequest> GetGuests()
        {
            Idal d = FactoryDal.getDal();
            if (d.GetGuests().Count() == 0)
            {
                throw new ArgumentNullException("BL: Couldn't return guests request because its list is empty");
            }
            return d.GetGuests();
        }

        public IEnumerable<Order> GetOrders()
        {
            Idal d = FactoryDal.getDal();
            if (d.GetOrders() == null)
            {
                throw new ArgumentNullException("BL: Couldn't return guests request because its list is empty");
            }
            return d.GetOrders();
        }

        public List<BankBranch> GetBankBranches()
        {

            Idal d = FactoryDal.getDal();
            if (d.GetBankBranches().Count() == 0)
            {
                throw new ArgumentNullException("BL: Couldn't return bank branches because its list is empty");
            }
            return d.GetBankBranches();
        }

        public IEnumerable<HostingUnit> GetAvailableDays(DateTime date, int numOfDays)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> hostingUnits = new List<HostingUnit>();
            DateTime releaseDate = date.AddDays(numOfDays);
            foreach (var item in d.GetHostingUnitCopy())
            {
                bool flag = true;
                for (int j = date.Day, k = date.Month; j < releaseDate.Day; j++)
                {
                    if (j != 31 && item.Diary[k - 1, j - 1] == true)
                    {
                        flag = false;
                    }
                    if (j == 31 && item.Diary[k, 0] == true)
                    {
                        flag = false;
                        j = 0;
                        k++;
                    }
                }
                if (flag)
                {
                    hostingUnits.Add(item);
                }
            }
            if (hostingUnits.Count() == 0)
            {
                throw new ArgumentNullException("BL: There isn't any hosting units which are available in the required dates");
            }
            return hostingUnits;
        }

        public int AmountOfOrdersPerDay(HostingUnit hostingUnit)
        {
            return Configuration.amountOfOrders;
        }

        public IEnumerable<Host> GetHosts()
        {
            Idal d = FactoryDal.getDal();
            return d.GetHosts().OrderBy(x => x.HostKey);
        }
        #endregion

        #region Function Which Calculate
        public int AmountOfDaysBetween(params DateTime[] dates)
        {
            if (dates.Count() > 2)
            {
                throw new NotImplementedException("BL: the function received more than two parameters");
            }
            if (dates.Count() == 0)
            {
                throw new ArgumentNullException("BL: the function didn't received any parameters");
            }
            if (dates.Count() == 2)
            {
                if (dates[0] > dates[1])
                {
                    TimeSpan t = dates[0] - dates[1];
                    return (int)t.TotalDays;
                }
                else
                {
                    TimeSpan t = dates[1] - dates[0];
                    return (int)t.TotalDays;
                }
            }
            DateTime temp = DateTime.Now;
            if (dates[0] > temp)
            {
                throw new InvalidTimeZoneException("BL: the date that was received is later than today");
            }
            TimeSpan help = temp - dates[0];
            return (int)help.TotalDays;
        }

        public IEnumerable<Order> AmountOfOrders(int numOfDays)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetOrders()
                         let help1 = DateTime.Now - item.CreateDate
                         let help2 = DateTime.Now - item.OrderDate
                         where (help1.TotalDays >= numOfDays || help2.TotalDays >= numOfDays)
                         select item;
            if (result.Count() == 0)
            {
                throw new InvalidTimeZoneException("BL: There aren't any orders that are out of date");
            }
            return result;
        }

        public IEnumerable<GuestRequest> CustomerDemands(Func<GuestRequest, bool> predicat)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetGuests()
                         where predicat(item) == true
                         select item;
            if (result.Count() == 0)
            {
                throw new NotSupportedException("BL: There aren't any guest requests which have the attribute");
            }
            return result;
        }

        public int AmountOfInvetations(GuestRequest guestRequest)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetOrders()
                         let temp = guestRequest.GuestRequestKey
                         where item.GuestRequestKey == temp
                         select item;
            if (result.Count() == 0)
            {
                throw new NotImplementedException("BL: There aren't any orders for the guest request");
            }
            return result.Count();
        }

        public int AmountOfFinalizedOrders(HostingUnit hostingUnit)
        {
            Idal d = FactoryDal.getDal();
            int counter = 0;
            foreach (var item1 in d.GetOrders())
            {
                foreach (var item2 in d.GetHostingUnitCopy())
                {
                    if (item1.HostingUnitKey == item2.HostingUnitKey && (item1.Status == BE.Enum.OrderStatus.ClosedDueToCostumerResponse || item1.Status == BE.Enum.OrderStatus.AnEmailWasSent))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        #endregion

        #region Grouping
        public IEnumerable<IGrouping<BE.Enum.Area, GuestRequest>> GetsAllGuestRequestsGroupsByArea()
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetGuests()
                         group item by item.Area into g
                         select g;
            if (result.Count() == 0)
            {
                throw new ArgumentNullException("BL: Couldn't return the groups because there aren't any guests requests");
            }
            return result;
        }

        public IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestsRequestsGropuByAmountOfPeople()
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetGuests()
                         let amount = item.Adults + item.Children
                         group item by amount into g
                         select g;
            if (result.Count() == 0)
            {
                throw new ArgumentNullException("BL: Couldn't return the groups because there aren't any guests requests");
            }
            return result;
        }

        public IEnumerable<IGrouping<int, Host>> GetAllHostsGruopByAmountOfHostingUnits()
        {
            Idal d = FactoryDal.getDal();
            return from item in d.GetHosts()
                   let amount = item.amountOfHostingUnits
                   group item by amount into g
                   select g;
        }

        public List<HostingUnit> GetAllHostingUnitsGroupByArea(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.Area.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }

        public List<HostingUnit> GetAllHostingUnitsGroupByType(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.Type.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }
        public List<HostingUnit> GetAllHostingUnitsGroupByPool(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.Pool.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }
        public List<HostingUnit> GetAllHostingUnitsGroupByJacuzzi(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.Jacuzzi.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }
        public List<HostingUnit> GetAllHostingUnitsGroupByGarden(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.Garden.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }
        public List<HostingUnit> GetAllHostingUnitsGroupByChildrenAttractions(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.ChildrensAttractions.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }
        public List<HostingUnit> GetAllHostingUnitsGroupBySpooky(string a, List<HostingUnit> list)
        {
            Idal d = FactoryDal.getDal();
            List<HostingUnit> l = new List<HostingUnit>();
            foreach (var item in list)
            {
                if (item.Spooky.ToString() == a)
                {
                    l.Add(item);
                }
            }
            return l;
        }

        public IEnumerable<IGrouping<Func<HostingUnit, T>, HostingUnit>> orderByHostingUnit<T>(List<HostingUnit> list, Func<HostingUnit, T> predicat = null)
        {
            if (predicat != null)
            {
                return from item in list
                       group item by (predicat);
            }
            else
            {
                throw new InvalidDataException("BL: Didn't choose how to group");
            }
        }
        #endregion

        #region Host
        public void AddHost(Host host)
        {
            Idal d = FactoryDal.getDal();
            var result = from item in d.GetHosts()
                         let key = host.HostKey
                         where item.HostKey.CompareTo(key) == 0
                         select item;
            if (result.Count() != 0)
            {
                throw new DuplicateNameException("BL: the host already exists");
            }
            try
            {
                host.fullName = host.PrivateName + " " + host.FamilyName;
                host.amountOfHostingUnits = 0;
                d.AddHost(host);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public void UpdateHost(Host host)
        {
            Idal d = FactoryDal.getDal();
            try
            {
                d.UpdateHost(host);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public void RemoveHost(Host host)
        {
            Idal d = FactoryDal.getDal();
            var result = from item1 in d.GetHostingUnitCopy()//result saves all the hosting units that belong the the spesific host
                         where item1.Owner.HostKey.CompareTo(host.HostKey) == 0
                         select item1;
            foreach (var item2 in result)
            {
                var result2 = from item in d.GetOrders()
                              where (item.HostingUnitKey.CompareTo(item2.HostingUnitKey) == 0 && (item.Status == BE.Enum.OrderStatus.AnEmailWasSent || item.Status == BE.Enum.OrderStatus.ClosedDueToCostumerBooked))
                              select item;
                if (result2.Count() != 0)
                {
                    throw new InvalidDataException("BL: Can't remove the host because one of its units is currnetly open to an order");
                }
            }
            try
            {
                d.RemoveHost(host);
            }
            catch (System.Exception exp)
            {
                throw exp;
            }
        }

        public string getFullName(Host host)
        {
            string name = host.PrivateName + " " + host.FamilyName;
            return name;
        }
        #endregion

        #region Images
        public void AddImage(string name, string newImagePath)
        {
            string photoPath = @"..\..\..\Images\" + name + @".jpg";
            (File.Create(photoPath)).Close();
            System.IO.File.Copy(newImagePath, photoPath, true);
        }
        public string getImagePath(string name)
        {
            string tempSource = System.IO.Path.GetTempFileName();
            File.Copy(@"..\..\..\Images\" + name + @".jpg", tempSource, true);
            return tempSource;
        }
        #endregion

        #region Reviews

        public void AddReviewFromGuest(messages m)
        {
            Idal d = FactoryDal.getDal();
            d.AddReviewFromGuest(m);
        }

        public void AddReviewToGuest(messages m)
        {
            Idal d = FactoryDal.getDal();
            d.AddReviewToGuest(m);
        }

        public void AddCommentFromHost(messages m)
        {
            Idal d = FactoryDal.getDal();
            d.AddCommentFromHost(m);
        }

        public void RemoveReviewToGuset(messages m)
        {
            Idal d = FactoryDal.getDal();
            d.RemoveReviewToGuset(m);
        }

        public void RemoveReviewFromGuset(messages m)
        {
            Idal d = FactoryDal.getDal();
            d.RemoveReviewFromGuset(m);
        }

        public void RemoveCommentFromHost(messages m)
        {
            Idal d = FactoryDal.getDal();
            d.RemoveCommentFromHost(m);
        }

        public IEnumerable<messages> GetReviewsFromGuests()
        {
            Idal d = FactoryDal.getDal();
            return d.GetReviewsFromGuests();
        }

        public IEnumerable<messages> GetReviewsToGuests()
        {
            Idal d = FactoryDal.getDal();
            return d.GetReviewsToGuests();
        }

        public IEnumerable<messages> GetCommentsFromHosts()
        {
            Idal d = FactoryDal.getDal();
            return d.GetCommentsFromHosts();
        }
        #endregion

    }
}
