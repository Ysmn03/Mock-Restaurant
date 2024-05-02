using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterOfferModel : BaseModel
    {
        [Required]
        public int MasterOfferId { get; set; }
        [Required(ErrorMessage = "Title Is Required")]
        [DisplayName("Title")]
        public string? MasterOfferTitle { get; set; }
        [Required(ErrorMessage = "Breef Is Required")]
        [DisplayName("Breef")]
        public string? MasterOfferBreef { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [DisplayName("Description")]
        public string? MasterOfferDesc { get; set; }
        [Required]
        [DisplayName("Image")]
        public string? MasterOfferImageUrl { get; set; }
        [DisplayName("Image")]
        public IFormFile? File { get; set; }
    }
}
