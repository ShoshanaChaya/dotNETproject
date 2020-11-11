using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data;
using System.Net;
using BE;
using DS;

namespace DAL
{
    [Serializable]
    public class Dal_XML_imp : Idal
    {
        XElement HostRoot;
        // XElement HostingUnitRoot;
        XElement GuestRequestRoot;
        XElement OrderRoot;
        XElement BankBranchRoot;
        XElement ConfigRoot;
        XElement reviewsFromGuestsRoot;
        XElement commentsFromHostRoot;
        XElement reviewsToGuestsRoot;
        List<BankBranch> branches = new List<BankBranch>();
        List<messages> reviewsFromGuests = new List<messages>();
        List<messages> commentsFromHost = new List<messages>();
        List<messages> reviewsToGuests = new List<messages>();
        BackgroundWorker worker = new BackgroundWorker();

        const string HostPath = @"HostXml.xml";
        private const string HostingUnitPath = @"HostingUnitXml.xml";
        const string GuestRequestPath = @"GuestRequestXml.xml";
        const string OrderPath = @"OrderXml.xml";
        const string BankBranchPath = @"BankBranchXml.xml";
        const string ConfigPath = @"ConfigXml.xml";
        const string reviewsFromGuestsPath = @"reviewsFromGuestsXml.xml";
        const string commentsFromHostPath = @"commentsFromHostXml.xml";
        const string reviewsToGuestsPath = @"reviewsToGuestsXml.xml";


        const string xmlLocalPath = @"atm.xml";

        public Dal_XML_imp()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    ConfigRoot = new XElement("Config");
                    XElement hostingUnitKey = new XElement("HostingUnitKey", "10000000");
                    XElement guestRequestKey = new XElement("GuestRequestKey", "10000000");
                    XElement orderKey = new XElement("OrderKey", "10000000");
                    XElement bankNumber = new XElement("BankNumber", "10000000");
                    XElement fee = new XElement("fee", "10");
                    XElement amountOfDaysUntilOutOfDate = new XElement("amountOfDaysUntilOutOfDate", "30");
                    XElement gains = new XElement("gains", "0");
                    XElement amountOfOrders = new XElement("amountOfOrders", "0");
                    XElement password = new XElement("password", "1234");
                    ConfigRoot = new XElement("Config", hostingUnitKey, guestRequestKey, orderKey, bankNumber, fee, amountOfDaysUntilOutOfDate, gains, amountOfOrders, password);
                    ConfigRoot.Save(ConfigPath);
                }
                if (!File.Exists(HostPath))
                {
                    CreateFilesHost();
                }
                else
                {
                    LoadDataHost();
                }

                if (!File.Exists(HostingUnitPath))
                {
                    CreateFilesHostingUnit();
                }


                if (!File.Exists(GuestRequestPath))
                {
                    CreateFilesGuestRequest();
                }
                else
                {
                    LoadDataGuestRequest();
                }

                if (!File.Exists(OrderPath))
                {
                    CreateFilesOrder();
                }
                else
                {
                    LoadDataOrder();
                }

                if (!File.Exists(BankBranchPath))
                {
                    CreateFilesBankBranch();
                }
                else
                {
                    LoadDataBankBranch();
                }

                if (!File.Exists(reviewsFromGuestsPath))
                {
                    CreateFilesreviewsFromGuests();
                }
                else
                {
                    LoadDatareviewsFromGuests();
                }

                if (!File.Exists(reviewsToGuestsPath))
                {
                    CreateFilesreviewsToGuests();
                }
                else
                {
                    LoadDatareviewsToGuests();
                }

                if (!File.Exists(commentsFromHostPath))
                {
                    CreateFilescommentsFromHost();
                }
                else
                {
                    LoadDatacommentsFromHost();
                }

