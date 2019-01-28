using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Configuration
{
    public class Car
    {
        public string Name { get; set; }
        public List<Camera> Cameras { get; set; }
        public double Acceleration { get; set; }
    }
}
