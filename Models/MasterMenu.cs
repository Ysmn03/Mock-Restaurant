using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterMenu : BaseModel
    {
        public int MasterMenuId { get; set; }
        public string MasterMenuName { get; set; } = null!;
        public string MasterMenuUrl { get; set; } = null!;
    }
}
