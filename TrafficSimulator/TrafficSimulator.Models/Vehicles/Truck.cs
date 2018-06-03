using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;

namespace TrafficSimulator.Models.Vehicles
{
    public class Truck : Vehicle
    {
        public override VehicleType VehicleType => VehicleType.Truck;
    }
}
