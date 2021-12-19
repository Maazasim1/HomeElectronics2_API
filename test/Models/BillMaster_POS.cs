using System;
namespace test.Models
{
    public class BillMaster_POS
    {
        public int BillMasterNO { get; set; }   
        public string BillCreatedBy { get; set; }
        public string BillCreatedOn { get; set; }
        public string BillModifiedOn { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string DeliveryCharges { get; set; }
        public string InstallationChares { get; set; }
        public int totalAmount { get; set; }
        
        public string SaleType { get; set; }

        public string MethodPayment { get; set; }

        public string ChequeNumber { get; set; }

        public string BankName { get; set; }


    }
}
