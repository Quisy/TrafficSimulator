using System.Drawing;
using System.Numerics;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Base
{
    public abstract class Vehicle
    {
        public Vector2 Position;

        public abstract VehicleType VehicleType { get; }

        public DriverType DriverType { get; set; }

        
        public double CurrentSpeed { get; set; }

        protected Vehicle(Vector2 startPosition)
        {
            this.Position = new Vector2(startPosition.X, startPosition.Y);
        }
    }
}
