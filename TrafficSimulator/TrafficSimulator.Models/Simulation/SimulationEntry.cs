using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficSimulator.Models.Simulation
{
    public class SimulationEntry
    {
        public SimulationEntry()
        {
            Cars = new List<CarEntry>();
        }

        public int TickId { get; set; }
        public List<CarEntry> Cars { get; set; }
    }
}
