using System;
using System.Collections.Generic;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Colliders;
using TrafficSimulator.Models.Configuration;

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

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void SetDirection(Direction direction)
        {
            Direction = direction;
            Cameras.ForEach(c=>c.SetDirectionByCarDirection(direction));
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
