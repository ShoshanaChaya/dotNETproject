using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using static BE.Enum;
using static BE.Configuration;

namespace DS
{
    public static class DataSource
    {
        public static List<Order> orders = new List<Order>()
        {
            new Order()
            {
                OrderKey = 10000000,
                Status = OrderStatus.AnEmailWasSent,
                CreateDate = new DateTime(2020,1,1),
                OrderDate = new DateTime(2020,3,3),
                HostingUnitKey = 10000000,
                GuestRequestKey = 10000000,
                HostingUnitName = "apple"
            },
            new Order()
            {
                OrderKey = 10000001,
                Status = OrderStatus.AnEmailWasSent,
                CreateDate = new DateTime(2020,2, 2),
                OrderDate = new DateTime(2020,4,4),
                HostingUnitKey = 10000001,
                GuestRequestKey = 10000001,
                HostingUnitName = "orange"
            },
            new Order()
            {
                OrderKey = 10000002,
                Status = OrderStatus.ClosedDueToCostumerResponse,
                CreateDate = new DateTime(2020,3,3),
                OrderDate = new DateTime(2020,5,5),
                HostingUnitKey = 10000002,
                GuestRequestKey = 10000002,
                HostingUnitName = "pear"

            }
        };

        // int i = 3, j = 5;
        public static List<HostingUnit> hostingUnits = new List<HostingUnit>()
        {
            new HostingUnit()
            {
                HostingUnitKey = 10000000,
                HostingUnitName = "apple",
                Area=BE.Enum.Area.Center,
                Type=ResortType.Hotel,
                Garden= Garden.Necessary,
                Jacuzzi= Jacuzzi.NotInterested,
                ChildrensAttractions= ChildrensAttractions.NotInterested,
                Pool=Pool.Necessary,
                Diary=new bool[12,31],
                Spooky=Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "017735705",
                    PrivateName = "david",
                    FamilyName = "cohen",
                    PhoneNumber = "0543198488",
                    MailAddress = "yaelfi6@gmail.com",
                    BankAccountNumber = 112234,
                    fullName="david cohen",
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 11,
                        BranchAddress = "beit hadfus 78",
                        BranchCity  ="tlv"
                    }
                }
            },
             new HostingUnit()
            {
                HostingUnitKey = 10000005,
                HostingUnitName = "mushroom",
                Area=BE.Enum.Area.West,
                Type=ResortType.AccommodationApartment,
                Garden = Garden.NotInterested,
                Jacuzzi= Jacuzzi.NotInterested,
                ChildrensAttractions= ChildrensAttractions.NotInterested,
                Pool=Pool.Necessary,
                Diary=new bool[12,31],
                Spooky=Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "017735705",
                    PrivateName = "david",
                    FamilyName = "cohen",
                    PhoneNumber = "0543198488",
                    MailAddress = "yaelfi6@gmail.com",
                    BankAccountNumber = 112234,
                    CollectionClearance = true,
                    fullName="david cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 11,
                        BranchAddress = "beit hadfus 78",
                        BranchCity  ="tlv"
                    }
                }
            },
            new HostingUnit()
            {
                HostingUnitKey = 10000001,
                HostingUnitName = "orange",
                Area=BE.Enum.Area.East,
                Type=ResortType.Hotel,
                Garden= Garden.Necessary,
                Jacuzzi= Jacuzzi.NotInterested,
                ChildrensAttractions= ChildrensAttractions.NotInterested,
                Pool=Pool.Necessary,
                Diary=new bool[12,31],
                Spooky=Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "029567567",
                    PrivateName = "levi",
                    FamilyName = "cohen",
                    PhoneNumber = "0543198488",
                    MailAddress = "yaelfi6@gmail.com",
                    BankAccountNumber = 111212,
                    CollectionClearance = true,
                    fullName="levi cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 12,
                        BranchAddress = "beit hadfus 28",
                        BranchCity = "tlv"
                    }
                }
            },
            new HostingUnit()
            {
                HostingUnitKey = 10000003,
                HostingUnitName = "grape",
                Area=BE.Enum.Area.Center,
                Type=ResortType.Tent,
                Garden= Garden.Necessary,
                Jacuzzi= Jacuzzi.NotInterested,
                ChildrensAttractions= ChildrensAttractions.NotInterested,
                Pool=Pool.Necessary,
                Diary=new bool[12,31],
                Spooky=Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "029567567",
                    PrivateName = "levi",
                    FamilyName = "cohen",
                    PhoneNumber = "0543198488",
                    MailAddress = "yaelfi6@gmail.com",
                    BankAccountNumber = 111212,
                    CollectionClearance = true,
                    fullName="levi cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 12,
                        BranchAddress = "beit hadfus 28",
                        BranchCity = "tlv"
                    }
                }
            },
            new HostingUnit()
            {
                HostingUnitKey = 10000004,
                HostingUnitName = "onion",
                Area=BE.Enum.Area.Center,
                Type=ResortType.Hotel,
                Garden= Garden.Necessary,
                Jacuzzi= Jacuzzi.NotInterested,
                ChildrensAttractions= ChildrensAttractions.NotInterested,
                Pool=Pool.Necessary,
                Diary=new bool[12,31],
                Spooky=Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "318336070",
                    PrivateName = "ron",
                    FamilyName = "cohen",
                    PhoneNumber = "0543198488",
                    MailAddress = "yaelfi6@gmail.com",
                    BankAccountNumber = 131313,
                    CollectionClearance = true,
                    fullName="ron cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 13,
                        BranchAddress = "beit hadfus 7",
                        BranchCity = "tlv"
                    }
                }
            },
            new HostingUnit()
            {
                HostingUnitKey = 10000002,
                HostingUnitName = "pear",
                Area=BE.Enum.Area.West,
                Type=ResortType.Tzimer,
                Garden= Garden.Necessary,
                Jacuzzi= Jacuzzi.NotInterested,
                ChildrensAttractions= ChildrensAttractions.NotInterested,
                Pool=Pool.Necessary,
                Spooky=Spooky.ALittileBit,
                Diary=new bool[12,31],
                Owner = new Host()
                {
                    HostKey = "318336070",
                    PrivateName = "ron",
                    FamilyName = "cohen",
                    PhoneNumber = "0543198488",
                    MailAddress = "yaelfi6@gmail.com",
                    BankAccountNumber = 131313,
                    CollectionClearance = true,
                    fullName="ron cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 13,
                        BranchAddress = "beit hadfus 7",
                        BranchCity = "tlv"
                    }
                }
            }
        };

        public static List<GuestRequest> guestRequests = new List<GuestRequest>()
        {
            new GuestRequest()
            {
                GuestRequestKey = 10000000,
                PrivateName = "yosi",
                FamilyName = "cohen",
                MailAddress = "yosi@gmail.com",
                Status = CustomerOrderStatus.Open,
                RegistrationDate = new DateTime(2020, 1,1),
                EntryDate = new DateTime(2022,4,4),
                ReleaseDate = new DateTime(2022, 5,5),
                Area = Area.Center,
                Type = ResortType.Hotel,
                Adults = 3,
                Children = 2,
                Pool = Pool.Necessary,
                Jacuzzi = Jacuzzi.NotInterested,
                Garden = Garden.Optional,
                ChildrensAttractions = ChildrensAttractions.Necessary,
                Code="1000",
                amount=5
    },
            new GuestRequest()
            {
                GuestRequestKey = 10000001,
                PrivateName = "ron",
                FamilyName = "cohen",
                MailAddress = "ron@gmail.com",
                Status = CustomerOrderStatus.Open,
                RegistrationDate = new DateTime(2020, 1,1),
                EntryDate = new DateTime(2022,6,6),
                ReleaseDate = new DateTime(2022, 6, 8),
                Area = Area.East,
                Type = ResortType.Tent,
                Adults = 7,
                Children = 5,
                Pool = Pool.Optional,
                Jacuzzi = Jacuzzi.Optional,
                Garden = Garden.Optional,
                ChildrensAttractions = ChildrensAttractions.Optional,
                Code="1001",
                amount=12
            },
            new GuestRequest()
            {
                GuestRequestKey = 10000002,
                PrivateName = "dan",
                FamilyName = "cohen",
                MailAddress = "dan@gmail.com",
                Status = CustomerOrderStatus.OutOfDate,
                RegistrationDate = new DateTime(2020, 1,1),
                EntryDate = new DateTime(2022,4,4),
                ReleaseDate = new DateTime(2022, 5,5),
                Area = Area.South,
                Type = ResortType.Tent,
                Adults = 10,
                Children = 11,
                Pool = Pool.NotInterested,
                Jacuzzi = Jacuzzi.NotInterested,
                Garden = Garden.NotInterested,
                ChildrensAttractions = ChildrensAttractions.Necessary,
                Code="1002",
                amount=21
            }
        };
        public static List<Host> hosts = new List<Host>()
        {
            new Host()
            {
            HostKey = "318336070",
            PrivateName = "ron",
            FamilyName = "cohen",
            PhoneNumber = "056666666",
            MailAddress = "ron@gmail.com",
            BankAccountNumber = 131313,
            CollectionClearance = true,
            amountOfHostingUnits = 2,
            fullName="ron cohen",
            BankBranchDetails = new BankBranch()
                {
                BankNumber = ++Configuration.bankNumber,
                BankName = "leumi",
                BranchNumber = 13,
                BranchAddress = "beit hadfus 7",
                BranchCity = "tlv"
                }
            },
            new Host()
            {
                HostKey = "029567567",
                 PrivateName = "levi",
                    FamilyName = "cohen",
                    PhoneNumber = "055555555",
                    MailAddress = "levi@gmail.com",
                    BankAccountNumber = 111212,
                    CollectionClearance = true,
                    amountOfHostingUnits = 2,
                    fullName="levi cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 12,
                        BranchAddress = "beit hadfus 28",
                        BranchCity = "tlv"
                    }
            },
            new Host()
            {
                 HostKey = "017735705",
                    PrivateName = "david",
                    FamilyName = "cohen",
                    PhoneNumber = "054898944",
                    MailAddress = "yosi123@gmail.com",
                    BankAccountNumber = 112234,
                    CollectionClearance = true,
                    amountOfHostingUnits = 2,
                    fullName="david cohen",
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber=++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 11,
                        BranchAddress = "beit hadfus 78",
                        BranchCity  ="tlv"
                    }
            }
        };
    }
}