using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Crossroads
{
    public class CrossroadsLight
    {
        public Point Position { get; set; }

        public LightColor CurrentColor { get; set; }
    }
}
