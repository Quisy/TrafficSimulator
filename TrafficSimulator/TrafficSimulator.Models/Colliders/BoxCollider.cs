using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace TrafficSimulator.Models.Colliders
{
    public class BoxCollider : Collider
    {
        private Vector2 _position;
        private Vector2 _size;
        private Rectangle _rect;

        public BoxCollider(Vector2 position, Vector2 size)
        {
            _position = position;
            _size = size;
            _rect = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y);
            Points = new List<Vector2>
            {
                new Vector2(position.X, position.Y),
                new Vector2((position.X + size.X), (position.Y + size.Y)),
                new Vector2(position.X, (position.Y + size.Y)),
                new Vector2((position.X + size.X),  position.Y)
            };

        }

        public override void ChangePosition(Vector2 position)
        {
            var shiftVector = position - _position;
            _position = position;
            _rect.X = (int)position.X;
            _rect.Y = (int)position.Y;

            for (int i = 0; i < Points.Count; i++)
            {
                var point = Points[i];
                Points[i] = new Vector2(point.X + shiftVector.X, point.Y + shiftVector.Y);
            }
        }

        public override void ChangeSize(Vector2 size)
        {
            _size = size;
            _rect.Size = new Size((int)size.X, (int)size.Y);

            Points.Clear();
            Points.Add(new Vector2(_position.X, _position.Y));
            Points.Add(new Vector2((_position.X + _size.X), (_position.Y + _size.Y)));
            Points.Add(new Vector2(_position.X, (_position.Y + _size.Y)));
            Points.Add(new Vector2((_position.X + _size.X), _position.Y));
        }


        public override bool CheckCollision(Collider collider)
        {
            if (collider == null) return false;
            var colliderType = collider.GetType();
            if (colliderType == typeof(BoxCollider))
            {
                var col = collider as BoxCollider;
                var c1 = this._rect;
                var c2 = col._rect;

                return c2 != null &&
                       (
                           ((c1.Right >= c2.Left && c1.Right <= c2.Right) || (c1.Left >= c2.Left && c1.Left <= c2.Right))
                           && ((c1.Bottom >= c2.Top && c1.Bottom <= c2.Bottom) || (c1.Top >= c2.Top && c1.Top <= c2.Bottom) || (c1.Top <= c2.Top && c1.Bottom >= c2.Bottom))
                           );
            }
            return false;
        }
    }
}
