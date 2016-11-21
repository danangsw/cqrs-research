using EventFlow.Specifications;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Example.Shipping.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Specifications
{
    public class RouteSpecification : Specification<Itinerary>
    {
        public RouteSpecification(
            Route route)
        {
            Route = route;
        }

        public Route Route { get; }

        protected override IEnumerable<string> IsNotSatisfiedBecause(Itinerary obj)
        {
            var itineraryDepartureLocation = obj.DepartureLocation();
            if (Route.OriginLocationId != obj.DepartureLocation())
            {
                yield return $"Route origin location '{Route.OriginLocationId}' does not match itinerary departure location '{itineraryDepartureLocation}'";
            }

            var itineraryDepartureTime = obj.DepartureTime();
            if (Route.DepartureTime.IsAfter(itineraryDepartureTime))
            {
                yield return $"Route origin depature '{Route.DepartureTime}' is after itinerary depature '{itineraryDepartureTime}'";
            }

            var itineraryArrivalLocation = obj.ArrivalLocation();
            if (Route.DestinationLocationId != obj.ArrivalLocation())
            {
                yield return $"Route destination location '{Route.DestinationLocationId}' does not match itinerary arrival location '{itineraryArrivalLocation}'";
            }

            var itineraryArrivalTime = obj.ArrivalTime();
            if (Route.ArrivalDeadline.IsBefore(itineraryArrivalTime))
            {
                yield return $"Route arrival deadline '{Route.ArrivalDeadline}' is before itinerary arrival '{itineraryArrivalTime}'";
            }
        }
    }
}
