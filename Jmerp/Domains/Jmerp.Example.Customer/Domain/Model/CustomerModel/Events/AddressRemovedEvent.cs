using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;


namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressRemoved", 1)]
    public class AddressRemovedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressId AddressId { get; }
        public AddressRemovedEvent(AddressId addressId)
        {
            AddressId = addressId;
        }
    }
}
