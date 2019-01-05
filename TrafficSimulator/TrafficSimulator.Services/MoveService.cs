using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Maps;
using TrafficSimulator.Models.Vehicles;
using TrafficSimulator.Services.Interfaces;

namespace TrafficSimulator.Services
{
    public class MoveService : IMoveService
    {
        public MoveService()
        {
        }

        public void Move(Car car, Direction direction)
        {
            Vector2 moveVector = new Vector2(0,0);
            int moveRange = (int)car.CurrentSpeed;

            switch (direction)
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

        
    }
}
