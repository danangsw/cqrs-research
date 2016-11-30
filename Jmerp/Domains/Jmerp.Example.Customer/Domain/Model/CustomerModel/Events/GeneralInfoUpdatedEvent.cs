using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("GeneralInfoUpdated", 1)]
    public class GeneralInfoUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public GeneralInfo GeneralInfo { get; }

        public GeneralInfoUpdatedEvent(GeneralInfo generalInfo)
        {
            GeneralInfo = generalInfo;
        }
    }
}
