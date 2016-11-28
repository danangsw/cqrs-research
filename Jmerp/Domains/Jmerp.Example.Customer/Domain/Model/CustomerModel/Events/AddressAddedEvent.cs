using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAdded", 1)]
    public class AddressAddedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressDetail AddressDetail { get; private set; }

        public AddressAddedEvent(AddressDetail addressDetail)
        {
            AddressDetail = addressDetail;
        }
    }
}
