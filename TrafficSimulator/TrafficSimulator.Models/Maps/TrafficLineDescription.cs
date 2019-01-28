using System;
using System.Collections.Generic;
using System.Text;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Maps
{
    public class TrafficLineDescription
    {
        public TrafficLineDirection Direction { get; set; }

        public int MaxSpeed { get; set; }

        public char MapMark { get; set; }

        public char? SpawnPointMark { get; set; }

    }
}
