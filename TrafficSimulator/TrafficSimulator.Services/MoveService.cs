using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Configuration;
using TrafficSimulator.Models.Maps;
using TrafficSimulator.Services.Interfaces;
using Car = TrafficSimulator.Models.Vehicles.Car;

namespace TrafficSimulator.Services
{
    public class MoveService : IMoveService
    {
        private readonly Settings _settings;

        public MoveService(Settings setting)
        {
            _settings = setting;
        }

        public void Move(Car car)
        {
            Vector2 moveVector = new Vector2(0,0);

            int moveRange = this.GetDistance(car.CurrentSpeed, car.CurrentAcceleration) * _settings.PixelsPerMeter;

            switch (car.Direction)
            {
                case Direction.Up:
                    moveVector.Y = moveRange;
                    break;
                case Direction.Down:
                    moveVector.Y = -moveRange;
                    break;
                case Direction.Left:
                    moveVector.X = -moveRange;
                    break;
                case Direction.Right:
                    moveVector.X = moveRange;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            car.Move(moveVector);
        }



        public int GetDistance(double speed)
        {
            return (int)speed / _settings.TicksPerSecond;
        }

        public int GetDistance(double speed, double acceleration)
        {
            return (int)(speed / _settings.TicksPerSecond + acceleration/(Math.Pow(_settings.TicksPerSecond,2)*2));
        }

        public double GetSpeed(double speed, double acceleration)
        {
            return speed + acceleration / _settings.TicksPerSecond;
        }
    }
}
