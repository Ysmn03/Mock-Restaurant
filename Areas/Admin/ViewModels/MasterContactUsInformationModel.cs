using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterContactUsInformationModel :BaseModel
    {
        [Required]
        public int MasterContactUsInformationId { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [DisplayName("Description")]
        public string? MasterContactUsInformationIdesc { get; set; }
        [Required]
        [DisplayName("Image")]
        public string? MasterContactUsInformationImageUrl { get; set; }
        [Required(ErrorMessage = "Redirect Is Required")]
        [DisplayName("Redirect")]
        public string? MasterContactUsInformationRedirect { get; set; }
    }
}
