using System.Numerics;
using TrafficSimulator.Models.Base;

namespace TrafficSimulator.Models.Vehicles
{
    public class Car : Vehicle
    {
        public Camera[] Cameras { get; set; }

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
