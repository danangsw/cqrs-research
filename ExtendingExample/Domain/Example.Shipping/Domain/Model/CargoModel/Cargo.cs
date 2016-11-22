using EventFlow.Entities;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel
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
