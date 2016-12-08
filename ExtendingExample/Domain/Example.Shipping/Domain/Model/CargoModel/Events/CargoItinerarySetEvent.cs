using EventFlow.Aggregates;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Events
{
    public class CargoItinerarySetEvent : AggregateEvent<CargoAggregate, CargoId>
    {
        public CargoItinerarySetEvent(
            Itinerary itinerary)
        {
            Itinerary = itinerary;
        }

        public Itinerary Itinerary { get; }
    }
}
