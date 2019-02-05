using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Maps;

namespace TrafficSimulator.Models.Simulation
{
    public class CarEntry
    {
        public Guid Id { get; set; }
        public List<MapElement> VisibleElements { get; set; }
        public Vector2 Position { get; set; }
        public Direction Direction { get; set; }
        public List<Guid> Collisions { get; set; }
    }
}
