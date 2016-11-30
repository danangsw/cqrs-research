using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;


namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsShippingAddressUpdated", 1)]
    public class AddressAsShippingAddressUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressId AddressId { get; }
        public AddressAsShippingAddressUpdatedEvent(AddressId addressId)
        {
            AddressId = addressId;
        }
    }
}
