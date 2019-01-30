using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using TrafficSimulator.Models.Colliders;

namespace TrafficSimulator.Models.Base
{
    public abstract class Entity
    {
        public Vector2 Position;
        public Collider Collider;

        public virtual void Move(Vector2 moveVector)
        {
            this.Position.X += moveVector.X;
            this.Position.Y += moveVector.Y; 
        }

        public abstract void Update();
    }
}
