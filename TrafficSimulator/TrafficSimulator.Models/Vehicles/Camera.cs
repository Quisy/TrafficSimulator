using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Maps;

namespace TrafficSimulator.Models.Vehicles
{
    public class Camera : Entity
    {
        public int Range { get; set; }

        public int Span { get; set; }

        public Direction CurrentDirection { get; set; }

        public CameraType CameraType { get; set; }

        public Camera(Configuration.Camera cameraConfig)
        {
            Range = cameraConfig.Range;
            Span = cameraConfig.Span;
            CameraType = cameraConfig.CameraType;
        }

        public List<MapElement> GetVisibleObjects(RoadsMap map)
        {
            var elements = map.MapElements.ToList();

            switch (CameraType)
            {
                case CameraType.Front:
                    return this.GetFrontObjects(elements);
                case CameraType.Back:
                    return this.GetBackObjects(elements);
                case CameraType.Left:
                    return this.GetLeftObjects(elements);
                case CameraType.Right:
                    return this.GetRightObjects(elements);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetDirectionByCarDirection(Direction carDirection)
        {
            switch (CameraType)
            {
                case CameraType.Front:
                    CurrentDirection = this.GetFrontCameraDirection(carDirection);
                    break;
                case CameraType.Back:
                    CurrentDirection = this.GetBackCameraDirection(carDirection);
                    break;
                case CameraType.Left:
                    CurrentDirection = this.GetLeftCameraDirection(carDirection);
                    break;
                case CameraType.Right:
                    CurrentDirection = this.GetRightCameraDirection(carDirection);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private List<MapElement> GetFrontObjects(List<MapElement> mapElements)
        {
            List<MapElement> visibleObjects;

            switch (CurrentDirection)
            {
                case Direction.Up:
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(Position.Y - e.Position.Y) <= Range
                                 && Math.Abs(Position.X - e.Position.X) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Left:
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(Position.X - e.Position.X) <= Range
                                 && Math.Abs(Position.Y - e.Position.Y) <= Span / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentDirection), CurrentDirection, null);
            }

            return visibleObjects;
        }

        private List<MapElement> GetBackObjects(List<MapElement> mapElements)
        {
            List<MapElement> visibleObjects;

            switch (CurrentDirection)
            {
                case Direction.Up:
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(Position.Y - e.Position.Y) <= Range
                                 && Math.Abs(Position.X - e.Position.X) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Left:
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => Math.Abs(Position.X - e.Position.X) <= Range
                                 && Math.Abs(Position.Y - e.Position.Y) <= Span / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentDirection), CurrentDirection, null);
            }

            return visibleObjects;
        }


        private List<MapElement> GetLeftObjects(List<MapElement> mapElements)
        {
            List<MapElement> visibleObjects;

            switch (CurrentDirection)
            {
                case Direction.Up:
                    visibleObjects = mapElements
                        .Where(
                            e => Position.X - e.Position.X <= Range
                                 && Math.Abs(Position.Y - e.Position.Y) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => e.Position.X - Position.X <= Range
                                 && Math.Abs(Position.Y - e.Position.Y) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Left:
                    visibleObjects = mapElements
                        .Where(
                            e => e.Position.Y - Position.Y <= Range
                                 && Math.Abs(Position.X - e.Position.X) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => Position.Y - e.Position.Y <= Range
                                 && Math.Abs(Position.X - e.Position.X) <= Span / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentDirection), CurrentDirection, null);
            }

            return visibleObjects;
        }

        private List<MapElement> GetRightObjects(List<MapElement> mapElements)
        {
            List<MapElement> visibleObjects;

            switch (CurrentDirection)
            {
                case Direction.Up:
                    visibleObjects = mapElements
                        .Where(
                            e => Position.Y - e.Position.Y <= Range
                                 && Math.Abs(Position.X - e.Position.X) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Down:
                    visibleObjects = mapElements
                        .Where(
                            e => e.Position.Y - Position.Y <= Range
                                 && Math.Abs(Position.X - e.Position.X) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Left:
                    visibleObjects = mapElements
                        .Where(
                            e => Position.X - e.Position.X <= Range
                                 && Math.Abs(Position.Y - e.Position.Y) <= Span / 2)
                        .ToList();
                    break;
                case Direction.Right:
                    visibleObjects = mapElements
                        .Where(
                            e => e.Position.X - Position.X <= Range
                                 && Math.Abs(Position.Y - e.Position.Y) <= Span / 2)
                        .ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentDirection), CurrentDirection, null);
            }

            return visibleObjects;
        }

        private Direction GetFrontCameraDirection(Direction carDirection)
        {
            return carDirection;
        }

        private Direction GetBackCameraDirection(Direction carDirection)
        {
            return carDirection.Opposite();
        }

        private Direction GetRightCameraDirection(Direction carDirection)
        {
            return carDirection.NextRight();
        }

        private Direction GetLeftCameraDirection(Direction carDirection)
        {
            return carDirection.NextLeft();
        }
    }
}