                BankThread();
                //worker.DoWork += BankThread;
                //worker.RunWorkerAsync();
                ConfigRoot = XElement.Load(ConfigPath);

            }
            catch
            {
                throw new FileLoadException("Couldn't load the file");
            }

            //saveListToXML<HostingUnit>(DataSource.hostingUnits, HostingUnitPath);
        }
        private void BankThread()
        {
            if (BankBranchRoot.IsEmpty == true)
            {
                const string xmlLocalPath = BankBranchPath;
                WebClient wc = new WebClient();
                try
                {
                    string xmlServerPath =
                   @"https://www.boi.org.il/en/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/snifim_en.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                catch (Exception)
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                finally
                {
                    wc.Dispose();
                }
                if (!File.Exists(xmlLocalPath))
                {
                    throw new FileLoadException("could't upload the file");
                }
                BankBranchRoot = XElement.Load(BankBranchPath);
                foreach (var item in BankBranchRoot.Elements())
                {
                    branches.Add(new BankBranch()
                    {
                        BranchAddress = item.Element("Address").Value,
                        BankNumber = int.Parse(item.Element("Bank_Code").Value),
                        BankName = item.Element("Bank_Name").Value,
                        BranchCity = item.Element("City").Value,
                        BranchNumber = int.Parse(item.Element("Branch_Code").Value)
                    });
                }
            }
            //BankBranchRoot = XElement.Load(BankBranchPath);
            //branches = loadListFromXML<BankBranch>(BankBranchPath);
            //BankBranchRoot = XElement.Load(BankBranchPath);
            else
            {
                foreach (var item in BankBranchRoot.Elements())
                {
                    branches.Add(new BankBranch()
                    {
                        BranchAddress = item.Element("Address").Value,
                        BankNumber = int.Parse(item.Element("Bank_Code").Value),
                        BankName = item.Element("Bank_Name").Value,
                        BranchCity = item.Element("City").Value,
                        BranchNumber = int.Parse(item.Element("Branch_Code").Value)
                    });
                }
            }

            branches = branches.Distinct<BankBranch>().ToList();
            //BankBranchRoot.Save(BankBranchPath);
        }

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
        #region CreateFiles
        private void CreateFilesHost()
        {
            HostRoot = new XElement("hosts");
            HostRoot.Save(HostPath);
        }
        private void CreateFilesHostingUnit()
        {
            List<HostingUnit> hostingUnits = new List<HostingUnit>();
            SaveToXML<List<HostingUnit>>(hostingUnits, HostingUnitPath);
            //HostingUnitRoot = new XElement("hostsingUnits");
            //HostingUnitRoot.Save(HostingUnitPath);
        }
        private void CreateFilesGuestRequest()
        {
            GuestRequestRoot = new XElement("guestRequest");
            GuestRequestRoot.Save(GuestRequestPath);
        }
        private void CreateFilesOrder()
        {
            OrderRoot = new XElement("order");
            OrderRoot.Save(OrderPath);
        }

        private void CreateFilesBankBranch()
        {
            BankBranchRoot = new XElement("BRANCHES");
            // branches = loadListFromXML<BankBranch>(BankBranchPath);
            BankBranchRoot.Save(BankBranchPath);
        }

        private void CreateFilesreviewsFromGuests()
        {
            reviewsFromGuestsRoot = new XElement("reviews");
            reviewsFromGuestsRoot.Save(reviewsFromGuestsPath);
        }

        private void CreateFilesreviewsToGuests()
        {
            reviewsToGuestsRoot = new XElement("reviews");
            reviewsToGuestsRoot.Save(reviewsToGuestsPath);
        }

        private void CreateFilescommentsFromHost()
        {
            commentsFromHostRoot = new XElement("comments");
            commentsFromHostRoot.Save(commentsFromHostPath);
        }
        #endregion

        #region LoadData
        private void LoadDataHost()
        {
            try
            {
                HostRoot = XElement.Load(HostPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataHostingUnit()
        {
            try
            {
                LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataGuestRequest()
        {
            try
            {
                GuestRequestRoot = XElement.Load(GuestRequestPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDatareviewsToGuests()
        {
            try
            {
                reviewsToGuestsRoot = XElement.Load(reviewsToGuestsPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDatacommentsFromHost()
        {
            try
            {
                commentsFromHostRoot = XElement.Load(commentsFromHostPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void LoadDataOrder()
        {
            try
            {
                OrderRoot = XElement.Load(OrderPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void LoadDatareviewsFromGuests()
        {
            try
            {
                reviewsFromGuestsRoot = XElement.Load(reviewsFromGuestsPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void LoadDataBankBranch()
        {
            try
            {
                //double i = 0;

                // branches = loadListFromXML<BankBranch>(BankBranchPath);
                //BankBranchRoot.Save(BankBranchPath);
                BankBranchRoot = XElement.Load(BankBranchPath);
                // while (i < 100000000)
                //i++;

            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataConfig()
        {
            try
            {
                ConfigRoot = XElement.Load(ConfigPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        #endregion

        #region Get
        public Host GetHost(long hostKey)
        {
            LoadDataHost();
            Host host;
            try
            {
                host = (from item in HostRoot.Elements()
                        where long.Parse(item.Element("HostKey").Value) == hostKey
                        select new Host()
                        {
                            HostKey = item.Element("HostKey").Value,
                            PrivateName = item.Element("PrivateName").Value,
                            FamilyName = item.Element("FamilyName").Value,
                            PhoneNumber = item.Element("PhoneNumber").Value,
                            MailAddress = item.Element("MailAddress").Value,
                            BankAccountNumber = long.Parse(item.Element("BankAccountNumber").Value),
                            CollectionClearance = bool.Parse(item.Element("CollectionClearance").Value),
                            amountOfHostingUnits = int.Parse(item.Element("amountOfHostingUnits").Value)
                        }).FirstOrDefault();
            }
            catch
            {
                host = null;
            }
            return host;
        }

        public GuestRequest GetGuestRequest(long key)
        {
            LoadDataGuestRequest();
            GuestRequest guestRequest;
            try
            {
                guestRequest = (from item in GuestRequestRoot.Elements()
                                where long.Parse(item.Element("guestRequestKey").Value) == key
                                select new GuestRequest()
                                {
                                    GuestRequestKey = long.Parse(item.Element("GuestRequestKey").Value),
                                    PrivateName = item.Element("PrivateName").Value,
                                    FamilyName = item.Element("FamilyName").Value,
                                    MailAddress = item.Element("MailAddress").Value,
                                    Status = (BE.Enum.CustomerOrderStatus)System.Enum.Parse(typeof(BE.Enum.CustomerOrderStatus), "Status", true),
                                    RegistrationDate = DateTime.Parse(item.Element("RegistrationDate").Value),
                                    EntryDate = DateTime.Parse(item.Element("EntryDate").Value),
                                    ReleaseDate = DateTime.Parse(item.Element("ReleaseDate").Value),
                                    Area = (BE.Enum.Area)System.Enum.Parse(typeof(BE.Enum.Area), "Area", true),
                                    Type = (BE.Enum.ResortType)System.Enum.Parse(typeof(BE.Enum.ResortType), "Type", true),
                                    Adults = int.Parse(item.Element("Adults").Value),
                                    Children = int.Parse(item.Element("Children").Value),
                                    Spooky = (BE.Enum.Spooky)System.Enum.Parse(typeof(BE.Enum.Spooky), "Spooky", true),
                                    Pool = (BE.Enum.Pool)System.Enum.Parse(typeof(BE.Enum.Pool), "Pool", true),
                                    Jacuzzi = (BE.Enum.Jacuzzi)System.Enum.Parse(typeof(BE.Enum.Jacuzzi), "Jacuzzi", true),
                                    Garden = (BE.Enum.Garden)System.Enum.Parse(typeof(BE.Enum.Garden), "Garden", true),
                                    ChildrensAttractions = (BE.Enum.ChildrensAttractions)System.Enum.Parse(typeof(BE.Enum.ChildrensAttractions), "ChildrensAttractions", true),
                                    Code = item.Element("Code").Value,
                                    amountOFPeople = int.Parse(item.Element("amountOFPeople").Value),
                                    amount = int.Parse(item.Element("amount").Value)
                                }).FirstOrDefault();
            }
            catch
            {
                guestRequest = null;
            }
            return guestRequest;
        }
        public Order GetOrder(long key)
        {
            LoadDataOrder();
            Order order;
            try
            {
                order = (from item in OrderRoot.Elements()
                         where long.Parse(item.Element("orderKey").Value) == key
                         select new Order()
                         {
                             HostingUnitKey = long.Parse(item.Element("HostingUnitKey").Value),
                             GuestRequestKey = long.Parse(item.Element("GuestRequestKey").Value),
                             Status = (BE.Enum.OrderStatus)System.Enum.Parse(typeof(BE.Enum.OrderStatus), "Status", true),
                             CreateDate = DateTime.Parse(item.Element("CreateDate").Value),
                             OrderDate = DateTime.Parse(item.Element("OrderDate").Value),
                             HostingUnitName = item.Element("HostingUnitName").Value
                         }).FirstOrDefault();
            }
            catch
            {
                order = null;
            }
            return order;
        }
        public BankBranch GetBankBranch(int branchNumber)
        {
            //LoadDataBankBranch();
            BankBranch bankBranch;
            try
            {
                bankBranch = (from item in BankBranchRoot.Elements()
                              where int.Parse(item.Element("BranchNumber").Value) == branchNumber
                              select new BankBranch()
                              {
                                  BankNumber = int.Parse(item.Element("BankNumber").Value),
                                  BankName = item.Element("BankName").Value,
                                  BranchAddress = item.Element("BranchAddress").Value,
                                  BranchCity = item.Element("BranchCity").Value
                              }).FirstOrDefault();
            }
            catch
            {
                bankBranch = null;
            }
            return bankBranch;
        }
        public Object GetConfig(int num)
        {
            LoadDataConfig();
            //Configuration config;
            foreach (var X in ConfigRoot.Elements())
            {
                if (int.Parse(X.Element("Key").Value) == num)
                {
                    if (Convert.ToBoolean(X.Element("Value").Element("Readable").Value))
                        return int.Parse(X.Element("Value").Element("value").Value);
                    throw new AccessViolationException("שגיאה! אין הרשאה לראות מאפיין קונפיגורציה זה.");
                }
            }
            throw new KeyNotFoundException("שגיאה! לא קיים קונפיגורציה במערכת בשם זה.");
        }
        #endregion

        #region Guest Request
        void Idal.AddGuestRequest(GuestRequest guestRequest)
        {
            LoadDataGuestRequest();
            LoadDataConfig();
            if (GuestRequestRoot.Elements() == null)
            {
                CreateFilesHostingUnit();
                //GuestRequestRoot.Add(guestRequest);
                //GuestRequestRoot.Save(GuestRequestPath);
            }
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
            ConfigRoot = XElement.Load(ConfigPath);
            long code = long.Parse(ConfigRoot.Element("GuestRequestKey").Value) + 1;
            guestRequest.GuestRequestKey = code;
            int amount = (int)(guestRequest.ReleaseDate - guestRequest.EntryDate).TotalDays;
            guestRequest.amount = (int)(guestRequest.ReleaseDate - guestRequest.EntryDate).TotalDays;
            guestRequest.amountOFPeople = guestRequest.Adults + guestRequest.Children;
            //GuestRequestRoot.Add(guestRequest);
            XElement GuestRequestKey = new XElement("GuestRequestKey", guestRequest.GuestRequestKey);
            XElement PrivateName = new XElement("PrivateName", guestRequest.PrivateName);
            XElement FamilyName = new XElement("FamilyName", guestRequest.FamilyName);
            XElement MailAddress = new XElement("MailAddress", guestRequest.MailAddress);
            XElement Status = new XElement("Status", guestRequest.Status);
            XElement RegistrationDate = new XElement("RegistrationDate", guestRequest.RegistrationDate);
            XElement EntryDate = new XElement("EntryDate", guestRequest.EntryDate);
            XElement ReleaseDate = new XElement("ReleaseDate", guestRequest.ReleaseDate);
            XElement Area = new XElement("Area", guestRequest.Area);
            XElement Type = new XElement("Type", guestRequest.Type);
            XElement Adults = new XElement("Adults", guestRequest.Adults);
            XElement Children = new XElement("Children", guestRequest.Children);
            XElement Spooky = new XElement("Spooky", guestRequest.Spooky);
            XElement Pool = new XElement("Pool", guestRequest.Pool);
            XElement Jacuzzi = new XElement("Jacuzzi", guestRequest.Jacuzzi);
            XElement Garden = new XElement("Garden", guestRequest.Garden);
            XElement ChildrensAttractions = new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions);
            XElement Code = new XElement("Code", guestRequest.Code);
            XElement amountOFPeople = new XElement("amountOFPeople", guestRequest.Children + guestRequest.Adults);
            XElement Amount = new XElement("amount", guestRequest.amount);

            GuestRequestRoot.Add(new XElement("guestRequest", GuestRequestKey, PrivateName, FamilyName, MailAddress, Status, RegistrationDate, EntryDate, ReleaseDate, Area, Type, Adults, Children, Spooky, Pool, Jacuzzi, Garden, ChildrensAttractions, Code, amountOFPeople, Amount));
            GuestRequestRoot.Save(GuestRequestPath);
            ConfigRoot.Element("GuestRequestKey").Value = code.ToString();
            int number = int.Parse(ConfigRoot.Element("amountOfOrders").Value) + 1;
            ConfigRoot.Element("amountOfOrders").Value = number.ToString();
            ConfigRoot.Save(ConfigPath);
            //GuestRequestRoot.Add(GetGuestRequest(guestRequest.GuestRequestKey));
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
            XElement result = (from item in GuestRequestRoot.Elements()
                               where long.Parse(item.Element("GuestRequestKey").Value) == guestRequest.GuestRequestKey
                               select item).FirstOrDefault();
            if (result == null)
            {
                throw new KeyNotFoundException("Dal: Couldn't update the request because it dosn't exist");
            }
            result.Remove();

            //GuestRequestRoot.Add(guestRequest);
            XElement GuestRequestKey = new XElement("GuestRequestKey", guestRequest.GuestRequestKey);
            XElement PrivateName = new XElement("PrivateName", guestRequest.PrivateName);
            XElement FamilyName = new XElement("FamilyName", guestRequest.FamilyName);
            XElement MailAddress = new XElement("MailAddress", guestRequest.MailAddress);
            XElement Status = new XElement("Status", guestRequest.Status);
            XElement RegistrationDate = new XElement("RegistrationDate", guestRequest.RegistrationDate);
            XElement EntryDate = new XElement("EntryDate", guestRequest.EntryDate);
            XElement ReleaseDate = new XElement("ReleaseDate", guestRequest.ReleaseDate);
            XElement Area = new XElement("Area", guestRequest.Area);
            XElement Type = new XElement("Type", guestRequest.Type);
            XElement Adults = new XElement("Adults", guestRequest.Adults);
            XElement Children = new XElement("Children", guestRequest.Children);
            XElement Spooky = new XElement("Spooky", guestRequest.Spooky);
            XElement Pool = new XElement("Pool", guestRequest.Pool);
            XElement Jacuzzi = new XElement("Jacuzzi", guestRequest.Jacuzzi);
            XElement Garden = new XElement("Garden", guestRequest.Garden);
            XElement ChildrensAttractions = new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions);
            XElement Code = new XElement("Code", guestRequest.Code);

            GuestRequestRoot.Add(new XElement("guestRequest", GuestRequestKey, PrivateName, FamilyName, MailAddress, Status, RegistrationDate, EntryDate, ReleaseDate, Area, Type, Adults, Children, Spooky, Pool, Jacuzzi, Garden, ChildrensAttractions, Code));

            GuestRequestRoot.Save(GuestRequestPath);
        }
        void Idal.UpdateGuestRequestInfo(GuestRequest guestRequest)
        {
            int i = 0, j = GuestRequestRoot.Elements().Count();
            foreach (var item1 in GuestRequestRoot.Elements())
            {
                i++;
                if (item1.Element("Code").Value.CompareTo(guestRequest.Code) == 0)
                {
                    item1.Remove();

                    //DataSource.guestRequests.Remove(DataSource.guestRequests.Find(x => x.GuestRequestKey.CompareTo(item1.Element("Code").Value) == 0));
                    item1.Element("PrivateName").Value = guestRequest.PrivateName;
                    item1.Element("FamilyName").Value = guestRequest.FamilyName;
                    item1.Element("MailAddress").Value = guestRequest.MailAddress;

                    //GuestRequestRoot.Add(GetGuestRequest(guestRequest.GuestRequestKey));
                    XElement GuestRequestKey = new XElement("GuestRequestKey", guestRequest.GuestRequestKey);
                    XElement PrivateName = new XElement("PrivateName", guestRequest.PrivateName);
                    XElement FamilyName = new XElement("FamilyName", guestRequest.FamilyName);
                    XElement MailAddress = new XElement("MailAddress", guestRequest.MailAddress);
                    XElement Status = new XElement("Status", guestRequest.Status);
                    XElement RegistrationDate = new XElement("RegistrationDate", guestRequest.RegistrationDate);
                    XElement EntryDate = new XElement("EntryDate", guestRequest.EntryDate);
                    XElement ReleaseDate = new XElement("ReleaseDate", guestRequest.ReleaseDate);
                    XElement Area = new XElement("Area", guestRequest.Area);
                    XElement Type = new XElement("Type", guestRequest.Type);
                    XElement Adults = new XElement("Adults", guestRequest.Adults);
                    XElement Children = new XElement("Children", guestRequest.Children);
                    XElement Spooky = new XElement("Spooky", guestRequest.Spooky);
                    XElement Pool = new XElement("Pool", guestRequest.Pool);
                    XElement Jacuzzi = new XElement("Jacuzzi", guestRequest.Jacuzzi);
                    XElement Garden = new XElement("Garden", guestRequest.Garden);
                    XElement ChildrensAttractions = new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions);
                    XElement Code = new XElement("Code", guestRequest.Code);

                    GuestRequestRoot.Add(new XElement("guestRequest", GuestRequestKey, PrivateName, FamilyName, MailAddress, Status, RegistrationDate, EntryDate, ReleaseDate, Area, Type, Adults, Children, Spooky, Pool, Jacuzzi, Garden, ChildrensAttractions, Code));

                    GuestRequestRoot.Save(GuestRequestPath);
                    //DataSource.guestRequests.Add(item1.Copy<GuestRequest>());
                }
                if (i == j)
                {
                    return;
                }
            }
        }

        void Idal.UpdateGuestRequestCode(GuestRequest guestRequest)
        {
            XElement GuestRequestElement = (from item in GuestRequestRoot.Elements()
                                            where long.Parse(item.Element("GuestRequestKey").Value) == guestRequest.GuestRequestKey
                                            select item).FirstOrDefault();
            GuestRequestElement.Element("Code").Value = guestRequest.Code;
            GuestRequestRoot.Save(GuestRequestPath);
        }
        void Idal.RemoveGuestRequest(GuestRequest guestRequest)
        {
            try
            {
                XElement GuestRequestElement = (from item in GuestRequestRoot.Elements()
                                                where long.Parse(item.Element("GuestRequestKey").Value) == guestRequest.GuestRequestKey
                                                select item).FirstOrDefault();
                GuestRequestElement.Remove();
                GuestRequestRoot.Save(GuestRequestPath);
            }
            catch
            {
                throw new DataException("Dal: Can't remove the guest request");
            }
        }
        #endregion

        #region Hosting Unit
        void Idal.AddHostingUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> lst = LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            var v = from item in lst
                    where item.HostingUnitKey == hostingUnit.HostingUnitKey
                    select item;
            if (v.FirstOrDefault() != null)
            {
                throw new InvalidDataException("The hostingUnit already exists");
            }
            ConfigRoot = XElement.Load(ConfigPath);
            hostingUnit.Owner.amountOfHostingUnits = hostingUnit.Owner.amountOfHostingUnits + 1;
            long number = int.Parse(ConfigRoot.Element("HostingUnitKey").Value) + 1;
            hostingUnit.HostingUnitKey = number;
            hostingUnit.Diary = new bool[31, 12];
            lst.Add(hostingUnit);
            SaveToXML<List<HostingUnit>>(lst, HostingUnitPath);
            ConfigRoot.Element("HostingUnitKey").Value = number.ToString();
            ConfigRoot.Save(ConfigPath);
            var result = from item in HostRoot.Elements()
                         where item.Element("HostKey").Value == hostingUnit.Owner.HostKey
                         select item;

            XElement privateName = new XElement("PrivateName", result.FirstOrDefault().Element("PrivateName").Value);
            XElement familyName = new XElement("FamilyName", result.FirstOrDefault().Element("FamilyName").Value);
            XElement phoneNumber = new XElement("PhoneNumber", result.FirstOrDefault().Element("PhoneNumber").Value);
            XElement mailAddress = new XElement("MailAddress", result.FirstOrDefault().Element("MailAddress").Value);
            XElement fullName = new XElement("fullName", result.FirstOrDefault().Element("fullName").Value);
            XElement amountOfHostingUnits = new XElement("amountOfHostingUnits", int.Parse(result.FirstOrDefault().Element("amountOfHostingUnits").Value) + 1);
            XElement hostKey = new XElement("HostKey", result.FirstOrDefault().Element("HostKey").Value);
            XElement CollectionClearance = new XElement("CollectionClearance", result.FirstOrDefault().Element("CollectionClearance").Value);
            XElement BankAccountNumber = new XElement("BankAccountNumber", result.FirstOrDefault().Element("BankAccountNumber").Value);
            XElement bankBranchDetails = new XElement("BankBranchDetails", result.FirstOrDefault().Element("BankBranchDetails").Value);
            HostRoot.Add(new XElement("host", privateName, familyName, phoneNumber, mailAddress, fullName, amountOfHostingUnits, hostKey, CollectionClearance, BankAccountNumber, bankBranchDetails));
            //HostRoot.Add(host);
            result.FirstOrDefault().Remove();
            HostRoot.Save(HostPath);
            //result.FirstOrDefault().Element("amountOfHostingUnits").Value;
            //int.Parse(result.FirstOrDefault().Element("amountOfHostingUnits").Value)++;
            //result.FirstOrDefault().ReplaceNodes("amountOfHostingUnits");


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
            List<HostingUnit> hostingUnits = LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            var result = from item in hostingUnits
                         where item.HostingUnitKey == hostingUnitKey
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't remove the host unit because it doesn't exist");
            }
            var result1 = from item in hostingUnits
                          where item.HostingUnitKey == hostingUnitKey
                          select item;

            result1.FirstOrDefault().Owner.amountOfHostingUnits--;
            var result2 = from item in HostRoot.Elements()
                          where item.Element("HostKey").Value == result1.FirstOrDefault().Owner.HostKey
                          select item;

            XElement privateName = new XElement("PrivateName", result2.FirstOrDefault().Element("PrivateName").Value);
            XElement familyName = new XElement("FamilyName", result2.FirstOrDefault().Element("FamilyName").Value);
            XElement phoneNumber = new XElement("PhoneNumber", result2.FirstOrDefault().Element("PhoneNumber").Value);
            XElement mailAddress = new XElement("MailAddress", result2.FirstOrDefault().Element("MailAddress").Value);
            XElement fullName = new XElement("fullName", result2.FirstOrDefault().Element("fullName").Value);
            XElement amountOfHostingUnits = new XElement("amountOfHostingUnits", int.Parse(result2.FirstOrDefault().Element("amountOfHostingUnits").Value) - 1);
            XElement hostKey = new XElement("HostKey", result2.FirstOrDefault().Element("HostKey").Value);
            XElement CollectionClearance = new XElement("CollectionClearance", result2.FirstOrDefault().Element("CollectionClearance").Value);
            XElement BankAccountNumber = new XElement("BankAccountNumber", result2.FirstOrDefault().Element("BankAccountNumber").Value);
            XElement bankBranchDetails = new XElement("BankBranchDetails", result2.FirstOrDefault().Element("BankBranchDetails").Value);
            result2.FirstOrDefault().Remove();
            HostRoot.Add(new XElement("host", privateName, familyName, phoneNumber, mailAddress, fullName, amountOfHostingUnits, hostKey, CollectionClearance, BankAccountNumber, bankBranchDetails));
            HostRoot.Save(HostPath);

            foreach (var item in hostingUnits)
            {
                if (item.HostingUnitKey == hostingUnitKey)
                {
                    hostingUnits.Remove(item);
                    break;
                }
            }
            SaveToXML<List<HostingUnit>>(hostingUnits, HostingUnitPath);
        }

        void Idal.UpdateHostingUnit(HostingUnit hostingUnit)
        {
            List<HostingUnit> hostingUnits = LoadFromXML<List<HostingUnit>>(HostingUnitPath);

            var result = from item in hostingUnits
                         where item.HostingUnitKey == hostingUnit.HostingUnitKey
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't update the host unit because it doesn't exist");
            }
            if (hostingUnit.Diary == null)
            {
                hostingUnit.Diary = new bool[12, 31];
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 31; j++)
                    {
                        hostingUnit.Diary[j, i] = result.FirstOrDefault().Diary[j, i];
                    }
                }
            }
            foreach (var item in hostingUnits)
            {
                if (item.HostingUnitKey == hostingUnit.HostingUnitKey)
                {
                    hostingUnits.Remove(item);
                    break;
                }
            }
            hostingUnits.Add(hostingUnit.Copy());
            SaveToXML<List<HostingUnit>>(hostingUnits, HostingUnitPath);
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
            List<HostingUnit> lst = new List<HostingUnit>();
            lst = LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            if (lst.Count() == 0 && GuestRequestRoot.Elements().Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Couldn't add the order because there aren't any hosting units or guests requests");
            }
            long code = long.Parse(ConfigRoot.Element("OrderKey").Value);
            order.OrderKey = ++code;
            ConfigRoot.Element("OrderKey").Value = code.ToString();
            ConfigRoot.Save(ConfigPath);
            var result = from item in lst
                         where item.HostingUnitKey == order.HostingUnitKey
                         select item;

            XElement HostingUnitKey = new XElement("HostingUnitKey", order.HostingUnitKey);
            XElement GuestRequestKey = new XElement("GuestRequestKey", order.GuestRequestKey);
            XElement OrderKey = new XElement("OrderKey", order.OrderKey);
            XElement OrderStatus = new XElement("OrderStatus", order.Status);
            XElement CreateDate = new XElement("CreateDate", DateTime.Now);
            XElement OrderDate = new XElement("OrderDate", DateTime.Now);
            XElement HostingUnitName = new XElement("HostingUnitName", result.FirstOrDefault().HostingUnitName);

            OrderRoot.Add(new XElement("order", HostingUnitKey, GuestRequestKey, OrderKey, OrderStatus, CreateDate, OrderDate, HostingUnitName));
            OrderRoot.Save(OrderPath);
        }

        void Idal.UpdateOrder(Order order)
        {
            var result = from item in OrderRoot.Elements()
                         where order.OrderKey == long.Parse(item.Element("OrderKey").Value)
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Couldn't update the order because the order doesn't exist");
            }
            long amount = int.Parse(ConfigRoot.Element("amountOfOrders").Value);
            amount++;
            ConfigRoot.Element("amountOfOrders").Value = amount.ToString();
            ConfigRoot.Save(ConfigPath);
            var result2 = from item in OrderRoot.Elements()
                          where long.Parse(item.Element("OrderKey").Value) == order.OrderKey
                          select item;
            result2.FirstOrDefault().Element("OrderStatus").Value = BE.Enum.OrderStatus.ClosedDueToCostumerResponse.ToString();

            var result3 = from item3 in GuestRequestRoot.Elements()
                          where long.Parse(item3.Element("GuestRequestKey").Value) == order.GuestRequestKey
                          select item3;
            string firstName = result3.FirstOrDefault().Element("PrivateName").Value;
            string lastName = result3.FirstOrDefault().Element("FamilyName").Value;
            DateTime entryDate = DateTime.Parse(result3.FirstOrDefault().Element("EntryDate").Value);
            DateTime releaseDate = DateTime.Parse(result3.FirstOrDefault().Element("ReleaseDate").Value);


            foreach (var item in GuestRequestRoot.Elements())
            {
                if ((item.Element("PrivateName").Value == firstName) && (item.Element("FamilyName").Value == lastName))
                {
                    if ((DateTime.Parse(item.Element("EntryDate").Value) == entryDate && (DateTime.Parse(item.Element("ReleaseDate").Value) == releaseDate)))
                    {
                        item.Element("Status").Value = BE.Enum.CustomerOrderStatus.ClosedThroughWebsite.ToString();
                    }
                }
            }
            OrderRoot.Save(OrderPath);
            GuestRequestRoot.Save(GuestRequestPath);
        }

        void Idal.UpdateStatus()
        {
            long amount = int.Parse(ConfigRoot.Element("amountOfDaysUntilOutOfDate").Value);
            foreach (var item in OrderRoot.Elements())
            {
                if ((DateTime.Now - DateTime.Parse(item.Element("CreateDate").Value)).TotalDays > amount && (BE.Enum.OrderStatus)System.Enum.Parse(typeof(BE.Enum.OrderStatus), item.Element("Status").Value) == BE.Enum.OrderStatus.AnEmailWasSent)
                {
                    item.Element("Status").Value = BE.Enum.OrderStatus.ClosedDueToCostumerLackOfResponse.ToString();
                }
                if ((DateTime.Now - DateTime.Parse(item.Element("CreateDate").Value)).TotalDays > amount && (BE.Enum.OrderStatus)System.Enum.Parse(typeof(BE.Enum.OrderStatus), item.Element("Status").Value) == BE.Enum.OrderStatus.ClosedDueToCostumerBooked)
                {
                    var result = from item2 in GuestRequestRoot.Elements()
                                 where item.Element("GuestRequestKey").Value.CompareTo(item2.Element("GuestRequestKey").Value) == 0
                                 select item2;
                    result.FirstOrDefault().Remove();
                    item.Remove();
                }
            }
            OrderRoot.Save(OrderPath);
        }


        #endregion

        #region Get Lists
        IEnumerable<HostingUnit> Idal.GetHostingUnitCopy()
        {
            return LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            //var result = from item in HostingUnitRoot.Elements()
            //             select GetHostingUnit(long.Parse(item.Element("HostingUnitKey").Value));

            //return result.AsEnumerable().OrderByDescending(s => s.HostingUnitName);
        }

        IEnumerable<GuestRequest> Idal.GetGuests()
        {
            LoadDataGuestRequest();
            List<GuestRequest> lst = new List<GuestRequest>();
            foreach (var item in GuestRequestRoot.Elements())
            {
                lst.Add(new GuestRequest()
                {
                    GuestRequestKey = long.Parse(item.Element("GuestRequestKey").Value),
                    PrivateName = item.Element("PrivateName").Value,
                    FamilyName = item.Element("FamilyName").Value,
                    MailAddress = item.Element("MailAddress").Value,
                    Status = (BE.Enum.CustomerOrderStatus)System.Enum.Parse(typeof(BE.Enum.CustomerOrderStatus), item.Element("Status").Value, true),
                    RegistrationDate = DateTime.Parse(item.Element("RegistrationDate").Value),
                    EntryDate = DateTime.Parse(item.Element("EntryDate").Value),
                    ReleaseDate = DateTime.Parse(item.Element("ReleaseDate").Value),
                    Area = (BE.Enum.Area)System.Enum.Parse(typeof(BE.Enum.Area), item.Element("Area").Value, true),
                    Type = (BE.Enum.ResortType)System.Enum.Parse(typeof(BE.Enum.ResortType), item.Element("Type").Value, true),
                    Adults = int.Parse(item.Element("Adults").Value),
                    Children = int.Parse(item.Element("Children").Value),
                    Spooky = (BE.Enum.Spooky)System.Enum.Parse(typeof(BE.Enum.Spooky), item.Element("Spooky").Value, true),
                    Pool = (BE.Enum.Pool)System.Enum.Parse(typeof(BE.Enum.Pool), item.Element("Pool").Value, true),
                    Jacuzzi = (BE.Enum.Jacuzzi)System.Enum.Parse(typeof(BE.Enum.Jacuzzi), item.Element("Jacuzzi").Value, true),
                    Garden = (BE.Enum.Garden)System.Enum.Parse(typeof(BE.Enum.Garden), item.Element("Garden").Value, true),
                    ChildrensAttractions = (BE.Enum.ChildrensAttractions)System.Enum.Parse(typeof(BE.Enum.ChildrensAttractions), item.Element("ChildrensAttractions").Value, true),
                    amountOFPeople=int.Parse(item.Element("amountOFPeople").Value),
                    amount=int.Parse(item.Element("amount").Value),
                    Code = item.Element("Code").Value
                });
            }
            return lst;
        }

        IEnumerable<Order> Idal.GetOrders()
        {
            LoadDataOrder();
            List<Order> list = new List<Order>();
            foreach (var item in OrderRoot.Elements())
            {
                list.Add(new Order()
                {
                    HostingUnitKey = long.Parse(item.Element("HostingUnitKey").Value),
                    GuestRequestKey = long.Parse(item.Element("GuestRequestKey").Value),
                    OrderKey = long.Parse(item.Element("OrderKey").Value),
                    Status = (BE.Enum.OrderStatus)System.Enum.Parse(typeof(BE.Enum.OrderStatus), item.Element("OrderStatus").Value, true),
                    CreateDate = DateTime.Parse(item.Element("CreateDate").Value),
                    OrderDate = DateTime.Parse(item.Element("OrderDate").Value),
                    HostingUnitName = item.Element("HostingUnitName").Value
                });
            }
            return list;
        }

        IEnumerable<Host> Idal.GetHosts()
        {
            LoadDataHost();
            List<Host> hosts;
            try
            {
                hosts = (from p in HostRoot.Elements()
                         select new Host()
                         {
                             PrivateName = p.Element("PrivateName").Value,
                             FamilyName = p.Element("FamilyName").Value,
                             PhoneNumber = p.Element("PhoneNumber").Value,
                             MailAddress = p.Element("MailAddress").Value,
                             fullName = p.Element("fullName").Value,
                             amountOfHostingUnits = int.Parse(p.Element("amountOfHostingUnits").Value),
                             HostKey = p.Element("HostKey").Value,
                             CollectionClearance = bool.Parse(p.Element("CollectionClearance").Value),
                             BankAccountNumber = int.Parse(p.Element("BankAccountNumber").Value)
                         }).ToList();
            }
            catch
            {
                hosts = null;
            }
            return hosts;
        }
        List<BankBranch> Idal.GetBankBranches()
        {
            //while (branches.Count() != 1488)
            //{

            //}
            return branches;
        }
        #endregion

        #region Host

        void Idal.AddHost(Host host)
        {
            if (!isValidEmail(host.MailAddress))
            {
                throw new InvalidDataException("Dal: can't add the host because the email address is invalid");
            }
            XElement privateName = new XElement("PrivateName", host.PrivateName);
            XElement familyName = new XElement("FamilyName", host.FamilyName);
            XElement phoneNumber = new XElement("PhoneNumber", host.PhoneNumber);
            XElement mailAddress = new XElement("MailAddress", host.MailAddress);
            XElement fullName = new XElement("fullName", host.fullName);
            XElement amountOfHostingUnits = new XElement("amountOfHostingUnits", 0);
            XElement hostKey = new XElement("HostKey", host.HostKey);
            XElement CollectionClearance = new XElement("CollectionClearance", host.CollectionClearance);
            XElement BankAccountNumber = new XElement("BankAccountNumber", host.BankAccountNumber);

            XElement BankNumber = new XElement("BankNumber", host.BankBranchDetails.BankNumber);
            XElement BankName = new XElement("BankName", host.BankBranchDetails.BankName);
            XElement BranchNumber = new XElement("BranchNumber", host.BankBranchDetails.BranchNumber);
            XElement BranchAddress = new XElement("BranchAddress", host.BankBranchDetails.BranchAddress);
            XElement BranchCity = new XElement("BranchCity", host.BankBranchDetails.BranchCity);

            XElement bankBranchDetails = new XElement("BankBranchDetails", BankNumber, BankName, BranchNumber, BranchAddress, BranchCity);
            HostRoot.Add(new XElement("host", privateName, familyName, phoneNumber, mailAddress, fullName, amountOfHostingUnits, hostKey, CollectionClearance, BankAccountNumber, bankBranchDetails));
            //HostRoot.Add(host);
            HostRoot.Save(HostPath);
        }

        void Idal.UpdateHost(Host host)
        {
            var result = from item in HostRoot.Elements()
                         where int.Parse(item.Element("HostKey").Value).CompareTo(host.HostKey) == 0
                         select item;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't update the host because it doesn't exist");
            }
            result.FirstOrDefault().Remove();
            HostRoot.Add(host);
            HostRoot.Save(HostPath);
        }

        void Idal.RemoveHost(Host host)
        {
            var result = from item1 in HostRoot.Elements()
                         where item1.Element("HostKey").Value.CompareTo(host.HostKey) == 0
                         select item1;
            if (result.Count() == 0)
            {
                throw new KeyNotFoundException("Dal: Can't remove the host because it doesn't exist");
            }
            List<HostingUnit> lst = new List<HostingUnit>();
            lst = LoadFromXML<List<HostingUnit>>(HostingUnitPath);
            var result2 = from item2 in lst
                          where item2.Owner.HostKey.CompareTo(host.HostKey) == 0
                          select item2;
            foreach (var item3 in result2)
            {
                lst.Remove(item3);
            }
            result.FirstOrDefault().Remove();
            HostRoot.Save(HostPath);
        }
        #endregion

        #region Reviews
        void Idal.AddReviewFromGuest(messages m)
        {
            XElement review = new XElement("review", m.message);
            XElement date = new XElement("date", DateTime.Now);

            reviewsFromGuestsRoot.Add(new XElement("review", review, date));
            reviewsFromGuestsRoot.Save(reviewsFromGuestsPath);
        }

        void Idal.AddReviewToGuest(messages m)
        {
            XElement review = new XElement("review", m.message);
            XElement date = new XElement("date", DateTime.Now);

            reviewsToGuestsRoot.Add(new XElement("review", review, date));
            reviewsToGuestsRoot.Save(reviewsToGuestsPath);
        }

        void Idal.AddCommentFromHost(messages m)
        {
            XElement review = new XElement("review", m.message);
            XElement date = new XElement("date", DateTime.Now);

            commentsFromHostRoot.Add(new XElement("review", review, date));
            commentsFromHostRoot.Save(commentsFromHostPath);
        }

        void Idal.RemoveReviewToGuset(messages m)
        {
            var result = from item in reviewsToGuestsRoot.Elements()
                         where DateTime.Parse(item.Element("date").Value).CompareTo(m.date) == 0
                         select item;
            result.FirstOrDefault().Remove();
            reviewsToGuestsRoot.Save(reviewsToGuestsPath);
        }

        void Idal.RemoveReviewFromGuset(messages m)
        {
            //var result = from item in reviewsFromGuestsRoot.Elements()
            //             where item.Element("date").Value.CompareTo(m.date) == 0
            //             select item;
            var result = from item in reviewsFromGuestsRoot.Elements()
                         where DateTime.Parse(item.Element("date").Value).CompareTo(m.date) == 0
                         select item;
            result.FirstOrDefault().Remove();
            reviewsFromGuestsRoot.Save(reviewsFromGuestsPath);
        }

        void Idal.RemoveCommentFromHost(messages m)
        {
            var result = from item in commentsFromHostRoot.Elements()
                         where DateTime.Parse(item.Element("date").Value).CompareTo(m.date) == 0
                         select item;
            result.FirstOrDefault().Remove();
            commentsFromHostRoot.Save(commentsFromHostPath);
        }

        IEnumerable<messages>Idal.GetReviewsFromGuests()
        {
            LoadDatareviewsFromGuests();
            List<messages> lst = new List<messages>();
            foreach(var item in reviewsFromGuestsRoot.Elements())
            {
                lst.Add(new messages()
                {
                    message = item.Element("review").Value,
                    date=DateTime.Parse(item.Element("date").Value)
                });
            }
            return lst;
        }

        IEnumerable<messages> Idal.GetReviewsToGuests()
        {
            LoadDatareviewsToGuests();
            List<messages> lst = new List<messages>();
            foreach (var item in reviewsToGuestsRoot.Elements())
            {
                lst.Add(new messages()
                {
                    message = item.Element("review").Value,
                    date = DateTime.Parse(item.Element("date").Value)
                });
            }
            return lst;
        }

        IEnumerable<messages> Idal.GetCommentsFromHosts()
        {
           LoadDatacommentsFromHost();
            List<messages> lst = new List<messages>();
            foreach (var item in commentsFromHostRoot.Elements())
            {
                lst.Add(new messages()
                {
                    message = item.Element("review").Value,
                    date = DateTime.Parse(item.Element("date").Value)
                });
            }
            return lst;
        }
        #endregion
    }
}
