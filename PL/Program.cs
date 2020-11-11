using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    public class Program
    {
        IBL bl = FactoryBL.getBL();
        static void Main(string[] args)
        {
            //try
            //{
            IBL bl = FactoryBL.getBL();

            #region Hosting Unit
            HostingUnit h1 = new HostingUnit()
            {
                HostingUnitName = "apple",
                Area = BE.Enum.Area.Center,
                Type = BE.Enum.ResortType.Hotel,
                Pool = BE.Enum.Pool.Necessary,
                Jacuzzi = BE.Enum.Jacuzzi.NotInterested,
                Garden = BE.Enum.Garden.NotInterested,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                Spooky = BE.Enum.Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "315206482",
                    PrivateName = "dan",
                    FamilyName = "cohen",
                    PhoneNumber = (0543198488).ToString(),
                    MailAddress = "dan@gmail.com",
                    BankAccountNumber = 118833,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configuration.bankNumber,
                        BankName = "hapoalim",
                        BranchNumber = 18,
                        BranchAddress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h1.Diary[4, 4] = true;
            h1.Diary[4, 5] = true;
            h1.Diary[4, 6] = true;

            HostingUnit h2 = new HostingUnit()
            {
                HostingUnitName = "pear",
                Area = BE.Enum.Area.East,
                Type = BE.Enum.ResortType.Tzimer,
                Pool = BE.Enum.Pool.Necessary,
                Jacuzzi = BE.Enum.Jacuzzi.NotInterested,
                Garden = BE.Enum.Garden.Optional,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                Spooky = BE.Enum.Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "209387125",
                    PrivateName = "yosi",
                    FamilyName = "cohen",
                    PhoneNumber = (0543198488).ToString(),
                    MailAddress = "yosi@gmail.com",
                    BankAccountNumber = 912233,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 19,
                        BranchAddress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h2.Diary[8, 4] = true;
            h2.Diary[8, 5] = true;
            h2.Diary[8, 6] = true;
            HostingUnit h3 = new HostingUnit()
            {
                HostingUnitName = "orange",
                Area = BE.Enum.Area.Center,
                Type = BE.Enum.ResortType.Tent,
                Pool = BE.Enum.Pool.NotInterested,
                Jacuzzi = BE.Enum.Jacuzzi.Optional,
                Garden = BE.Enum.Garden.Necessary,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                Spooky = BE.Enum.Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "318336062",
                    PrivateName = "ron",
                    FamilyName = "cohen",
                    PhoneNumber = (0543198488).ToString(),
                    MailAddress = "ron@gmail.com",
                    BankAccountNumber = 141414,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 14,
                        BranchAddress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h3.Diary[4, 4] = true;
            h3.Diary[4, 5] = true;
            h3.Diary[4, 6] = true;
            HostingUnit h4 = new HostingUnit()
            {
                HostingUnitName = "grape",
                Area = BE.Enum.Area.East,
                Type = BE.Enum.ResortType.Tent,
                Pool = BE.Enum.Pool.NotInterested,
                Jacuzzi = BE.Enum.Jacuzzi.Optional,
                Garden = BE.Enum.Garden.Necessary,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                Spooky = BE.Enum.Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "029567567",
                    PrivateName = "yael shoshana",
                    FamilyName = "chaya",
                    PhoneNumber = (0543198488).ToString(),
                    MailAddress = "yalla@gmail.com",
                    BankAccountNumber = 112236,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 16,
                        BranchAddress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h4.Diary[2, 4] = true;
            h4.Diary[2, 5] = true;
            h4.Diary[2, 6] = true;

            HostingUnit h5 = new HostingUnit()
            {
                HostingUnitName = "olive",
                Area = BE.Enum.Area.East,
                Type = BE.Enum.ResortType.Tent,
                Pool = BE.Enum.Pool.NotInterested,
                Jacuzzi = BE.Enum.Jacuzzi.Optional,
                Garden = BE.Enum.Garden.Necessary,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                Spooky = BE.Enum.Spooky.ALittileBit,
                Owner = new Host()
                {
                    HostKey = "029567567" ,
                    PrivateName = "yael shoshana",
                    FamilyName = "chaya",
                    PhoneNumber =  (0543198488).ToString(),
                    MailAddress = "yalla@gmail.com",
                    BankAccountNumber = 112236,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configuration.bankNumber,
                        BankName = "leumi",
                        BranchNumber = 16,
                        BranchAddress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h5.Diary[2, 4] = true;
            h5.Diary[2, 5] = true;
            h5.Diary[2, 6] = true;

            bl.AddHostingUnit(h1);
            bl.AddHostingUnit(h2);
            bl.AddHostingUnit(h3);
            bl.AddHostingUnit(h4);
            bl.AddHostingUnit(h5);

            foreach (var item in bl.GetHostingUnitCopy())
            {
                Console.WriteLine("hosting unit: \n" + item + "\n");
            }
            h4.HostingUnitName = "banana";
            bl.UpdateHostingUnit(h4);
            foreach (var item in bl.GetHostingUnitCopy())
            {
                Console.WriteLine("Hosting Unit: \n" + item + "\n");
            }

            //Console.WriteLine("hosting unit group by area: \n");
            //var v = bl.GetAllHostingUnitsGroupByArea();
            //foreach (var item in v)
            //{
            //    Console.WriteLine(item.Key);
            //    foreach (var w in item)
            //        Console.WriteLine(w);
            //}

            DateTime temp = new DateTime(2020, 4, 4);
            //Console.WriteLine("get available days: \n");
            //var vd = bl.GetAvailableDays(temp, 2);
            //foreach(var item6 in vd)
            //{
            //    Console.WriteLine();
            //}
            var vd = bl.GetAvailableDays(temp, 2);
            int i = 1;
            foreach (var item6 in vd)
            {
                Console.WriteLine("hosting unit #" + i + ": \n" + item6);
                i++;
            }

            //int i = 1;
            //foreach (var item in bl.GetHostingUnitCopy())
            //{

            //    Console.WriteLine("hosting unit #" +i+ ": \n" + bl.GetAvailableDays(temp, 2) + "\n");
            //    i++;
            //}
            #endregion

            #region Guest Request
            GuestRequest g1 = new GuestRequest()
            {
                PrivateName = "yosi",
                FamilyName = "cohen",
                MailAddress = "yosi@gmail.com",
                Status = BE.Enum.CustomerOrderStatus.Open,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 4, 3),
                ReleaseDate = new DateTime(2020, 4, 6),
                Area = BE.Enum.Area.Center,
                Type = BE.Enum.ResortType.Hotel,
                Adults = 4,
                Children = 5,
                Pool = BE.Enum.Pool.Necessary,
                Jacuzzi = BE.Enum.Jacuzzi.NotInterested,
                Garden = BE.Enum.Garden.NotInterested,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                GuestRequestKey = ++Configuration.guestRequestKey,
                Spooky = BE.Enum.Spooky.ALittileBit
            };

            GuestRequest g2 = new GuestRequest()
            {
                PrivateName = "ron",
                FamilyName = "cohen",
                MailAddress = "ron@gmail.com",
                Status = BE.Enum.CustomerOrderStatus.Open,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 3, 4),
                ReleaseDate = new DateTime(2020, 3, 6),
                Area = BE.Enum.Area.East,
                Type = BE.Enum.ResortType.Tzimer,
                Adults = 3,
                Children = 2,
                Pool = BE.Enum.Pool.Necessary,
                Jacuzzi = BE.Enum.Jacuzzi.NotInterested,
                Garden = BE.Enum.Garden.Optional,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                GuestRequestKey = ++Configuration.guestRequestKey,
                Spooky = BE.Enum.Spooky.ALittileBit
            };

            GuestRequest g3 = new GuestRequest()
            {
                PrivateName = "dan",
                FamilyName = "cohen",
                MailAddress = "yosi@gmail.com",
                Status = BE.Enum.CustomerOrderStatus.Open,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 4, 3),
                ReleaseDate = new DateTime(2020, 4, 6),
                Area = BE.Enum.Area.Center,
                Type = BE.Enum.ResortType.Tent,
                Adults = 3,
                Children = 6,
                Pool = BE.Enum.Pool.NotInterested,
                Jacuzzi = BE.Enum.Jacuzzi.Optional,
                Garden = BE.Enum.Garden.Necessary,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                GuestRequestKey = ++Configuration.guestRequestKey,
                Spooky = BE.Enum.Spooky.ALittileBit,
            };


            GuestRequest g4 = new GuestRequest()
            {
                PrivateName = "yahoo",
                FamilyName = "cohen",
                MailAddress = "yahoo@gmail.com",
                Status = BE.Enum.CustomerOrderStatus.Open,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 2, 4),
                ReleaseDate = new DateTime(2020, 2, 6),
                Area = BE.Enum.Area.East,
                Type = BE.Enum.ResortType.Tent,
                Adults = 1,
                Children = 4,
                Pool = BE.Enum.Pool.NotInterested,
                Jacuzzi = BE.Enum.Jacuzzi.Optional,
                Garden = BE.Enum.Garden.Necessary,
                ChildrensAttractions = BE.Enum.ChildrensAttractions.Necessary,
                GuestRequestKey = ++Configuration.guestRequestKey,
                Spooky = BE.Enum.Spooky.ALittileBit
            };
            bl.AddGuestRequest(g1);
            bl.AddGuestRequest(g2);
            bl.AddGuestRequest(g3);
            bl.AddGuestRequest(g4);
            foreach (var item in bl.GetGuests())
            {
                Console.WriteLine("hosting unit: \n" + item + "\n");
            }

            g2.FamilyName = "Levi";
            bl.UpdateGuestRequest(g2);

            Console.WriteLine("Hosting Unit after changes: ");
            foreach (var item in bl.GetGuests())
            {
                Console.WriteLine("hosting unit: \n" + item + "\n");
            }


            Console.WriteLine("groups by amount of people: \n");
            var r = bl.GetAllGuestsRequestsGropuByAmountOfPeople();
            foreach (var item in r)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            Console.WriteLine("groups by area: \n");
            var b = bl.GetsAllGuestRequestsGroupsByArea();
            foreach (var item in b)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            #endregion

            #region Order

            Order o1 = new Order()
            {
                Status = BE.Enum.OrderStatus.NotYetAddressed,
                CreateDate = new DateTime(2019, 2, 1),
                OrderDate = new DateTime(2020, 4, 4),
                HostingUnitKey = h1.HostingUnitKey,
                GuestRequestKey = g2.GuestRequestKey
            };

            Order o2 = new Order()
            {
                Status = BE.Enum.OrderStatus.NotYetAddressed,
                CreateDate = new DateTime(2019, 4, 1),
                OrderDate = new DateTime(2020, 3, 4),
                HostingUnitKey = h2.HostingUnitKey,
                GuestRequestKey = g1.GuestRequestKey
            };

            Order o3 = new Order()
            {
                Status = BE.Enum.OrderStatus.NotYetAddressed,
                CreateDate = new DateTime(2019, 4, 1),
                OrderDate = new DateTime(2020, 4, 4),
                HostingUnitKey = h3.HostingUnitKey,
                GuestRequestKey = g4.GuestRequestKey
            };

            Order o4 = new Order()
            {
                Status = BE.Enum.OrderStatus.NotYetAddressed,
                CreateDate = new DateTime(2019, 2, 2),
                OrderDate = new DateTime(2020, 2, 4),
                HostingUnitKey = h4.HostingUnitKey,
                GuestRequestKey = g3.GuestRequestKey
            };

            Order o5 = new Order()
            {
                Status = BE.Enum.OrderStatus.NotYetAddressed,
                CreateDate = new DateTime(2019, 6, 1),
                OrderDate = new DateTime(2020, 6, 3),
                HostingUnitKey = h1.HostingUnitKey,
                GuestRequestKey = g2.GuestRequestKey
            };

            bl.AddOrder(o1);
            bl.AddOrder(o2);
            bl.AddOrder(o3);
            bl.AddOrder(o4);
            bl.AddOrder(o5);


            foreach (var item0 in bl.GetOrders())
            {
                Console.WriteLine("Order: \n" + item0 + "\n");
            }

            bl.RemoveHostingUnit(h5.HostingUnitKey);

            Console.WriteLine("after removing: ");
            foreach (var item1 in bl.GetHostingUnitCopy())
            {
                Console.WriteLine("Hosting Unit: \n" + item1 + "\n");
            }
            Console.WriteLine("amount of finalize: " + bl.AmountOfFinalizedOrders(h2));


            Console.WriteLine("amount of invetation: \n" + bl.AmountOfInvetations(g1));

            bl.UpdateOrder(o1);
            bl.UpdateOrder(o2);

            Console.WriteLine("after updating: \n");
            foreach (var item2 in bl.GetOrders())
            {
                Console.WriteLine("Order: \n" + item2 + "\n");
            }

            Console.WriteLine("amount of orders: ");
            foreach (var item7 in bl.AmountOfOrders(4))
            {
                Console.WriteLine("order: " + item7 + "\n");
            }

            #endregion

            foreach (var item3 in bl.GetBankBranches())
            {
                Console.WriteLine("Branch: " + item3);
            }

            DateTime d1 = new DateTime(2019, 3, 3);
            DateTime d2 = new DateTime(2019, 3, 10);
            Console.WriteLine("amount of days between 2 given days: " + bl.AmountOfDaysBetween(d1, d2));
            Console.WriteLine("amount of days between given day and today: " + bl.AmountOfDaysBetween(d2));
            Console.WriteLine("hosting units grouped by hosts");
            var l = bl.GetAllHostsGruopByAmountOfHostingUnits();
            foreach (var item4 in l)
            {
                Console.WriteLine(item4.Key);
                foreach (var w in item4)
                {
                    Console.WriteLine(w);
                }
            }

            
            foreach (var item5 in bl.CustomerDemands(x => x.Area == g1.Area))
            {
                Console.WriteLine("demands: \n" + item5);
            }

            Console.WriteLine("bye bye :P");
            Console.ReadKey();
        }

    }
}

/*hosting unit:
hosting unit key: 10000001
owner: host key: 315206482
private name: dan
family name: cohen
phone number: 0543198488
mail addess: dan@gmail.com
bank branch details:
bank number: 10000001
bank name: hapoalim
branch number: 18
brach address: beit hadfus 78
branch city: tlv

bank account number: 118833
collection clearnce: True

hosting unit name: apple
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020


hosting unit:
hosting unit key: 10000002
owner: host key: 209387125
private name: yosi
family name: cohen
phone number: 0543198488
mail addess: yosi@gmail.com
bank branch details:
bank number: 10000002
bank name: leumi
branch number: 19
brach address: beit hadfus 78
branch city: tlv

bank account number: 912233
collection clearnce: True

hosting unit name: pear
dates that are occupied: First date: 5/9/2020  lastDate: 7/9/2020


hosting unit:
hosting unit key: 10000003
owner: host key: 318336062
private name: ron
family name: cohen
phone number: 0543198488
mail addess: ron@gmail.com
bank branch details:
bank number: 10000003
bank name: leumi
branch number: 14
brach address: beit hadfus 78
branch city: tlv

bank account number: 141414
collection clearnce: True

hosting unit name: orange
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020


hosting unit:
hosting unit key: 10000004
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000004
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: grape
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020


hosting unit:
hosting unit key: 10000005
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000005
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: olive
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020


Hosting Unit:
hosting unit key: 10000001
owner: host key: 315206482
private name: dan
family name: cohen
phone number: 0543198488
mail addess: dan@gmail.com
bank branch details:
bank number: 10000001
bank name: hapoalim
branch number: 18
brach address: beit hadfus 78
branch city: tlv

bank account number: 118833
collection clearnce: True

hosting unit name: apple
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020


Hosting Unit:
hosting unit key: 10000002
owner: host key: 209387125
private name: yosi
family name: cohen
phone number: 0543198488
mail addess: yosi@gmail.com
bank branch details:
bank number: 10000002
bank name: leumi
branch number: 19
brach address: beit hadfus 78
branch city: tlv

bank account number: 912233
collection clearnce: True

hosting unit name: pear
dates that are occupied: First date: 5/9/2020  lastDate: 7/9/2020


Hosting Unit:
hosting unit key: 10000003
owner: host key: 318336062
private name: ron
family name: cohen
phone number: 0543198488
mail addess: ron@gmail.com
bank branch details:
bank number: 10000003
bank name: leumi
branch number: 14
brach address: beit hadfus 78
branch city: tlv

bank account number: 141414
collection clearnce: True

hosting unit name: orange
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020


Hosting Unit:
hosting unit key: 10000005
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000005
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: olive
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020


Hosting Unit:
hosting unit key: 10000004
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000004
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: banana
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020


hosting unit group by area:

Center
hosting unit key: 10000001
owner: host key: 315206482
private name: dan
family name: cohen
phone number: 0543198488
mail addess: dan@gmail.com
bank branch details:
bank number: 10000001
bank name: hapoalim
branch number: 18
brach address: beit hadfus 78
branch city: tlv

bank account number: 118833
collection clearnce: True

hosting unit name: apple
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020

hosting unit key: 10000003
owner: host key: 318336062
private name: ron
family name: cohen
phone number: 0543198488
mail addess: ron@gmail.com
bank branch details:
bank number: 10000003
bank name: leumi
branch number: 14
brach address: beit hadfus 78
branch city: tlv

bank account number: 141414
collection clearnce: True

hosting unit name: orange
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020

East
hosting unit key: 10000002
owner: host key: 209387125
private name: yosi
family name: cohen
phone number: 0543198488
mail addess: yosi@gmail.com
bank branch details:
bank number: 10000002
bank name: leumi
branch number: 19
brach address: beit hadfus 78
branch city: tlv

bank account number: 912233
collection clearnce: True

hosting unit name: pear
dates that are occupied: First date: 5/9/2020  lastDate: 7/9/2020

hosting unit key: 10000005
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000005
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: olive
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020

hosting unit key: 10000004
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000004
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: banana
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020

hosting unit #1:
hosting unit key: 10000001
owner: host key: 315206482
private name: dan
family name: cohen
phone number: 0543198488
mail addess: dan@gmail.com
bank branch details:
bank number: 10000001
bank name: hapoalim
branch number: 18
brach address: beit hadfus 78
branch city: tlv

bank account number: 118833
collection clearnce: True

hosting unit name: apple
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020

hosting unit #2:
hosting unit key: 10000002
owner: host key: 209387125
private name: yosi
family name: cohen
phone number: 0543198488
mail addess: yosi@gmail.com
bank branch details:
bank number: 10000002
bank name: leumi
branch number: 19
brach address: beit hadfus 78
branch city: tlv

bank account number: 912233
collection clearnce: True

hosting unit name: pear
dates that are occupied: First date: 5/9/2020  lastDate: 7/9/2020

hosting unit #3:
hosting unit key: 10000003
owner: host key: 318336062
private name: ron
family name: cohen
phone number: 0543198488
mail addess: ron@gmail.com
bank branch details:
bank number: 10000003
bank name: leumi
branch number: 14
brach address: beit hadfus 78
branch city: tlv

bank account number: 141414
collection clearnce: True

hosting unit name: orange
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020

hosting unit #4:
hosting unit key: 10000005
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000005
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: olive
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020

hosting unit #5:
hosting unit key: 10000004
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000004
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: banana
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020

hosting unit:
guest request number: 10000005
private name: yosi
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Hotel
adults: 4
children: 5
pool: Necessary
jacuzzi: NotInterested
garden: NotInterested
childrens attractions: Necessary


hosting unit:
guest request number: 10000006
private name: ron
family name: cohen
mail address: ron@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 04/03/2020 0:00:00
release date: 06/03/2020 0:00:00
area: East
type: Tzimer
adults: 3
children: 2
pool: Necessary
jacuzzi: NotInterested
garden: Optional
childrens attractions: Necessary


hosting unit:
guest request number: 10000007
private name: dan
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Tent
adults: 3
children: 6
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary


hosting unit:
guest request number: 10000008
private name: yahoo
family name: cohen
mail address: yahoo@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 04/02/2020 0:00:00
release date: 06/02/2020 0:00:00
area: East
type: Tent
adults: 1
children: 4
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary


Hosting Unit after changes:
hosting unit:
guest request number: 10000005
private name: yosi
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Hotel
adults: 4
children: 5
pool: Necessary
jacuzzi: NotInterested
garden: NotInterested
childrens attractions: Necessary


hosting unit:
guest request number: 10000007
private name: dan
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Tent
adults: 3
children: 6
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary


hosting unit:
guest request number: 10000008
private name: yahoo
family name: cohen
mail address: yahoo@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 04/02/2020 0:00:00
release date: 06/02/2020 0:00:00
area: East
type: Tent
adults: 1
children: 4
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary


hosting unit:
guest request number: 10000006
private name: ron
family name: Levi
mail address: ron@gmail.com
status: ClosedThroughWebsite
registration date: 01/01/2020 0:00:00
entry date: 04/03/2020 0:00:00
release date: 06/03/2020 0:00:00
area: East
type: Tzimer
adults: 3
children: 2
pool: Necessary
jacuzzi: NotInterested
garden: Optional
childrens attractions: Necessary


groups by amount of people:

9
guest request number: 10000005
private name: yosi
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Hotel
adults: 4
children: 5
pool: Necessary
jacuzzi: NotInterested
garden: NotInterested
childrens attractions: Necessary

guest request number: 10000007
private name: dan
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Tent
adults: 3
children: 6
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary

5
guest request number: 10000008
private name: yahoo
family name: cohen
mail address: yahoo@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 04/02/2020 0:00:00
release date: 06/02/2020 0:00:00
area: East
type: Tent
adults: 1
children: 4
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary

guest request number: 10000006
private name: ron
family name: Levi
mail address: ron@gmail.com
status: ClosedThroughWebsite
registration date: 01/01/2020 0:00:00
entry date: 04/03/2020 0:00:00
release date: 06/03/2020 0:00:00
area: East
type: Tzimer
adults: 3
children: 2
pool: Necessary
jacuzzi: NotInterested
garden: Optional
childrens attractions: Necessary

groups by area:

Center
guest request number: 10000005
private name: yosi
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Hotel
adults: 4
children: 5
pool: Necessary
jacuzzi: NotInterested
garden: NotInterested
childrens attractions: Necessary

guest request number: 10000007
private name: dan
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Tent
adults: 3
children: 6
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary

East
guest request number: 10000008
private name: yahoo
family name: cohen
mail address: yahoo@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 04/02/2020 0:00:00
release date: 06/02/2020 0:00:00
area: East
type: Tent
adults: 1
children: 4
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary

guest request number: 10000006
private name: ron
family name: Levi
mail address: ron@gmail.com
status: ClosedThroughWebsite
registration date: 01/01/2020 0:00:00
entry date: 04/03/2020 0:00:00
release date: 06/03/2020 0:00:00
area: East
type: Tzimer
adults: 3
children: 2
pool: Necessary
jacuzzi: NotInterested
garden: Optional
childrens attractions: Necessary

An email was sent
An email was sent
An email was sent
An email was sent
An email was sent
Order:
hosting unit key: 10000001
guest request key: 10000006
order key: 10000001
status: AnEmailWasSent
create date: 01/02/2019 0:00:00
order date: 04/04/2020 0:00:00


Order:
hosting unit key: 10000002
guest request key: 10000005
order key: 10000002
status: AnEmailWasSent
create date: 01/04/2019 0:00:00
order date: 04/03/2020 0:00:00


Order:
hosting unit key: 10000003
guest request key: 10000008
order key: 10000003
status: AnEmailWasSent
create date: 01/04/2019 0:00:00
order date: 04/04/2020 0:00:00


Order:
hosting unit key: 10000004
guest request key: 10000007
order key: 10000004
status: AnEmailWasSent
create date: 02/02/2019 0:00:00
order date: 04/02/2020 0:00:00


Order:
hosting unit key: 10000001
guest request key: 10000006
order key: 10000005
status: AnEmailWasSent
create date: 01/06/2019 0:00:00
order date: 03/06/2020 0:00:00


after removing:
Hosting Unit:
hosting unit key: 10000001
owner: host key: 315206482
private name: dan
family name: cohen
phone number: 0543198488
mail addess: dan@gmail.com
bank branch details:
bank number: 10000001
bank name: hapoalim
branch number: 18
brach address: beit hadfus 78
branch city: tlv

bank account number: 118833
collection clearnce: True

hosting unit name: apple
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020


Hosting Unit:
hosting unit key: 10000002
owner: host key: 209387125
private name: yosi
family name: cohen
phone number: 0543198488
mail addess: yosi@gmail.com
bank branch details:
bank number: 10000002
bank name: leumi
branch number: 19
brach address: beit hadfus 78
branch city: tlv

bank account number: 912233
collection clearnce: True

hosting unit name: pear
dates that are occupied: First date: 5/9/2020  lastDate: 7/9/2020


Hosting Unit:
hosting unit key: 10000003
owner: host key: 318336062
private name: ron
family name: cohen
phone number: 0543198488
mail addess: ron@gmail.com
bank branch details:
bank number: 10000003
bank name: leumi
branch number: 14
brach address: beit hadfus 78
branch city: tlv

bank account number: 141414
collection clearnce: True

hosting unit name: orange
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020


Hosting Unit:
hosting unit key: 10000004
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000004
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: banana
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020


amount of finalize: 5
amount of invetation:
1
after updating:

Order:
hosting unit key: 10000001
guest request key: 10000006
order key: 10000001
status: ClosedDueToCostumerResponse
create date: 01/02/2019 0:00:00
order date: 04/04/2020 0:00:00


Order:
hosting unit key: 10000002
guest request key: 10000005
order key: 10000002
status: ClosedDueToCostumerResponse
create date: 01/04/2019 0:00:00
order date: 04/03/2020 0:00:00


Order:
hosting unit key: 10000003
guest request key: 10000008
order key: 10000003
status: AnEmailWasSent
create date: 01/04/2019 0:00:00
order date: 04/04/2020 0:00:00


Order:
hosting unit key: 10000004
guest request key: 10000007
order key: 10000004
status: AnEmailWasSent
create date: 02/02/2019 0:00:00
order date: 04/02/2020 0:00:00


Order:
hosting unit key: 10000001
guest request key: 10000006
order key: 10000005
status: AnEmailWasSent
create date: 01/06/2019 0:00:00
order date: 03/06/2020 0:00:00


amount of orders:
order: hosting unit key: 10000001
guest request key: 10000006
order key: 10000001
status: ClosedDueToCostumerResponse
create date: 01/02/2019 0:00:00
order date: 04/04/2020 0:00:00


order: hosting unit key: 10000002
guest request key: 10000005
order key: 10000002
status: ClosedDueToCostumerResponse
create date: 01/04/2019 0:00:00
order date: 04/03/2020 0:00:00


order: hosting unit key: 10000003
guest request key: 10000008
order key: 10000003
status: AnEmailWasSent
create date: 01/04/2019 0:00:00
order date: 04/04/2020 0:00:00


order: hosting unit key: 10000004
guest request key: 10000007
order key: 10000004
status: AnEmailWasSent
create date: 02/02/2019 0:00:00
order date: 04/02/2020 0:00:00


order: hosting unit key: 10000001
guest request key: 10000006
order key: 10000005
status: AnEmailWasSent
create date: 01/06/2019 0:00:00
order date: 03/06/2020 0:00:00


Branch: bank number: 10000011
bank name: leumi
branch number: 13
brach address: beit hadfus 7
branch city: jlm

Branch: bank number: 10000012
bank name: leumi
branch number: 14
brach address: beit hadfus 12
branch city: jlm

Branch: bank number: 10000013
bank name: leumi
branch number: 13
brach address: beit hadfus 13
branch city: jlm

Branch: bank number: 10000014
bank name: leumi
branch number: 114
brach address: beit hadfus 114
branch city: tlv

Branch: bank number: 10000015
bank name: leumi
branch number: 115
brach address: beit hadfus 115
branch city: haifa

amount of days between 2 given days: 7
amount of days between given day and today: 296
hosting units grouped by hosts
1
hosting unit key: 10000001
owner: host key: 315206482
private name: dan
family name: cohen
phone number: 0543198488
mail addess: dan@gmail.com
bank branch details:
bank number: 10000001
bank name: hapoalim
branch number: 18
brach address: beit hadfus 78
branch city: tlv

bank account number: 118833
collection clearnce: True

hosting unit name: apple
dates that are occupied: First date: 4/3/2020  lastDate: 6/3/2020
First date: 5/5/2020  lastDate: 7/5/2020

hosting unit key: 10000002
owner: host key: 209387125
private name: yosi
family name: cohen
phone number: 0543198488
mail addess: yosi@gmail.com
bank branch details:
bank number: 10000002
bank name: leumi
branch number: 19
brach address: beit hadfus 78
branch city: tlv

bank account number: 912233
collection clearnce: True

hosting unit name: pear
dates that are occupied: First date: 3/4/2020  lastDate: 6/4/2020
First date: 5/9/2020  lastDate: 7/9/2020

hosting unit key: 10000003
owner: host key: 318336062
private name: ron
family name: cohen
phone number: 0543198488
mail addess: ron@gmail.com
bank branch details:
bank number: 10000003
bank name: leumi
branch number: 14
brach address: beit hadfus 78
branch city: tlv

bank account number: 141414
collection clearnce: True

hosting unit name: orange
dates that are occupied: First date: 5/5/2020  lastDate: 7/5/2020

hosting unit key: 10000004
owner: host key: 29567567
private name: yael shoshana
family name: chaya
phone number: 0543198488
mail addess: yalla@gmail.com
bank branch details:
bank number: 10000004
bank name: leumi
branch number: 16
brach address: beit hadfus 78
branch city: tlv

bank account number: 112236
collection clearnce: True

hosting unit name: banana
dates that are occupied: First date: 5/3/2020  lastDate: 7/3/2020

demands:
guest request number: 10000005
private name: yosi
family name: cohen
mail address: yosi@gmail.com
status: ClosedThroughWebsite
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Hotel
adults: 4
children: 5
pool: Necessary
jacuzzi: NotInterested
garden: NotInterested
childrens attractions: Necessary

demands:
guest request number: 10000007
private name: dan
family name: cohen
mail address: yosi@gmail.com
status: Open
registration date: 01/01/2020 0:00:00
entry date: 03/04/2020 0:00:00
release date: 06/04/2020 0:00:00
area: Center
type: Tent
adults: 3
children: 6
pool: NotInterested
jacuzzi: Optional
garden: Necessary
childrens attractions: Necessary

bye bye :P
 */
