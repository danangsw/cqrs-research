using EventFlow.Aggregates;

namespace Jmerp.Example.Shipping.Domain.Model.LocationModel
{
    public class LocationState : AggregateState<LocationAggregate, LocationId, LocationState>
    {
        public string Name { get; private set; }
    }
}
