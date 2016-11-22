using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Commons;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    public class CustomerDomainErrorReceivedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public SubscriberResult SubscriberResult { get; private set; }

        public CustomerDomainErrorReceivedEvent(
          SubscriberResult subscriberResult)
        {
            SubscriberResult = subscriberResult;
        }
    }
}
