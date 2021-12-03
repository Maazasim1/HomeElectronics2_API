using System;
namespace test.Models
{
    public class StockTransfer
    {
        public string ItemModelNumber { get; set; }
        public int From_LocationID { get; set; }
        public int To_LocationID { get; set; }
        public int Stock_TrasferDetailsID { get; set; }
        public int Quantity { get; set; }
    }
}
