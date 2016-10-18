

using System;
using EventFlow.Aggregates;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel
{
    public class VoyageState : AggregateState<VoyageAggregate, VoyageId, VoyageState>,
    IApply<VoyageCreatedEvent>,
    IApply<VoyageScheduleUpdatedEvent>
    {
        public Schedule Schedule { get; private set; }

        public void Apply(VoyageCreatedEvent aggregateEvent)
        {
            Schedule = aggregateEvent.Schedule;
        }

        public void Apply(VoyageScheduleUpdatedEvent aggregateEvent)
        {
            Schedule = aggregateEvent.Schedule;
        }
    }
}
