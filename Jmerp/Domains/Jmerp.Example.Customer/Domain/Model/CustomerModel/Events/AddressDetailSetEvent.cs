using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressDetailSet", 1)]
    public class AddressDetailSetEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public AddressDetail AddressDetail { get; }

        public AddressDetailSetEvent(AddressDetail addressDetail)
        {
            AddressDetail = addressDetail;
        }
    }
}
