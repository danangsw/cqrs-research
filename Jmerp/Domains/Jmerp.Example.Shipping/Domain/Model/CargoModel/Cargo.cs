

using EventFlow.Entities;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel
{
    public class Cargo : Entity<CargoId>
    {
        public Cargo(
            CargoId id,
            Route route,
            Itinerary itinerary)
            : base(id)
        {
            Route = route;
            Itinerary = itinerary;
        }

        public Route Route { get; }
        public Itinerary Itinerary { get; }
    }
}
