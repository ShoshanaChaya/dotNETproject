using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class BankBranch
    {
        public override string ToString()
        {
            return "Bank number: " + BankNumber + Environment.NewLine + "Bank name: " + BankName + Environment.NewLine + "Branch number: " + BranchNumber + Environment.NewLine
                + "Brach address: " + BranchAddress + Environment.NewLine + "Branch city: " + BranchCity + Environment.NewLine;
        }
        //properties:
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
    }
}
