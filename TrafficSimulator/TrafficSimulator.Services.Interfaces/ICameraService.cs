using System;
using System.Collections.Generic;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Maps;
using TrafficSimulator.Models.Vehicles;

namespace TrafficSimulator.Services.Interfaces
{
    public interface ICameraService
    {
        void CheckObjects(RoadsMap map, Car car, Direction direction);
    }
}
