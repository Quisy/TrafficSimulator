using System;
using System.Collections.Generic;
using System.Text;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Configuration
{
    public class Element
    {
        public MapPoint Position { get; set; }
        public int Size { get; set; }
        public MapElementType ElementType { get; set; }
    }
}
