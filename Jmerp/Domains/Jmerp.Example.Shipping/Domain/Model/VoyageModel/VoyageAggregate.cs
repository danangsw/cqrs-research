using EventFlow.Aggregates;
using EventFlow.Entities;
using EventFlow.Extensions;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel
{
    public class VoyageAggregate : AggregateRoot<VoyageAggregate, VoyageId>
    {
        public VoyageAggregate(VoyageId id) : base(id)
        {
        }

        private readonly VoyageState _state = new VoyageState();

        public Schedule Schedule => _state.Schedule;

        public void Create(Schedule schedule)
        {
            Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);

            Emit(new VoyageCreatedEvent(schedule));
        }

        public void Delay(TimeSpan delay)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            if (delay == TimeSpan.Zero) return;

            var delayedSchedule = Schedule.Delay(delay);

            Emit(new VoyageScheduleUpdatedEvent(delayedSchedule));
        }
    }
}
