using EventFlow.Entities;
using Example.Shipping.Domain.Model.LocationModel;
using Example.Shipping.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Entities
{
    public class CarrierMovement : Entity<CarrierMovementId>
    {
        public CarrierMovement(
            CarrierMovementId id,
            LocationId departureLocationId,
            LocationId arrivalLocationId,
            DateTimeOffset departureTime,
            DateTimeOffset arrivalTime)
            : base(id)
        {
            if (departureLocationId == null) throw new ArgumentNullException(nameof(departureLocationId));
            if (arrivalLocationId == null) throw new ArgumentNullException(nameof(arrivalLocationId));
            if (departureTime == default(DateTimeOffset)) throw new ArgumentOutOfRangeException(nameof(departureTime));
            if (arrivalTime == default(DateTimeOffset)) throw new ArgumentOutOfRangeException(nameof(arrivalTime));
            if (departureTime.IsAfter(arrivalTime)) throw new ArgumentException("Arrival time must be after departure");

            DepartureLocationId = departureLocationId;
            ArrivalLocationId = arrivalLocationId;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public LocationId DepartureLocationId { get; }
        public LocationId ArrivalLocationId { get; }
        public DateTimeOffset DepartureTime { get; }
        public DateTimeOffset ArrivalTime { get; }

        
    }
}
