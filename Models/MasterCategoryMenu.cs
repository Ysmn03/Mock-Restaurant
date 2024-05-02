using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterCategoryMenu :BaseModel
    {
        //public MasterCategoryMenu()
        //{
        //    MasterItemMenus = new HashSet<MasterItemMenu>();
        //}

        public int MasterCategoryMenuId { get; set; }
        public string? MasterCategoryMenuName { get; set; }

        //public virtual ICollection<MasterItemMenu> MasterItemMenus { get; set; }
    }
}
