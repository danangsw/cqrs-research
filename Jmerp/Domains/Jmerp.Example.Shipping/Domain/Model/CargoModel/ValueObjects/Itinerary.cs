using EventFlow.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Extensions;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects
{
    public class Itinerary : ValueObject
    {
        public Itinerary(
            IEnumerable<TransportLeg> transportLegs)
        {
            var legsList = (transportLegs ?? Enumerable.Empty<TransportLeg>()).ToList();

            if (!legsList.Any()) throw new ArgumentException(nameof(transportLegs));

            (new TransportLegsAreConnectedSpecification()).ThrowDomainErrorIfNotStatisfied(legsList);

            TransportLegs = legsList;
        }

        public IReadOnlyList<TransportLeg> TransportLegs { get; private set; }
        public LocationId DepartureLocation()
        {
            return TransportLegs.First().LoadLocation;
        }

        public DateTimeOffset DepartureTime()
        {
            return TransportLegs.First().UnloadTime;
        }

        public DateTimeOffset ArrivalTime()
        {
            return TransportLegs.Last().UnloadTime;
        }

        public LocationId ArrivalLocation()
        {
            return TransportLegs.Last().UnloadLocation;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return TransportLegs;
        }
    }
}
