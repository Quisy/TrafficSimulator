using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Colliders;
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
        public bool InUse { get; set; }
        public Vector2 Size { get; set; }

        public Car(Configuration.Car car, List<Configuration.Track> tracks)
        {
            Id = Guid.NewGuid();
            Name = car.Name;
            Direction = Direction.Up;
            Cameras = car.Cameras.Select(c => new Camera(c)).ToList();
            MaxAcceleration = car.Acceleration;
            Track = new Models.Vehicles.Track(tracks.Single(t =>
                t.CarName.Equals(car.Name, StringComparison.OrdinalIgnoreCase)));
            Position = Track.StartPoint;
            CurrentSpeed = 14; //m/s
            InUse = true;
            Size = new Vector2(10, 30);
            Collider = new BoxCollider(Position, Size);

            Cameras.ForEach(c=>c.Position = this.Position);
        }

        public override void Move(Vector2 moveVector)
        {
            var prevPosition = this.Position;

            this.Position.X += moveVector.X;
            this.Position.Y += moveVector.Y;

            this.Cameras.ForEach(c => c.Move(moveVector));

            if (this.IsCheckpointTaken())
            {
                this.Position = Track.NextPoint;
                Track.SetCheckpoint();
            }

            this.Collider.ChangePosition(this.Position);

            if (Position.Equals(Track.EndPoint))
                InUse = false;
        }

        public List<Guid> CheckCollision(List<Car> cars)
        {
            var collisionCars = new List<Guid>();
            cars.ForEach(c =>
            {
                if (this.Collider.CheckCollision(c.Collider))
                {
                    collisionCars.Add(c.Id);
                    this.InUse = false;
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

        public Direction UpdateDirection()
        {
            if (Track.CurrentPoint.Equals(this.Position))
            {
                var nextPointDirection = this.GetNextPointDirection();
                this.SetDirection(nextPointDirection);
            }

            this.Cameras.ForEach(c => c.SetDirectionByCarDirection(Direction));

            return Direction;
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


        private bool IsCheckpointTaken()
        {
            var nextPoint = Track.NextPoint;

            switch (Direction)
            {
                case Direction.Up:
                    return Position.Y <= nextPoint.Y;
                case Direction.Down:
                    return Position.Y >= nextPoint.Y;
                case Direction.Left:
                    return Position.X <= nextPoint.X;
                case Direction.Right:
                    return Position.X >= nextPoint.X;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
