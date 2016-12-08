

using System;
using EventFlow.Aggregates;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel
{
    public class VoyageState : AggregateState<VoyageAggregate, VoyageId, VoyageState>,
        IApply<VoyageCreatedEvent>,
        IApply<CarrierMovementAddedEvent>,
        IApply<CarrierMovementUpdatedEvent>,
        IApply<VoyageScheduleUpdatedEvent>
    {
        public Schedule Schedule { get; private set; }


        public void Init()
        {
            if (Schedule == null)
            {
                Schedule = new Schedule();
            }
        }

        public void Apply(CarrierMovementUpdatedEvent aggregateEvent)
        {
            Schedule = Schedule.UpdateCarrierMovement(aggregateEvent.CarrierMovement);
        }

        public void Apply(CarrierMovementAddedEvent aggregateEvent)
        {
            Schedule = Schedule.AddCarrierMovement(aggregateEvent.CarrierMovement);
        }

        public void Apply(VoyageCreatedEvent aggregateEvent)
        {

        }

        public void Apply(VoyageScheduleUpdatedEvent aggregateEvent)
        {

        }
    }
}
