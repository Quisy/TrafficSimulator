using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Maps;

namespace TrafficSimulator.Models.Vehicles
{
    public class Car : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Camera> Cameras { get; set; }
        public Direction Direction { get; set; }
        public double CurrentSpeed { get; set; }
        public double CurrentAcceleration { get; set; }
        public double MaxAcceleration { get; set; }
        public Track Track { get; set; }

        public Car(Configuration.Car car, List<Configuration.Track> tracks)
        {
            Id = Guid.NewGuid();
            Name = car.Name;
            Direction = Direction.Up;
            Cameras = car.Cameras.Select(c => new Camera(c)).ToList();
            MaxAcceleration = car.Acceleration;
            Position = new Vector2();
            Track = new Models.Vehicles.Track(tracks.Single(t => 
                t.CarName.Equals(car.Name, StringComparison.OrdinalIgnoreCase)));
            CurrentSpeed = 50;
        }

        public List<Guid> CheckCollision(List<Car> cars)
        {
            var collisionCars = new List<Guid>();
            cars.ForEach(c =>
            {
                if (this.Collider.CheckCollision(c.Collider))
                {
                    collisionCars.Add(c.Id);
                }
            });

            return collisionCars;
        }

        public List<MapElement> GetVisibleElements(RoadsMap map)
        {
            List<MapElement> elements = new List<MapElement>();

            foreach (var camera in Cameras)
            {
                var visibleElements = camera.GetVisibleObjects(map);
                elements.AddRange(visibleElements);
            }

            return elements;
        }

        public void UpdateDirection()
        {
            if (Track.CurrentPoint.Equals(this.Position))
            {
                var nextPointDirection = this.GetNextPointDirection();
                Track.SetCheckpoint();
                this.SetDirection(nextPointDirection);
            }
        }

        public void SetDirection(Direction direction)
        {
            Direction = direction;
            Cameras.ForEach(c => c.SetDirectionByCarDirection(direction));
        }

        public void TurnRight()
        {
            Direction = Direction.NextRight();

            foreach (var camera in Cameras)
            {
                camera.CurrentDirection = camera.CurrentDirection.NextRight();
            }
        }

        public void TurnLeft()
        {
            Direction = Direction.NextLeft();

            foreach (var camera in Cameras)
            {
                camera.CurrentDirection = camera.CurrentDirection.NextLeft();
            }
        }

        public void TurnAround()
        {
            Direction = Direction.Opposite();

            foreach (var camera in Cameras)
            {
                camera.CurrentDirection = camera.CurrentDirection.Opposite();
            }
        }


        public Direction GetNextPointDirection()
        {
            var nextPoint = Track.NextPoint;

            if (Position.X < nextPoint.X)
                return Direction.Right;

            if (Position.X > nextPoint.X)
                return Direction.Left;

            if (Position.Y > nextPoint.Y)
                return Direction.Up;

            if (Position.Y < nextPoint.Y)
                return Direction.Down;

            return Direction.Up;
        }
    }
}
