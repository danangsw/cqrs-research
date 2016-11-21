using EventFlow.ValueObjects;
using Example.Shipping.Domain.Model.CargoModel.Specifications;
using Example.Shipping.Domain.Model.LocationModel;
using Example.Shipping.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.ValueObjects
{
    public class Route : ValueObject
    {
        public Route(
            LocationId originLocationId,
            LocationId destinationLocationId,
            DateTimeOffset departureTime,
            DateTimeOffset arrivalDeadline)
        {
            if (originLocationId == null) throw new ArgumentNullException(nameof(originLocationId));
            if (destinationLocationId == null) throw new ArgumentNullException(nameof(destinationLocationId));
            if (arrivalDeadline == default(DateTimeOffset)) throw new ArgumentOutOfRangeException(nameof(arrivalDeadline));
            if (departureTime == default(DateTimeOffset)) throw new ArgumentOutOfRangeException(nameof(departureTime));
            if (originLocationId == destinationLocationId) throw new ArgumentException("Origin and destination cannot be the same");
            if (departureTime.IsAfter(arrivalDeadline)) throw new ArgumentException("Departure must be before arrival");

            OriginLocationId = originLocationId;
            DestinationLocationId = destinationLocationId;
            DepartureTime = departureTime;
            ArrivalDeadline = arrivalDeadline;
        }

        public LocationId OriginLocationId { get; }
        public LocationId DestinationLocationId { get; }
        public DateTimeOffset DepartureTime { get; }
        public DateTimeOffset ArrivalDeadline { get; }

        public RouteSpecification Specification()
        {
            return new RouteSpecification(this);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OriginLocationId;
            yield return DestinationLocationId;
            yield return DepartureTime;
            yield return ArrivalDeadline;
        }
    }
}
