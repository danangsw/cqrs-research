using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("CustomerCreated",1)]
    public class CustomerCreatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public GeneralInfo GeneralInfo { get; }

        public CustomerCreatedEvent(GeneralInfo generalInfo)
        {
            GeneralInfo = generalInfo;
        }
    }
}
