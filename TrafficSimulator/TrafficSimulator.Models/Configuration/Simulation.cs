using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Configuration
{
    public class Simulation
    {
        public Map Map { get; set; }
        public List<Car> Cars { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
