using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class SystemSettingModel :BaseModel
    {
        [Required]
        public int SystemSettingId { get; set; }
        [Required]
        [DisplayName("Logo 1")]
        public string? SystemSettingLogoImageUrl { get; set; }
        [DisplayName("Logo 1")]
        public IFormFile? ImageUrl1 { get; set; }
        [Required]
        [DisplayName("Logo 2")]
        public string? SystemSettingLogoImageUrl2 { get; set; }
        [DisplayName("Logo 2")]
        public IFormFile? ImageUrl2 { get; set; }
        [Required(ErrorMessage = "Copyright Is Required")]
        [DisplayName("Copyright")]
        public string? SystemSettingCopyright { get; set; }
        [Required(ErrorMessage = "Welcome Title Is Required")]
        [DisplayName("Title")]
        public string? SystemSettingWelcomeNoteTitle { get; set; }
        [Required(ErrorMessage = "Welcome Breef Is Required")]
        [DisplayName("Breef")]
        public string? SystemSettingWelcomeNoteBreef { get; set; }
        [Required(ErrorMessage = "Welcome Description Is Required")]
        [DisplayName("Description")]
        public string? SystemSettingWelcomeNoteDesc { get; set; }
        [Required(ErrorMessage = "Welcome Note Is Required")]
        [DisplayName("Welcome Url")]
        public string? SystemSettingWelcomeNoteUrl { get; set; }
        [Required]
        [DisplayName("Note Image")]
        public string? SystemSettingWelcomeNoteImageUrl { get; set; }
        [DisplayName("Note Image")]
        public IFormFile? NoteImageUrl { get; set; }
        [Required(ErrorMessage = "Loccation Is Required")]
        [DisplayName("Location")]
        public string? SystemSettingMapLocation { get; set; }
    }
}
