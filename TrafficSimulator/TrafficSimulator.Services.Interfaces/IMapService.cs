using System;
using System.Collections.Generic;
using System.Text;
using TrafficSimulator.Models.Maps;

namespace TrafficSimulator.Services.Interfaces
{
    public interface IMapService
    {
        RoadsMap ReadMapFromFile(string mapFilePath, string descriptionFilePath);
    }
}
