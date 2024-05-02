using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterSocialMedia :BaseModel
    {
        public int MasterSocialMediaId { get; set; }
        public string MasterSocialMediaImageUrl { get; set; } = null!;
        public string MasterSocialMediaUrl { get; set; } = null!;
    }
}
