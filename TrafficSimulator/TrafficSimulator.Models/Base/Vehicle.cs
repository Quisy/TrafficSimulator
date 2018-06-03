using System.Drawing;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Base
{
    public abstract class Vehicle
    {
        public abstract VehicleType VehicleType { get; }

        public DriverType DriverType { get; set; }

        public Point Position { get; set; }

        public double CurrentSpeed { get; set; }
    }
}
