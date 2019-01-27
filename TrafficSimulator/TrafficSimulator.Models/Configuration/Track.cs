using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Configuration
{
    public class Track
    {
        public string Name { get; set; }
        public string CarName { get; set; }
        public MapPoint StartPoint { get; set; }
        public MapPoint EndPoint { get; set; }
        public MapPoint[] ControlPoints { get; set; }
    }
}
