using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsShippingDefaultUpdated", 1)]
    public class AddressAsShippingDefaultUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressDetail AddressDetail { get; }
        public AddressAsShippingDefaultUpdatedEvent(AddressDetail addressDetail)
        {
            AddressDetail = addressDetail;
        }
    }
}
