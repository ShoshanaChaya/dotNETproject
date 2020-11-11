using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : Idal
    {
        #region Guest Request
        void Idal.AddGuestRequest(GuestRequest guestRequest)
        {
            if ((DateTime.Now - guestRequest.EntryDate).TotalDays > 0)
            {
                guestRequest.Status = BE.Enum.CustomerOrderStatus.CannotAcceptTheOrder;
                throw new InvalidTimeZoneException("Dal: Entry date cannot be earlier than today");
            }
            if (!isValidEmail(guestRequest.MailAddress))
            {
                throw new InvalidDataException("Dal: The email address is invalid");
            }
            if (guestRequest.EntryDate.DayOfWeek == DayOfWeek.Saturday)
            {
                throw new InvalidDataException("Dal: The entry date can't be Shabbos");
            }
            if (guestRequest.ReleaseDate.DayOfWeek == DayOfWeek.Saturday)
            {
                throw new InvalidDataException("Dal: The release date can't be Shabbos");
            }
            //var result = from item in DataSource.guestRequests
            //             where item.MailAddress.CompareTo(guestRequest.MailAddress) == 0
            //             select item;
            //{
            //    if (result.Count() != 0)
            //}
            guestRequest.GuestRequestKey = ++Configuration.guestRequestKey;
            DataSource.guestRequests.Add(guestRequest.Copy<GuestRequest>());

        }

        bool isValidEmail(string mailAddress)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(mailAddress);
                return addr.Address == mailAddress;
            }
            catch
            {
                return false;
            }
        }

        public bool IdIsValid(string tz)
        {
            string id = tz;

            if (!System.Text.RegularExpressions.Regex.IsMatch(id, @"^\d{5,9}$"))
                return false;
            // number is too short - add leading 0000
            if (id.Length < 9)
            {
                while (id.Length < 9)
                {
                    id = '0' + id;
                }
            }
            //validate
            int mone = 0;
            int incNum;
            for (int i = 0; i < 9; i++)
            {
                incNum = Convert.ToInt32(id[i].ToString());
                incNum *= (i % 2) + 1;
                if (incNum > 9)
                    incNum -= 9;
                mone += incNum;
            }
            if (mone % 10 == 0)
                return true;
            else
                return false;
        }

        void Idal.UpdateGuestRequest(GuestRequest guestRequest)
        {
            var result = from item in DataSource.guestRequests
                         where item.GuestRequestKey == guestRequest.GuestRequestKey
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Couldn't update the request because it dosn't exist");
            }
            DataSource.guestRequests.Remove(result.Single());
            DataSource.guestRequests.Add(guestRequest.Copy<GuestRequest>());
        }
        void Idal.UpdateGuestRequestInfo(GuestRequest guestRequest)
        {
            //var result = from item in DataSource.guestRequests
            //             where item.Code.CompareTo(guestRequest.Code) == 0
            //             select item;
            int i = 0, j = DataSource.guestRequests.Count();
            foreach(var item1 in DataSource.guestRequests.Copy())
            {
                i++;
                if (item1.Code.CompareTo(guestRequest.Code) == 0)
                {
                    DataSource.guestRequests.Remove(DataSource.guestRequests.Find(x=>x.GuestRequestKey == item1.GuestRequestKey));
                    item1.PrivateName = guestRequest.PrivateName;
                    item1.FamilyName = guestRequest.FamilyName;
                    item1.MailAddress = guestRequest.MailAddress;
                    //item1.GuestRequestKey = guestRequest.GuestRequestKey;
                    DataSource.guestRequests.Add(item1.Copy<GuestRequest>());
                }
                if (i == j)
                {
                    return;
                }
            }
        }

        void Idal.UpdateGuestRequestCode(GuestRequest guestRequest)
        {
            int i = 0, j = DataSource.guestRequests.Count();
            foreach (var item1 in DataSource.guestRequests.Copy())
            {
                i++;
                if (item1.MailAddress.CompareTo(guestRequest.MailAddress) == 0)
                {                    
                    DataSource.guestRequests.Remove(DataSource.guestRequests.Find(x => x.GuestRequestKey == item1.GuestRequestKey));
                    item1.Code = guestRequest.Code;
                    DataSource.guestRequests.Add(item1.Copy<GuestRequest>());
                }
            }
            if (i == j)
            {
                return;
            }
        }

        void Idal.RemoveGuestRequest(GuestRequest guestRequest)
        {
            var result1 = from item1 in DataSource.guestRequests
                          where item1.GuestRequestKey == guestRequest.GuestRequestKey
                          select item1;
            if (result1.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't remove the guest request because it doesn't exist");
            }
            var result2 = from item2 in DataSource.orders
                          where item2.GuestRequestKey == guestRequest.GuestRequestKey
                          select item2;
            if (result2.Count() != 0)
            {
                foreach(var item in DataSource.orders)
                {
                    if (item.GuestRequestKey == guestRequest.GuestRequestKey)
                    {
                        DataSource.orders.Remove(item.Copy<Order>());
                    }
                    DateTime entryDate = guestRequest.EntryDate;
                    DateTime releaseDate = guestRequest.ReleaseDate;
                    int amountOfTotalDays = (int)(releaseDate - entryDate).TotalDays;
                    DateTime help = guestRequest.EntryDate;
                    int m = 0, j = 0;
                    for (DateTime i = help; j <= amountOfTotalDays; i = help.AddDays(m), j++)
                    {
                        m++;
                        DataSource.hostingUnits.Find(x => x.HostingUnitKey == item.HostingUnitKey).Diary[entryDate.Month - 1, i.Day - 1] = false;
                    }
                }
            }
            DataSource.guestRequests.Remove(result1.FirstOrDefault());
        }
        #endregion

        #region Hosting Unit
        void Idal.AddHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnit.HostingUnitKey = ++Configuration.hostingUnitKey;
            List<HostingUnit> hu = new List<HostingUnit>();
            if (DataSource.hostingUnits == null)
            {
                hu.Add(hostingUnit);
                DataSource.hostingUnits.Add(hostingUnit);
            }
            else
            {
                foreach (var item in DataSource.hostingUnits)
                {
                    if (item.HostingUnitKey == hostingUnit.HostingUnitKey)
                    {
                        hu.Add(item);
                    }
                }
                if (hu.Count() != 0)
                {
                    Console.WriteLine("Dal: Can't add the new host unit because it already exists");
                    throw new DuplicateNameException("Dal: Can't add the new host unit because it already exists");
                }
                if (!isValidEmail(hostingUnit.Owner.MailAddress))
                {
                    Console.WriteLine("Dal: The email address is invalid");
                    throw new InvalidDataException("Dal: The email address is invalid");
                }
                if (!IdIsValid(hostingUnit.Owner.HostKey))
                {
                    Console.WriteLine("Dal: The id number is invalid");
                    throw new InvalidDataException("Dal: The id number is invalid");
                }
                if (!isValidPhoneNumber(hostingUnit.Owner.PhoneNumber))
                {
                    Console.WriteLine("Dal: The phone number is invalid");
                    throw new InvalidDataException("Dal: The phone number is invalid");
                }
                DataSource.hostingUnits.Add(hostingUnit.Copy<HostingUnit>());
                //if (DataSource.hosts.Count() == 0)
                //{
                //   // hostingUnit.Owner.amountOfHostingUnits++;
                //    DataSource.hosts.Add(hostingUnit.Owner);
                //}
                //else
                //{
                    var result = from item in DataSource.hosts
                                 where item.HostKey == hostingUnit.Owner.HostKey
                                 select item;
                    if (result.Count() == 0)
                    {
                        hostingUnit.Owner.amountOfHostingUnits++;
                        DataSource.hosts.Add(hostingUnit.Owner);
                    }
                    else
                    {
                        DataSource.hosts.Find(x => x.HostKey == hostingUnit.Owner.HostKey).amountOfHostingUnits++;
                    }
                //}
            }
        }

        public bool isValidPhoneNumber(string number)
        {
            string phoneNumber = number.ToString();
            if (phoneNumber[1] != '5' && phoneNumber.Length != 9)
            {
                return false;
            }
            return true;
        }

        void Idal.RemoveHostingUnit(long hostingUnitKey)
        {
            var result = from item in DataSource.hostingUnits
                         where item.HostingUnitKey == hostingUnitKey
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't remove the host unit because it doesn't exist");
            }
            DataSource.hostingUnits.Remove(result.Single());
        }

        void Idal.UpdateHostingUnit(HostingUnit hostingUnit)
        {
            var result = from item in DataSource.hostingUnits
                         where item.HostingUnitKey == hostingUnit.HostingUnitKey
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't update the host unit because it doesn't exist");
            }
            DataSource.hostingUnits.Remove(result.Single());
            DataSource.hostingUnits.Add(hostingUnit.Copy<HostingUnit>());
        }

        void Idal.UpdateDiary()
        {
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            foreach (var item in DataSource.hostingUnits)
            {
                for (int i = 0; i < day; i++)
                {
                    item.Diary[month - 2, i] = false;
                }
            }
        }

        #endregion

        #region order
        void Idal.AddOrder(Order order)
        {
            if (DataSource.hostingUnits.Count() == 0 && DataSource.guestRequests.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Couldn't add the order because there aren't any hosting units or guests requests");
            }
            order.OrderKey = ++Configuration.orderKey;
            DataSource.orders.Add(order.Copy<Order>());
        }

        void Idal.UpdateOrder(Order order)
        {
            var result = from item in DataSource.orders
                         where order.OrderKey == item.OrderKey
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Couldn't update the order because the order doesn't exist");
            }
            Configuration.amountOfOrders++;
            DataSource.orders.Find(x => x.OrderKey == order.OrderKey).Status = BE.Enum.OrderStatus.ClosedDueToCostumerResponse;
            string firstName = DataSource.guestRequests.Find(x => x.GuestRequestKey == order.GuestRequestKey).PrivateName;
            string lastName = DataSource.guestRequests.Find(x => x.GuestRequestKey == order.GuestRequestKey).FamilyName;
            DateTime entryDate = DataSource.guestRequests.Find(x => x.GuestRequestKey == order.GuestRequestKey).EntryDate;
            DateTime releaseDate = DataSource.guestRequests.Find(x => x.GuestRequestKey == order.GuestRequestKey).ReleaseDate;
            foreach (var item in DataSource.guestRequests)
            {
                if (firstName.CompareTo(item.PrivateName) == 0 && lastName.CompareTo(item.FamilyName) == 0)
                {
                    if (entryDate == item.EntryDate && releaseDate == item.ReleaseDate)
                    {
                        item.Status = BE.Enum.CustomerOrderStatus.ClosedThroughWebsite;
                    }
                }
            }
        }

        void Idal.UpdateStatus()
        {
            foreach (var item in DataSource.orders)
            {
                if ((DateTime.Now - item.CreateDate).TotalDays > Configuration.amountOfDaysUntilOutOfDate && item.Status == BE.Enum.OrderStatus.AnEmailWasSent)
                {
                    item.Status = BE.Enum.OrderStatus.ClosedDueToCostumerLackOfResponse;
                }
                if ((DateTime.Now - item.CreateDate).TotalDays > Configuration.amountOfDaysUntilOutOfDate && item.Status == BE.Enum.OrderStatus.ClosedDueToCostumerBooked)
                {
                    var result = from item2 in DataSource.guestRequests
                                 where item2.GuestRequestKey == item.GuestRequestKey
                                 select item2;
                    DataSource.guestRequests.Remove(result.FirstOrDefault());
                    DataSource.orders.Remove(item);
                }
            }
        }
        #endregion

        #region Get Lists
        IEnumerable<HostingUnit> Idal.GetHostingUnitCopy()
        {
            var result = from item in DataSource.hostingUnits
                         select item.Copy<HostingUnit>();

            return result.AsEnumerable().OrderByDescending(s => s.HostingUnitName);
        }

        IEnumerable<GuestRequest> Idal.GetGuests()
        {
            return from item in DataSource.guestRequests
                   select item.Copy<GuestRequest>();
        }

        IEnumerable<Order> Idal.GetOrders()
        {
            return from item in DataSource.orders
                   select item.Copy<Order>();
        }

        IEnumerable<Host> Idal.GetHosts()
        {
            return from item in DataSource.hosts
                   select item.Copy<Host>();
        }
        List<BankBranch> Idal.GetBankBranches()
        {
            List<BankBranch> bankBranches = new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankNumber = ++Configuration.bankNumber,
                    BankName = "leumi",
                    BranchNumber = 13,
                    BranchAddress = "beit hadfus 7",
                    BranchCity = "jlm"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.bankNumber,
                    BankName = "leumi",
                    BranchNumber = 14,
                    BranchAddress = "beit hadfus 12",
                    BranchCity = "jlm"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.bankNumber,
                    BankName = "leumi",
                    BranchNumber = 13,
                    BranchAddress = "beit hadfus 13",
                    BranchCity = "jlm"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.bankNumber,
                    BankName = "leumi",
                    BranchNumber = 114,
                    BranchAddress = "beit hadfus 114",
                    BranchCity = "tlv"
                },
                new BankBranch()
                {
                    BankNumber = ++Configuration.bankNumber,
                    BankName = "leumi",
                    BranchNumber = 115,
                    BranchAddress = "beit hadfus 115",
                    BranchCity = "haifa"
                }
            };
            return bankBranches;
        }
        #endregion

        #region Host
        void Idal.AddHost(Host host)
        {
            if (!isValidEmail(host.MailAddress))
            {
                throw new InvalidDataException("Dal: can't add the host because the email address is invalid");
            }
            DataSource.hosts.Add(host.Copy<Host>());
        }

        void Idal.UpdateHost(Host host)
        {
            var result = from item in DataSource.hosts
                         where item.HostKey.CompareTo(host.HostKey)==0
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't update the host because it doesn't exist");
            }

            DataSource.hosts.Remove(result.Single());
            DataSource.hosts.Add(host.Copy<Host>());
        }

        void Idal.RemoveHost(Host host)
        {
            var result = from item1 in DataSource.hosts
                         where item1.HostKey.CompareTo(host.HostKey)==0
                         select item1;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't remove the host because it doesn't exist");
            }
            var result2 = from item2 in DataSource.hostingUnits
                          where item2.Owner.HostKey.CompareTo(host.HostKey)==0
                          select item2;
            foreach (var item3 in result2)
            {
                DataSource.hostingUnits.Remove(item3);
            }
            DataSource.hosts.Remove(result.Single());
        }
        #endregion


        #region Reviews
        void Idal.AddReviewFromGuest(messages m)
        {
            Configuration.reviewsFromGuests.Add(m);
        }

        void Idal.AddReviewToGuest(messages m)
        {
            Configuration.reviewsToGuests.Add(m);
        }

        void Idal.AddCommentFromHost(messages m)
        {
            Configuration.commentsFromHost.Add(m);
        }
        void Idal.RemoveReviewFromGuset(messages m)
        {
            Configuration.reviewsFromGuests.Remove(m);
        }

        void Idal.RemoveReviewToGuset(messages m)
        {
            Configuration.reviewsToGuests.Remove(m);
        }

        void Idal.RemoveCommentFromHost(messages m)
        {
            Configuration.commentsFromHost.Remove(m);
        }

        IEnumerable<messages> Idal.GetReviewsFromGuests()
        {
            return from item in Configuration.reviewsFromGuests
                   select item.Copy<messages>();
        }

        IEnumerable<messages> Idal.GetReviewsToGuests()
        {
            return from item in Configuration.reviewsToGuests
                   select item.Copy<messages>();
        }
        IEnumerable<messages> Idal.GetCommentsFromHosts()
        {
            return from item in Configuration.commentsFromHost
                   select item.Copy<messages>();
        }


        #endregion
    }
}
