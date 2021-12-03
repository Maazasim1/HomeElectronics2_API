using System;
namespace test.Models
{
    public class UserRights
    {
       public int UserTypeID { get; set; }
        public bool Allowsale { get; set; }
        public bool AllowStockTransfer { get; set; }
    }
}
