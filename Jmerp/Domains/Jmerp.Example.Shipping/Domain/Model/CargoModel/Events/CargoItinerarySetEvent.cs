

using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Events
{
    [EventVersion("CargoItinerarySet",1)]
    public class CargoItinerarySetEvent:AggregateEvent<CargoAggregate, CargoId>
    {
        public CargoItinerarySetEvent(
            Itinerary itinerary)
        {
            Itinerary = itinerary;
        }

        public Itinerary Itinerary { get;  }
    }
}
