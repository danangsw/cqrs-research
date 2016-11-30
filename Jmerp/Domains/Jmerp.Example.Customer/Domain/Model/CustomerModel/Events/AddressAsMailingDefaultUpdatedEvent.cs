using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsMailingDefaultUpdated", 1)]
    public class AddressAsMailingDefaultUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressId AddressId { get; }
        public AddressAsMailingDefaultUpdatedEvent(AddressId addressId)
        {
            AddressId = addressId;
        }
    }
}
