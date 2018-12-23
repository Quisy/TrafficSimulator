using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;

namespace TrafficSimulator.Models.Vehicles
{
    public class Car : Vehicle
    {
        public override VehicleType VehicleType => VehicleType.Car;

        public Car(Vector2 startPosition) : base(startPosition)
        {
        }

        public void Move(Vector2 position)
        {
            this.Position.X += position.X;
            this.Position.Y += position.Y;
        }


       
    }
}
