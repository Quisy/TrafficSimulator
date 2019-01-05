using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Maps;
using TrafficSimulator.Models.Vehicles;
using TrafficSimulator.Services.Interfaces;

namespace TrafficSimulator.Services
{
    public class CameraService : ICameraService
    {
        public void CheckObjects(RoadsMap map, Car car, Direction direction)
        {

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
                    visibleObjects = mapElements
                        .Where(
                            e => currentPosition.X - e.MeterPosition.X <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => e.MeterPosition.X - currentPosition.X <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Left:
                    visibleObjects = mapElements
                        .Where(
                            e => e.MeterPosition.Y - currentPosition.Y <= cameraRange
                                 && Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => currentPosition.Y - e.MeterPosition.Y <= cameraRange
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
            List<MapElement> visibleObjects;

            switch (direction)
            {
                case Direction.Up:
                    visibleObjects = mapElements
                        .Where(
                            e => e.MeterPosition.X - currentPosition.X <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => currentPosition.X - e.MeterPosition.X <= cameraRange
                                 && Math.Abs(currentPosition.Y - e.MeterPosition.Y) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Left:
                    visibleObjects = mapElements
                        .Where(
                            e => currentPosition.Y - e.MeterPosition.Y <= cameraRange
                                 && Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraSpan / 2)
                        .ToList();
                    break;
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => e.MeterPosition.Y - currentPosition.Y <= cameraRange
                                 && Math.Abs(currentPosition.X - e.MeterPosition.X) <= cameraSpan / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return visibleObjects;
        }
    }
}
