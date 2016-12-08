using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events
{
    [EventVersion("VoyageCreated", 1)]
    public class VoyageCreatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public VoyageCreatedEvent(
            )
        {

        }
    }
}
