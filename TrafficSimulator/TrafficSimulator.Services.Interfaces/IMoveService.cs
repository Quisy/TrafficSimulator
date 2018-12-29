using TrafficSimulator.Enums;
using TrafficSimulator.Models.Vehicles;

namespace TrafficSimulator.Services.Interfaces
{
    public interface IMoveService
    {
        void Move(Car car, Direction direction);
    }
}
