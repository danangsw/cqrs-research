using EventFlow.Extensions;
using EventFlow.ValueObjects;
using Example.Shipping.Domain.Model.CargoModel.Entities;
using Example.Shipping.Domain.Model.CargoModel.Specifications;
using Example.Shipping.Domain.Model.LocationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.ValueObjects
{
    public class Itinerary : ValueObject
    {
        public Itinerary()
        {
            TransportLegs = Enumerable.Empty<TransportLeg>().ToList();
        }

        public Itinerary(
            IEnumerable<TransportLeg> transportLegs)
        {
            var legsList = (transportLegs ?? Enumerable.Empty<TransportLeg>()).ToList();

            if (!legsList.Any()) throw new ArgumentException(nameof(transportLegs));
            (new TransportLegsAreConnectedSpecification()).ThrowDomainErrorIfNotStatisfied(legsList);

            TransportLegs = legsList;
        }

        public IReadOnlyList<TransportLeg> TransportLegs { get; }

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

        public Itinerary AddTransportLeg(TransportLeg transportLeg)
        {
            var newListTransportLeg = new List<TransportLeg>();
            if (TransportLegs != null)
            {
                newListTransportLeg.AddRange(TransportLegs);
            }
            newListTransportLeg.Add(transportLeg);
            return new Itinerary(newListTransportLeg);
        }
    }
}
