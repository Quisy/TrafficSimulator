using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TrafficSimulator.Models.Colliders
{
    public abstract class Collider
    {
        public List<Vector2> Points;
        public abstract void ChangePosition(Vector2 position);
        public abstract void ChangeSize(Vector2 size);
        public abstract bool CheckCollision(Collider collider);
    }
}
