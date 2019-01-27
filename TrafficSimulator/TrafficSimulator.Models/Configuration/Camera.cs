using System;
using System.Collections.Generic;
using System.Text;
using TrafficSimulator.Enums;

namespace TrafficSimulator.Models.Configuration
{
    public class Camera
    {
        public CameraType CameraType { get; set; }
        public int Range { get; set; }
        public int Span { get; set; }
    }
}
