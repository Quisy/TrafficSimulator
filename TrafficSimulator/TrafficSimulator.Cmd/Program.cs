using System;
using TrafficSimulator.Services;

namespace TrafficSimulator.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulationService = new SimulationService();

            simulationService.ReadConfiguration(@"D:\Projects\TrafficSimulator\TrafficSimulator\Files\Simulation1.json");
            simulationService.StartSimulation();
        }
    }
}
