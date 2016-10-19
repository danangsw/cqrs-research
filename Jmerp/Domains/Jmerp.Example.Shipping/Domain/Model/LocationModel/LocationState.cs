using EventFlow.Aggregates;
using Jmerp.Example.Shipping.Domain.Model.LocationModel.Events;

namespace Jmerp.Example.Shipping.Domain.Model.LocationModel
{
    public class LocationState : AggregateState<LocationAggregate, LocationId, LocationState>,
        IApply<LocationCreatedEvent>
    {
        public string Name { get; private set; }

        public void Apply(LocationCreatedEvent aggregateEvent)
        {
            Name = aggregateEvent.Name;
        }
    }
}
