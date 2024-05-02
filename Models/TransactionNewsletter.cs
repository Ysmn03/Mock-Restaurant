using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class TransactionNewsletter :BaseModel
    {
        public int TransactionNewsletterId { get; set; }
        public string? TransactionNewsletterEmail { get; set; }
    }
}
