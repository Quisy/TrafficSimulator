using System;
using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;
using TrafficSimulator.Models.Colliders;
using TrafficSimulator.Models.Configuration;

namespace TrafficSimulator.Models.Maps
{
    public class MapElement : Entity
    {
        public Guid Id { get; set; }
        public MapElementType MapElementType { get; set; }


        public MapElement(Element element)
        {
            this.Id = Guid.NewGuid();
            this.Position = new Vector2(element.Position.X, element.Position.Y);
            this.Collider = new BoxCollider(this.Position, new Vector2(element.Size, element.Size));
        }

        public MapElement(Vector2 position, Vector2 size)
        {
            this.Collider = new BoxCollider(position, size);
        }

        
    }
}
