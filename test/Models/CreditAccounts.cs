using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models
{
    public class CreditAccounts
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public int BillMasterID { get; set; }
        public float InitialCredited { get; set; }
        public float AmountDue { get; set; }

        public string CustomerPhoneNumber { get; set; }

    }
}
