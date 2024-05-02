using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterItemMenuModel:BaseModel
    {
        [Required]
        public int MasterItemMenuId { get; set; }
        [Required(ErrorMessage = "Title Is Required")]
        [DisplayName("Title")]
        public string? MasterItemMenuTitle { get; set; }
        [Required(ErrorMessage = "Breef Is Required")]
        [DisplayName("Breef")]
        public string? MasterItemMenuBreef { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [DisplayName("Description")]
        public string? MasterItemMenuDesc { get; set; }
        [Required(ErrorMessage = "Price Is Required")]
        [DisplayName("Price")]
        public decimal? MasterItemMenuPrice { get; set; }
        [Required(ErrorMessage = "Date Is Required")]
        [DisplayName("Date")]
        public DateTime? MasterItemMenuDate { get; set; }
        [Required]
        [DisplayName("Image")]

        public string? MasterItemMenuImageUrl { get; set; }
        [DisplayName("Image")]
        public IFormFile? File {  get; set; }
        [Required]
        [DisplayName("Category Name")]
        public int? MasterCategoryMenuId { get; set; }
        [Required]
        public MasterCategoryMenu? MasterCategoryMenu { get; set; }
    }
}
