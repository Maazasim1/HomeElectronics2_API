using System;
namespace test.Models
{
    public class DoubleEntryMaster
    {
     
            public int DoubleEntryID { get; set; }
            public string TransactionDate { get; set; }
        public string CreatedBy { get; set; }
        public int CreditAccount { get; set; }
        public float CreditAmount { get; set; }
        public int DebitAccount { get; set; }
        public int DebitAmount { get; set; }
        public string Narration { get; set; }
        
            
    
    
      }
}

