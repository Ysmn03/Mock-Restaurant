using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterSocialMediaModel:BaseModel
    {
        [Required]
        public int MasterSocialMediaId { get; set; }
        [Required(ErrorMessage = "Media Url Is Required")]
        [DisplayName("Media Url")]
        public string MasterSocialMediaUrl { get; set; } = null!;
        [Required]
        [DisplayName("Image")]
        public string MasterSocialMediaImageUrl { get; set; } = null!;
    }
}
