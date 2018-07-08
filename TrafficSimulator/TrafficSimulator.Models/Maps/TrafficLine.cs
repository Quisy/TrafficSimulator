using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TrafficSimulator.Models.Maps
{
    public class TrafficLine
    {
        public Point SpawnPoint { get; set; }
        public List<Point> Points { get; set; }
    }
}
