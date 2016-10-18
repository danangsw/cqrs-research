

using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Events
{
    [EventVersion("CargoBooked",1)]
    public class CargoBookedEvent : AggregateEvent<CargoAggregate, CargoId>
    {
        public Route Route { get; }

        public CargoBookedEvent(
            Route route)
        {
            Route = route;
        }
    }
}
