using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterMenuModel : BaseModel
    {
        [Required]
        public int MasterMenuId { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [DisplayName("Name")]
        public string? MasterMenuName { get; set; }
        [Required(ErrorMessage = "Url Is Required")]
        [DisplayName("Url")]
        public string? MasterMenuUrl { get; set; }
    }
}
