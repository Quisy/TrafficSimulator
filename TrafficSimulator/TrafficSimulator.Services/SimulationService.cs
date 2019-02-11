using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Configuration;
using TrafficSimulator.Models.Maps;
using TrafficSimulator.Models.Simulation;
using TrafficSimulator.Models.Vehicles;
using TrafficSimulator.Services.Interfaces;
using Camera = TrafficSimulator.Models.Vehicles.Camera;
using Car = TrafficSimulator.Models.Vehicles.Car;

namespace TrafficSimulator.Services
{
    public class SimulationService : ISimulationService
    {
        private bool _simulationEnabled;
        private Simulation _simulationConfiguration;
        private readonly IMoveService _moveService;
        private readonly List<Car> _cars;
        private RoadsMap _map;

        public SimulationService()
        {
            _cars = new List<Car>();

            var settings = new Settings
            {
                PixelsPerMeter = 10,
                TicksPerSecond = 10
            };

            _moveService = new MoveService(settings);
        }

        public void ReadConfiguration(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                _simulationConfiguration = (Simulation)serializer.Deserialize(file, typeof(Simulation));
            }

            this.ReadMap(_simulationConfiguration.Map);
            this.MapCars(_simulationConfiguration.Cars);
        }

        public void StartSimulation()
        {
            int tickId = 0;
            List<SimulationEntry> simulationEntries = new List<SimulationEntry>();
            _simulationEnabled = true;

            while (_simulationEnabled)
            {
                var simulationEntry = this.Tick();
                simulationEntry.TickId = tickId;
                simulationEntries.Add(simulationEntry);
                tickId++;
            }

            GenerateSimulationFile(simulationEntries);
        }

        private SimulationEntry Tick()
        {
            var simulationEntry = new SimulationEntry();

            var tasks = new List<Task>();

            foreach (var car in _cars.Where(c => c.InUse))
            {
                var carEntry = new CarEntry();
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    carEntry.Id = car.Id;
                    carEntry.Position = _moveService.Move(car);
                    carEntry.VisibleElements = car.GetVisibleElements(_map);
                    if (carEntry.VisibleElements.Count > 0)
                    {
                        Console.WriteLine("found");
                    }
                    carEntry.Collisions = car.CheckCollision(_cars.Where(c => c.Id != car.Id).ToList());
                    carEntry.Direction = car.UpdateDirection();
                }));

                simulationEntry.Cars.Add(carEntry);
            }
            Task.WaitAll(tasks.ToArray());

            if (_cars.All(c => !c.InUse))
            {
                _simulationEnabled = false;
            }

            return simulationEntry;
        }

        private void ReadMap(Map mapConfig)
        {
            this._map = new RoadsMap
            {
                Name = mapConfig.Name,
                MapElements = mapConfig.Elements.Select(e => new MapElement(e)).ToList()
            };
        }

        private void MapCars(List<Models.Configuration.Car> carsConfig)
        {
            foreach (var car in carsConfig)
            {
                var newCar = new Car(car, _simulationConfiguration.Tracks);

                var startDirection = newCar.GetNextPointDirection();
                newCar.SetDirection(startDirection);

                _cars.Add(newCar);
            }
        }

        private void GenerateSimulationFile(List<SimulationEntry> simulation)
        {
            File.WriteAllText($@"simulation.json", JsonConvert.SerializeObject(simulation));
        }

    }
}
