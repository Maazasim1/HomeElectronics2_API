using System;
namespace test.Models
{
    public class StockDetails
    {
        public int ID { get; set; }
        public string ItemBrand { get; set; }
        public string ItemType { get; set; }
        public string Item_ModelNumber { get; set; }
        public float UnitPrice { get; set; }
        public string WareHouseID { get; set; }
        public int Quantity { get; set; }
        public string StockAdd_Datetime { get; set; }
    }
}