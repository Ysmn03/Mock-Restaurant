using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterContactUsInformation :BaseModel
    {
        public int MasterContactUsInformationId { get; set; }
        public string? MasterContactUsInformationIdesc { get; set; }
        public string? MasterContactUsInformationImageUrl { get; set; }
        public string? MasterContactUsInformationRedirect { get; set; }
    }
}
