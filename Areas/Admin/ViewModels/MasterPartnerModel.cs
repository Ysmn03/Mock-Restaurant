using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterPartnerModel:BaseModel
    {
        [Required]
        public int MasterPartnerId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [DisplayName("Name")]
        public string? MasterPartnerName { get; set; }
        [Required(ErrorMessage = "Website Url Is Required")]
        [DisplayName("Website Url")]
        public string? MasterPartnerWebsiteUrl { get; set; }
        [Required]
        [DisplayName("Logo")]
        public string? MasterPartnerLogoImageUrl { get; set; }
        [DisplayName("Logo")]
        public IFormFile? File {  get; set; }
    }
}
