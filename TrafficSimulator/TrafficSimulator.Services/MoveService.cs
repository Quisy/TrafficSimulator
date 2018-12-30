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

        public void CheckObjects(RoadsMap map, Car car, Direction direction)
        { 
            switch (direction)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private List<MapElement> GetFrontObjects(Vector2 currentPosition, List<MapElement> mapElements, int cameraRange, int cameraSpan, Direction direction)
        {
            List<MapElement> visibleObjects;
            
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraRange
                                 && Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraSpan/2)
                        .ToList();
                    break;
                case Direction.Left:
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan/2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return visibleObjects;
        }

        private List<MapElement> GetBackObjects(Vector2 currentPosition, List<MapElement> mapElements, int cameraRange, int cameraSpan, Direction direction)
        {
            List<MapElement> visibleObjects;

            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraRange
                                 && Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Left:
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return visibleObjects;
        }


        private List<MapElement> GetLeftObjects(Vector2 currentPosition, List<MapElement> mapElements, int cameraRange, int cameraSpan, Direction direction)
        {
            List<MapElement> visibleObjects;

            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Left:
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraRange
                                 && Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraSpan / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return visibleObjects;
        }

        private List<MapElement> GetRightObjects(Vector2 currentPosition, List<MapElement> mapElements, int cameraRange, int cameraSpan, Direction direction)
        {
            return null;
        }
    }
}
