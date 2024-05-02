using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restuarant.Models
{
    public class MasterWorkingHours :BaseModel
    {
        public int MasterWorkingHoursId { get; set; }
        public string? MasterWorkingHoursIdName { get; set; }
        public string? MasterWorkingHoursIdTimeFormTo { get; set; }
    }
}
