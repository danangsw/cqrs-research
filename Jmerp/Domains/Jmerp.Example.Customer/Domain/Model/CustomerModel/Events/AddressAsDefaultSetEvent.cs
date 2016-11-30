using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressAsDefaultSet", 1)]
    public class AddressAsDefaultSetEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressDetail AddressDetail { get; private set; }

        public AddressAsDefaultSetEvent(AddressDetail addressDetail)
        {
            AddressDetail = addressDetail;
        }
    }
}
