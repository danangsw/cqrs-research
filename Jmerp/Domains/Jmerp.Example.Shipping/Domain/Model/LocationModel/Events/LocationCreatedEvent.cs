using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Jmerp.Example.Shipping.Domain.Model.LocationModel.Events
{
    [EventVersion("LocationCreated", 1)]
    public class LocationCreatedEvent : AggregateEvent<LocationAggregate, LocationId>
    {
        public LocationCreatedEvent(
            string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
