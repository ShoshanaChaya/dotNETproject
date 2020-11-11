using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Order
    {
        public override string ToString()
        {
            return "Hosting unit key: " + HostingUnitKey + Environment.NewLine + "Guest request key: " + GuestRequestKey + Environment.NewLine + "Order key: "
                + OrderKey + Environment.NewLine + "Status: " + Status + Environment.NewLine + "Create date: " + CreateDate + Environment.NewLine + "Order date: " + OrderDate + Environment.NewLine;
        }
        // properties:
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        public long OrderKey { get; set; }
        public Enum.OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string HostingUnitName { get; set; }
    }
}
