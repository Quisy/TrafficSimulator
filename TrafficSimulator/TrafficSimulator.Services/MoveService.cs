using System;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Vehicles;
using TrafficSimulator.Services.Interfaces;

namespace TrafficSimulator.Services
{
    public class MoveService : IMoveService
    {
        private readonly int _pixelsPerMeter;

        public MoveService(int pixelsPerMeter)
        {
            _pixelsPerMeter = pixelsPerMeter;
        }

        public void Move(Car car, Direction direction)
        {
            Vector2 moveVector = new Vector2(0,0);
            int pixelMove = (int)car.CurrentSpeed * _pixelsPerMeter;

            switch (direction)
            {
                case Direction.Up:
                    moveVector.Y = pixelMove;
                    break;
                case Direction.Down:
                    moveVector.Y = -pixelMove;
                    break;
                case Direction.Left:
                    moveVector.X = -pixelMove;
                    break;
                case Direction.Right:
                    moveVector.X = pixelMove;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            car.Move(moveVector);

        }
    }
}
