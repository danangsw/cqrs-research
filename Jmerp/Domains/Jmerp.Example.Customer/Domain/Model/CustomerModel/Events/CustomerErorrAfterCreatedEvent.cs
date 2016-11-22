using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Commons;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("CustomerErorrAfterCreated", 1)]
    public class CustomerErorrAfterCreatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
    }

    [EventVersion("CustomerDomainResponded", 1)]
    public class CustomerDomainRespondedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public SubscriberResult Result { get; private set; }

        public CustomerDomainRespondedEvent(
            SubscriberResult result)
        {
            Result = result;
        }
    }

}
