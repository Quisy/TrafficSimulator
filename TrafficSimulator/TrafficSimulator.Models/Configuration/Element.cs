using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Configuration
{
    public class Element
    {
        public MapPoint Position { get; set; }
        public int Size { get; set; }
        public ElementType ElementType { get; set; }
    }
}
