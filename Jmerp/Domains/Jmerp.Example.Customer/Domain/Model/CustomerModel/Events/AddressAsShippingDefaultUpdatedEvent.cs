using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsShippingDefaultUpdated", 1)]
    public class AddressAsShippingDefaultUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressId AddressId { get; }
        public AddressAsShippingDefaultUpdatedEvent(AddressId addressId)
        {
            AddressId = addressId;
        }
    }
}
