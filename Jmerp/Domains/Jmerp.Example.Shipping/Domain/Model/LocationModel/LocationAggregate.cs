using EventFlow.Aggregates;
using EventFlow.Exceptions;
using Jmerp.Example.Shipping.Domain.Model.LocationModel.Events;

namespace Jmerp.Example.Shipping.Domain.Model.LocationModel
{
    public class LocationAggregate : AggregateRoot<LocationAggregate, LocationId>
    {
        private readonly LocationState _state = new LocationState();

        public LocationAggregate(LocationId id) : base(id)
        {
            Register(_state);
        }

        public void Create(string name)
        {
            if (!IsNew) throw DomainError.With("Location is already created");
            Emit(new LocationCreatedEvent(name));
        }
    }
}
