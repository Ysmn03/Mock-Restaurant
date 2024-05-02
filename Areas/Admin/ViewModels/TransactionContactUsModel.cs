using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class TransactionContactUsModel
    {
        [Required]
        public int TransactionContactUsId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [DisplayName("Name")]
        public string? TransactionContactUsFullName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DisplayName("Email")]
        public string? TransactionContactUsEmail { get; set; }
        [Required(ErrorMessage = "Subject Is Required")]
        [DisplayName("Subject")]
        public string? TransactionContactUsSubject { get; set; }
        [Required(ErrorMessage = "Message Is Required")]
        [DisplayName("Message")]
        public string? TransactionContactUsMessage { get; set; }
    }
}
