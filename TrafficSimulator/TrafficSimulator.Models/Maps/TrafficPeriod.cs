using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Maps
{
    public class TrafficPeriod
    {
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public int TrafficJamsFactor { get; set; }
    }
}
