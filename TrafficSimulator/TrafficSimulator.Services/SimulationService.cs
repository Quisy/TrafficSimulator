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
        private List<Car> _cars;

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
        }

        public void StartSimulation()
        {
            _simulationEnabled = true;

            while (_simulationEnabled)
            {
                this.Tick();
            }
        }

        private void Tick()
        {
            var tasks = new List<Task>();

            foreach (var car in _cars)
            {
                //tasks.Add(Task.Factory.StartNew(() => DoSomething(item)));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private void MapCars(List<Models.Configuration.Car> carsConfig)
        {
            foreach (var car in carsConfig)
            {
                var newCar = new Car
                {
                    Id = Guid.NewGuid(),
                    Name = car.Name,
                    Direction = Direction.Up,
                    Cameras = car.Cameras.Select(c => new Camera(c)).ToList(),
                    MaxAcceleration = car.Acceleration,
                    Position = new Vector2(),
                    Track = new Models.Vehicles.Track(_simulationConfiguration.Tracks.Single(t =>
                        t.CarName.Equals(car.Name, StringComparison.OrdinalIgnoreCase)))
                };

                var startDirection = newCar.GetNextPointDirection();
                newCar.SetDirection(startDirection);

                _cars.Add(newCar);
            }
        }

    }
}
