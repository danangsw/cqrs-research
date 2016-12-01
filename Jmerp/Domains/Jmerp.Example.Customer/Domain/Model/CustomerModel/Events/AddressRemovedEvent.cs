using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressRemoved", 1)]
    public class AddressRemovedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressDetail AddressDetail { get; }
        public AddressRemovedEvent(AddressDetail addressDetail)
        {
            AddressDetail = addressDetail;
        }
    }
}
