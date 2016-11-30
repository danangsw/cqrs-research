using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsMailingAddressUpdated", 1)]
    public class AddressAsMailingAddressUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressId AddressId { get; }
        public AddressAsMailingAddressUpdatedEvent(AddressId addressId)
        {
            AddressId = addressId;
        }
    }
}
