using EventFlow.Aggregates;
using EventFlow.Extensions;
using Example.Shipping.Domain.Model.VoyageModel.Entities;
using Example.Shipping.Domain.Model.VoyageModel.Events;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel
{
    public class VoyageAggregate : AggregateRoot<VoyageAggregate, VoyageId>
    {
        private readonly VoyageState _state = new VoyageState();

        public VoyageAggregate(VoyageId id) : base(id)
        {
            _state.Init();
            Register(_state);
        }

        public Schedule Schedule => _state.Schedule;

        public void Create()
        {
            Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);

            Emit(new VoyageCreatedEvent());
        }

        public void AddSheduleCarrierMovement(CarrierMovement carrierMovement)
        {
            Emit(new CarrierMovementAddedEvent(carrierMovement));
        }

        public void UpdateScheduleCarrierMovement(CarrierMovement carrierMovement)
        {
            Emit(new CarrierMovementUpdatedEvent(carrierMovement));
        }

        public void Delay(TimeSpan delay)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            if (delay == TimeSpan.Zero) return;

            var delayedSchedule = Schedule.Delay(delay);

            delayedSchedule.CarrierMovements.ToList().ForEach(delegate (CarrierMovement carrierMovement)
            {
                UpdateScheduleCarrierMovement(carrierMovement);
            });

            Emit(new VoyageScheduleUpdatedEvent(delayedSchedule));
        }
    }
}
