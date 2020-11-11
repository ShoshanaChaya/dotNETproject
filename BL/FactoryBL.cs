using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class FactoryBL
    {
        static BL_imp bl = new BL_imp();
        public static IBL getBL()
        {
            return bl;
        }
    }
}