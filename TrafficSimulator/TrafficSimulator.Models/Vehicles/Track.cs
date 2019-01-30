using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TrafficSimulator.Models.Vehicles
{
    public class Track
    {
        private int _currentPointIndex = 0;


        public Track(Configuration.Track trackConfig)
        {
            Id = Guid.NewGuid();
            StartPoint = new Vector2(trackConfig.StartPoint.X, trackConfig.StartPoint.Y);
            EndPoint = new Vector2(trackConfig.EndPoint.X, trackConfig.EndPoint.Y);

            foreach (var point in trackConfig.ControlPoints)
            {
                ControlPoints.Add(new Vector2(point.X, point.Y));
            }
        }

        public Guid Id { get; set; }
        public Vector2 StartPoint { get; set; }
        public Vector2 EndPoint { get; set; }
        public List<Vector2> ControlPoints { get; set; }

        public Vector2 CurrentPoint
        {
            get
            {
                if (ControlPoints.Count >= _currentPointIndex + 1)
                {
                    return EndPoint;
                }

                if (_currentPointIndex == 0)
                {
                    return StartPoint;
                }

                return ControlPoints[_currentPointIndex];
            }
        }

        public Vector2 NextPoint
        {
            get
            {
                if (ControlPoints.Count >= _currentPointIndex + 1)
                {
                    return EndPoint;
                }

                return ControlPoints[_currentPointIndex + 1];
            }
        }

        public void SetCheckpoint()
        {
            _currentPointIndex++;
        }
    }
}
