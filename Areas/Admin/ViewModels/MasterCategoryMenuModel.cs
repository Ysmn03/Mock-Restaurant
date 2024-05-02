using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterCategoryMenuModel : BaseModel
    {
        [Required]
        public int MasterCategoryMenuId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [DisplayName("Category Name")]
        public string? MasterCategoryMenuName { get; set; }
        public IList<MasterCategoryMenu>? ListCategory { get; set; }
    }
}
