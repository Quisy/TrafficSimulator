using System.Numerics;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Vehicles;

namespace TrafficSimulator.Services.Interfaces
{
    public interface IMoveService
    {
        Vector2 Move(Car car);
    }
}
