using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsBillingDefaultUpdated", 1)]
    public class AddressAsBillingDefaultUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressDetail AddressDetail { get; private set; }
        public AddressAsBillingDefaultUpdatedEvent(AddressDetail addressDetail)
        {
            AddressDetail = addressDetail;
        }
    }
}
