using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Configuration
{
    public class Map
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public List<Element> Elements { get; set; }
    }
}
