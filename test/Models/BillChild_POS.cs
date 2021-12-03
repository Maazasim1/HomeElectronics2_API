using System;
namespace test.Models
{
    public class BillChild_POS
    {
        public int BillMasterID { get; set; }
        public string ItemSKU { get; set; }
        public string ItemBrand { get; set; }
        public string ItemType { get; set; }
        public int ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
    }
}
