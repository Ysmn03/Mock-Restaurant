using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterPartner : BaseModel
    {
        public int MasterPartnerId { get; set; }
        public string? MasterPartnerName { get; set; }
        public string? MasterPartnerLogoImageUrl { get; set; }
        public string? MasterPartnerWebsiteUrl { get; set; }
    }
}
