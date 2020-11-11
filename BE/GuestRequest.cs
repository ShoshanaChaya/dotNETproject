using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class GuestRequest
    {
        public override string ToString()
        {
            return "Guest request number: " + GuestRequestKey + Environment.NewLine + "First name: " + PrivateName + Environment.NewLine + "Family name: " + FamilyName + Environment.NewLine +
                "Mail address: " + MailAddress + Environment.NewLine + "Status: " + Status + Environment.NewLine + "Registration date: " + RegistrationDate + Environment.NewLine + "Entry date: " +
                EntryDate + Environment.NewLine + "Release date: " + ReleaseDate + Environment.NewLine + "Area: " + Area + Environment.NewLine +
                "Type: " + Type + Environment.NewLine + "Adults: " + Adults + Environment.NewLine + "Children: " + Children + Environment.NewLine + "Pool: " + Pool + Environment.NewLine + "Jacuzzi: " + Jacuzzi +
                Environment.NewLine + "Garden: " + Garden + Environment.NewLine + "Childrens attractions: " + ChildrensAttractions + Environment.NewLine;
        }
        // properties:
        public long GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public Enum.CustomerOrderStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public Enum.Area Area { get; set; }
        public Enum.ResortType Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; } 
        public Enum.Pool Pool { get; set; }
        public Enum.Jacuzzi Jacuzzi { get; set; }
        public Enum.Garden Garden { get; set; }
        public Enum.ChildrensAttractions ChildrensAttractions { get; set; }
        public Enum.Spooky Spooky { get; set; }
        public string Code { get; set; }
        public int amount { get; set; }
        public int amountOFPeople { get; set; }
    }
}