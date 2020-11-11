using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Enum
    {
        public enum ResortType
        {
            Tzimer,
            AccommodationApartment,
            Hotel,
            Tent
        }
        public enum Area
        {
            North,
            South,
            West,
            East,
            Center
        }
        public enum OrderStatus
        {
            NotYetAddressed,
            AnEmailWasSent,
            ClosedDueToCostumerLackOfResponse,
            ClosedDueToCostumerResponse,
            ClosedDueToCostumerBooked
        }
        public enum CustomerOrderStatus
        {
            Open,
            ClosedThroughWebsite,
            OutOfDate,
            CannotAcceptTheOrder
        }
        public enum Pool
        {
            Necessary,
            Optional,
            NotInterested
        }
        public enum Jacuzzi
        {
            Necessary,
            Optional,
            NotInterested
        }
        public enum Garden
        {
            Necessary,
            Optional,
            NotInterested
        }
        public enum ChildrensAttractions
        {
            Necessary,
            Optional,
            NotInterested
        }

        public enum Spooky
        {
            NoWay,
            ALittileBit,
            TerrifyMe
        }
        public enum GroupOptions
        {
            Area,
            Type,
            Spooky,
            pool,
            Jacuzzi,
            Graden,
            ChildrenAttractions,
            All
        }
        public enum managerOptions
        {
            Hosts,
            Orders,
            GuestRequestByArea,
            GuestRequestByAmountOfPeople,
            HostingUnits
        }
        public enum typeOfOrders
        {
           Finalized,
           NotFinalizedYet
        }
        public enum reviewsOptions
        {
            ReviewsFromGuests,
            ReviewsToGuests,
            ReviewsFromHosts
        }
    }
}