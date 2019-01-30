using System;
using System.Collections.Generic;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Colliders;
using TrafficSimulator.Models.Configuration;

namespace TrafficSimulator.Models.Vehicles
{
    public class Car : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Camera> Cameras { get; set; }
        public Direction Direction { get; set; }
        public double CurrentSpeed { get; set; }
        public double CurrentAcceleration { get; set; }
        public double MaxAcceleration { get; set; }
        public Track Track { get; set; }
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
