using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Host
    {
        public override string ToString()
        {
            return "Host key: " + HostKey + Environment.NewLine + "First name: " + PrivateName + Environment.NewLine + "Family name: " + FamilyName + Environment.NewLine +
                "Phone number: " + PhoneNumber + Environment.NewLine + "Mail addess: " + MailAddress + Environment.NewLine + Environment.NewLine + "Bank branch details: " + "\n" + BankBranchDetails + "Bank account number: " + BankAccountNumber + Environment.NewLine +
                "Collection clearnce: " + CollectionClearance + Environment.NewLine;
        }
        // properties:
        public string HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public long BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }
        public int amountOfHostingUnits { get; set; }
        public string fullName { get; set; }
    }
}
