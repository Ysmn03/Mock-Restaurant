using System;
using System.Collections.Generic;

namespace Restuarant.Models
{
    public class MasterSlider :BaseModel
    {
        public int MasterSliderId { get; set; }
        public string? MasterSliderTitle { get; set; }
        public string? MasterSliderBreef { get; set; }
        public string? MasterSliderDesc { get; set; }
        public string? MasterSliderUrl { get; set; }
        public string? MasterSliderImageUrl { get; set; }

    }
}
