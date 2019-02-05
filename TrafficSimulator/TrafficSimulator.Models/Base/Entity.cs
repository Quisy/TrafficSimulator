using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TrafficSimulator.Models.Colliders;
using TrafficSimulator.Models.Configuration;
using TrafficSimulator.Models.Maps;

namespace TrafficSimulator.Models.Base
{
    public abstract class Entity
    {
        public Vector2 Position;
        public Collider Collider;
    }
}
