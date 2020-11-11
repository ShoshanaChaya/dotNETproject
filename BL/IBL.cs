using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        #region Guest Request
        /// <summary>
        /// add guestRequest to the DataBase
        /// </summary>
        /// <param name="guestRequest"></param>
        void AddGuestRequest(GuestRequest guestRequest);

        /// <summary>
        /// Get deep clone of GuestRequest by GuestRequestKey
        /// </summary>
        /// <param name="guestReques"></param>
        void UpdateGuestRequest(GuestRequest guestRequest);
        
        /// <summary>
        /// Updates the information of the guest
        /// </summary>
        /// <param name="guestReques"></param>
        void UpdateGuestRequestInfo(GuestRequest guestRequest);

        /// <summary>
        /// Updates the code of the guest
        /// </summary>
        /// <param name="guestReques"></param>
        void UpdateGuestRequestCode(GuestRequest guestRequest);

        /// <summary>
        /// Calculates which area has the most booking
        /// </summary>
        string TheMostWantedArea();

        /// <summary>
        /// Calculates which type of hosting unit is most booked
        /// </summary>
        string TheMostWantedTypeOfHostingUnit();

        /// <summary>
        /// Removing a GuestRequest
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        void RemoveGuestRequest(GuestRequest guestRequest);

        /// <summary>
        /// Calculates the amount of guests
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        int AmountOfPeople(GuestRequest guestRequest);

        /// <summary>
        /// Calculates the amount of vacation days
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        int amountOfVacationDays(GuestRequest guestRequest);
        #endregion

        #region Hosting Unit
        /// <summary>
        /// add hostingUnit to the DataBase
        /// </summary>
        /// <param name="hostingUnit"></param>
        void AddHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// removing a hosting unit according to the hosting unit key 
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        void RemoveHostingUnit(long hostingUnitKey);

        /// <summary>
        /// Get deep clone of hostingUnit by HostingUnit.
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns> 
        void UpdateHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// update the diary of each hosting unit
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns> 
        void UpdateDiary();

        /// <summary>
        /// the dates of the guest request is invalid
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns> 
        bool InvalidDates(GuestRequest guestRequest);
        #endregion

        #region Order
        /// <summary>
        /// add order to the DataBase
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(Order order);

        /// <summary>
        /// Get deep clone of order by Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns> 
        void UpdateOrder(Order order);

        /// <summary>
        /// update the status of each order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns> 
        void UpdateStatus();
        #endregion

        #region Get Lists
        /// <summary>
        /// Get All HostingUnits
        /// </summary>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetHostingUnitCopy();

        /// <summary>
        /// Get All Guests
        /// </summary>
        /// <returns></returns>
        IEnumerable<GuestRequest> GetGuests();

        /// <summary>
        /// Get All Hosts
        /// </summary>
        /// <returns></returns>
        IEnumerable<Host> GetHosts();

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetOrders();

        /// <summary>
        /// Get All BankBranches
        /// </summary>
        /// <returns></returns>
        List<BankBranch> GetBankBranches();

        /// <summary>
        /// get all availabe hosting units in the required period
        /// </summary>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetAvailableDays(DateTime date, int numOfDays);
        #endregion

        #region Function Which Calculate
        /// <summary>
        /// calculates the amount of days between the two days or between the given date until today
        /// </summary>
        /// <returns></returns>
        int AmountOfDaysBetween(params DateTime[] dates);

        /// <summary>
        /// checks if the amount of days since the orders were required are bigger or equals to the given amount of days
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> AmountOfOrders(int numOfDays);

        /// <summary>
        /// returns all customers requests which fit to a particular condition
        /// </summary>
        /// <returns></returns>
        IEnumerable<GuestRequest> CustomerDemands(Func<GuestRequest, bool> predicate = null);

        /// <summary>
        /// returns amount of invetations that the client received
        /// </summary>
        /// <returns></returns>
        int AmountOfInvetations(GuestRequest guestRequest);

        /// <summary>
        /// returns the amount of orders which were finalized successfully
        /// </summary>
        /// <returns></returns>
        int AmountOfFinalizedOrders(HostingUnit hostingUnit);

        /// <summary>
        /// returns the amount of orders that were finalized every day
        /// </summary>
        /// <returns></returns>
        int AmountOfOrdersPerDay(HostingUnit hostingUnit);
        #endregion

        #region Grouping
        /// <summary>
        /// gets all guest requests and Groups by area
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<BE.Enum.Area, GuestRequest>> GetsAllGuestRequestsGroupsByArea();

        /// <summary>
        /// gets all guest requests and Groups by amount of people
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<int, GuestRequest>> GetAllGuestsRequestsGropuByAmountOfPeople();

        /// <summary>
        /// gets all hosts and Groups by amount of hosting units
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<int, Host>> GetAllHostsGruopByAmountOfHostingUnits();

        /// <summary>
        /// gets all hosting units and Groups by area
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupByArea(string a, List<HostingUnit> list);

        /// <summary>
        /// gets all hosting units and Groups by type
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupByType(string a, List<HostingUnit> list);

        /// <summary>
        /// gets all hosting units and Groups by requirment for pool
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupByPool(string a, List<HostingUnit> list);

        /// <summary>
        /// gets all hosting units and Groups by requirment for jacuzzi
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupByJacuzzi(string a, List<HostingUnit> list);

        /// <summary>
        /// gets all hosting units and Groups by requirment for garden
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupByGarden(string a, List<HostingUnit> list);

        /// <summary>
        /// gets all hosting units and Groups by requirment for Children attractions
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupByChildrenAttractions(string a, List<HostingUnit> list);

        /// <summary>
        /// gets all hosting units and Groups by requirment for spooky
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetAllHostingUnitsGroupBySpooky(string a, List<HostingUnit> list);

        /// <summary>
        /// gets a list and sorts by a delegate
        /// </summary>
        /// <returns></returns>
        IEnumerable<IGrouping<Func<HostingUnit, T>, HostingUnit>> orderByHostingUnit<T>(List<HostingUnit> list, Func<HostingUnit, T> predicat = null);

        #endregion

        #region Host
        /// <summary>
        /// Add host to the DataBase
        /// </summary>
        void AddHost(Host host);

        /// <summary>
        /// Get deep clone of host by Host.
        /// </summary>
        /// <returns></returns> 
        void UpdateHost(Host host);

        /// <summary>
        /// Removing a host
        /// </summary>
        void RemoveHost(Host host);

        /// <summary>
        /// Returns the fullName
        /// </summary>
        string getFullName(Host host);
        #endregion

        #region Image
        /// <summary>
        /// Adds an image
        /// </summary>
        void AddImage(string id, string newImagePath);

        /// <summary>
        /// Gets an image
        /// </summary>
        string getImagePath(string name);
        #endregion

        #region MoreFunctions
        /// <summary>
        /// starts the thread
        /// </summary>
        void startThread();

        /// <summary>
        /// closes all orders that are out of date
        /// </summary>
        void ResetOrders();
        #endregion

        #region Reviews
        /// <summary>
        /// adds a review from the guest
        /// </summary>
        void AddReviewFromGuest(messages m);

        /// <summary>
        /// adds a review to the guests to see
        /// </summary>
        void AddReviewToGuest(messages m);

        /// <summary>
        /// add a review from the host
        /// </summary>
        void AddCommentFromHost(messages m);

        /// <summary>
        /// removes a comment from the guests to see
        /// </summary>
        void RemoveReviewToGuset(messages m);

        /// <summary>
        /// removes a comment
        /// </summary>
        void RemoveReviewFromGuset(messages m);

        /// <summary>
        /// removes a comment from the host
        /// </summary>
        void RemoveCommentFromHost(messages m);

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        IEnumerable<messages> GetReviewsFromGuests();

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        IEnumerable<messages> GetReviewsToGuests();

        /// <summary>
        /// Get all reviews
        /// </summary>
        /// <returns></returns>
        IEnumerable<messages> GetCommentsFromHosts();

        #endregion
    }
}
