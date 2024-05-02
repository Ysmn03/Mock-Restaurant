using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterServices : BaseModel
    {
        public int MasterServicesId { get; set; }
        public string? MasterServicesTitle { get; set; }
        public string? MasterServicesDesc { get; set; }
        public string? MasterServicesImage { get; set; }
    }
}
