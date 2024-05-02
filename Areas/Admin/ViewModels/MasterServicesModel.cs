using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterServicesModel:BaseModel
    {
        [Required]
        public int MasterServicesId { get; set; }
        [Required(ErrorMessage = "Title Is Required")]
        [DisplayName("Title")]
        public string? MasterServicesTitle { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [DisplayName("Description")]
        public string? MasterServicesDesc { get; set; }
        [Required]
        [DisplayName("Image")]
        public string? MasterServicesImage { get; set; }
    }
}
