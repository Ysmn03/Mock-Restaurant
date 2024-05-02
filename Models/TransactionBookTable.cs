using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class TransactionBookTable :BaseModel
    {
        public int TransactionBookTableId { get; set; }
        public string? TransactionBookTableFullName { get; set; }
        public string? TransactionBookTableEmail { get; set; }
        public string? TransactionBookTableMobileNumber { get; set; }
        public string? TransactionBookTableDate { get; set; }
    }
}
