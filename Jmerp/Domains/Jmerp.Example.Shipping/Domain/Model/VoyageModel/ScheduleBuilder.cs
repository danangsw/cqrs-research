using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel
{
    public class ScheduleBuilder
    {
        private readonly List<CarrierMovement> _carrierMovements = new List<CarrierMovement>();
        private LocationId _departureLocation;

        public ScheduleBuilder(
            LocationId departureLocation)
        {
            _departureLocation = departureLocation;
        }

        public ScheduleBuilder Add(
            LocationId arrivalLocationId,
            DateTimeOffset departureTime,
            DateTimeOffset arrivalTime)
        {
            _carrierMovements.Add(new CarrierMovement(
                CarrierMovementId.New,
                _departureLocation,
                arrivalLocationId,
                departureTime,
                arrivalTime));

            _departureLocation = arrivalLocationId;

            return this;
        }

        public Schedule Build()
        {
            return new Schedule(_carrierMovements);
        }
    }
}
