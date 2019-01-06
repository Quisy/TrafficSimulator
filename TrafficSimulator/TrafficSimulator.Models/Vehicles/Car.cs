using System.Numerics;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Colliders;

namespace TrafficSimulator.Models.Vehicles
{
    public class Car : Entity
    {
        public Camera[] Cameras { get; set; }
        public double CurrentSpeed { get; set; }
    }
}
