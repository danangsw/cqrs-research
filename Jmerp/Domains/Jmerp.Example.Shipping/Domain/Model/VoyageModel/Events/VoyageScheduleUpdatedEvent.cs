using EventFlow.Aggregates;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events
{
    public class VoyageScheduleUpdatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public VoyageScheduleUpdatedEvent(
            Schedule schedule)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }
}
