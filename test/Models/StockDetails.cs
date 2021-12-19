using System;
namespace test.Models
{
    public class StockDetails
    {
        
        public string Item_ModelNumber { get; set; }
        public float UnitPrice { get; set; }
        public string WareHouseID { get; set; }
        public int Quantity { get; set; }
        public string StockAdd_Datetime { get; set; }
    }
}