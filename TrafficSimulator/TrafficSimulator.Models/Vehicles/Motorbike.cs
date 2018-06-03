using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Base;

namespace TrafficSimulator.Models.Vehicles
{
    public class Motorbike : Vehicle
    {
        public override VehicleType VehicleType => VehicleType.Motorbike;
    }
}
