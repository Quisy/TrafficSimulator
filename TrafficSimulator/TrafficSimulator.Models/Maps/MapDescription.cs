using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Maps
{
    public class MapDescription
    {
        public string Name { get; set; }
        public CrossroadsDescription[] CrossroadsDescriptions { get; set; }
        public TrafficLineDescription[] TrafficLineDescriptions { get; set; }
        public SignDescription[] SignDescriptions { get; set; }
    }
}
