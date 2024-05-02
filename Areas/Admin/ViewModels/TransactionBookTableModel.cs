using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class TransactionBookTableModel :BaseModel
    {
        [Required]
        public int TransactionBookTableId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [DisplayName("Name")]
        public string? TransactionBookTableFullName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DisplayName("Email")]
        public string? TransactionBookTableEmail { get; set; }
        [Required(ErrorMessage = "Mobile Number Is Required")]
        [DisplayName("Phone Number")]
        public string? TransactionBookTableMobileNumber { get; set; }
        [Required(ErrorMessage = "Date Is Required")]
        [DisplayName("Date")]
        public string? TransactionBookTableDate { get; set; }
    }
}
