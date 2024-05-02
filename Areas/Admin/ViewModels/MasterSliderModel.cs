using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterSliderModel:BaseModel
    {
        [Required]
        public int MasterSliderId { get; set; }
        [Required(ErrorMessage = "Title Is Required")]
        [DisplayName("Title")]
        public string? MasterSliderTitle { get; set; }
        [Required(ErrorMessage = "Breef Is Required")]
        [DisplayName("Breef")]
        public string? MasterSliderBreef { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [DisplayName("Description")]
        public string? MasterSliderDesc { get; set; }
        [Required(ErrorMessage = "Slider Url Is Required")]
        [DisplayName("Slider Url")]
        public string? MasterSliderUrl { get; set; }
        [Required]
        [DisplayName("Image")]
        public string? MasterSliderImageUrl { get; set; }
        [DisplayName("Image")]
        public IFormFile? File {  get; set; }
    }
}
