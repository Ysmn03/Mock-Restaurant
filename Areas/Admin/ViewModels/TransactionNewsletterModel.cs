using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class TransactionNewsletterModel : BaseModel
    {
        [Required]
        public int TransactionNewsletterId { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DisplayName("Email")]
        public string? TransactionNewsletterEmail { get; set; }
    }
}
