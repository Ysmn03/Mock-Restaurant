using Restuarant.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Areas.Admin.ViewModels
{
    public class MasterWorkingHoursModel:BaseModel
    {
        [Required]
        public int MasterWorkingHoursId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [DisplayName("Name")]
        public string? MasterWorkingHoursIdName { get; set; }
        [Required(ErrorMessage = "Time Is Required")]
        [DisplayName("Time From To")]
        public string? MasterWorkingHoursIdTimeFormTo { get; set; }
    }
}
