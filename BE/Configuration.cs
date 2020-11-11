using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static long guestRequestKey = 10000003;
        public static int bankNumber = 10000000;
        //public static long hostKey = 10000000;
        public static long hostingUnitKey = 10000006;
        public static long orderKey = 10000003;
        public static int fee = 10;
        public static int amountOfDaysUntilOutOfDate = 30;
        public static int gains = 0;
        public static int amountOfOrders = 0;
        public static string password = "1234";
        public static List<messages> reviewsFromGuests = new List<messages>();
        public static List<messages> commentsFromHost = new List<messages>();
        public static List<messages> reviewsToGuests = new List<messages>();
    }
}