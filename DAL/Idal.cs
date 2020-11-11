using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
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
        /// <param name="GuestRequestKey"></param>
        /// <returns></returns> 
        void UpdateGuestRequest(GuestRequest guestRequest);

        /// <summary>
        /// Updates the information of the guest
        /// </summary>
        /// <param name="GuestRequestKey"></param>
        /// <returns></returns> 
        void UpdateGuestRequestInfo(GuestRequest guestRequest);

        /// <summary>
        /// Updates the code of the guest
        /// </summary>
        /// <param name="GuestRequestKey"></param>
        /// <returns></returns> 
        void UpdateGuestRequestCode(GuestRequest guestRequest);

        /// <summary>
        /// Get all guests
        /// </summary>
        /// <returns></returns>
        IEnumerable<GuestRequest> GetGuests();

        /// <summary>
        /// Removing a GuestRequest
        /// </summary>
        /// <param name="hostingUnitKey"></param>
        void RemoveGuestRequest(GuestRequest guestRequest);
        #endregion

        #region Hosting Unit
        /// <summary>
        /// Add hostingUnit to the DataBase
        /// </summary>
        /// <param name="hostingUnit"></param>
        void AddHostingUnit(HostingUnit hostingUnit);

        /// <summary>
        /// Removing a hosting unit according to the hosting unit key 
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
        /// Get All HostingUnits
        /// </summary>
        /// <returns></returns>
        IEnumerable<HostingUnit> GetHostingUnitCopy();

        /// <summary>
        /// update the diary of each hosting unit
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns> 
        void UpdateDiary();

        /// <summary>
        /// Get All Hosts
        /// </summary>
        /// <returns></returns>
        IEnumerable<Host> GetHosts();

        //void AddUpdate(HostingUnit hostingUnit);


        #endregion

        #region Order
        /// <summary>
        /// Add order to the DataBase
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(Order order);

        /// <summary>
        /// Get deep clone of order by Order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns> 
        void UpdateOrder(Order order);

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetOrders();

        /// <summary>
        /// update the status of each order
        /// </summary>
        /// <returns></returns>
        void UpdateStatus();
        #endregion

        #region Bank Branch
        /// <summary>
        /// Get all bankBranches
        /// </summary>
        /// <returns></returns>
        List<BankBranch> GetBankBranches();
        #endregion

        #region Host
        /// <summary>
        /// Add host to the DataBase
        /// </summary>
        void AddHost(Host host);

        /// <summary>
        /// Get deep clone of host by Host.
        /// </summary>
        void UpdateHost(Host host);

        /// <summary>
        /// Removing a host
        /// </summary>
        void RemoveHost(Host host);
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
        /// removes a comment
        /// </summary>
        void RemoveReviewFromGuset(messages m);

        /// <summary>
        /// removes a comment from the guests to see
        /// </summary>
        void RemoveReviewToGuset(messages m);

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