using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Maps
{
    public class MapElement
    {
        public MapElementType MapElementType { get; set; }

        public Point Position { get; set; }

        public char Mark { get; set; }

        public MeterPosition MeterPosition { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
