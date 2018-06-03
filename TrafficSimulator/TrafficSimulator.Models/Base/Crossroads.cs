using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Crossroads;

namespace TrafficSimulator.Models.Base
{
    public abstract class Crossroads
    {
        public abstract CrossroadsType CrossroadsType { get; }

        public Point[] PositionArea { get; set; }

        public CrossroadsLight[] Lights { get; set; }
    }
}
