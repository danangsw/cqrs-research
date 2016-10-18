

using System;
using EventFlow.Aggregates;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel
{
    public class CargoState : AggregateState<CargoAggregate, CargoId, CargoState>,
        IApply<CargoBookedEvent>,
        IApply<CargoItinerarySetEvent>
    {
        public Route Route { get; private set; }
        public Itinerary Itinerary { get; private set; }

        public void Apply(CargoBookedEvent aggregateEvent)
        {
            Route = aggregateEvent.Route;
        }

        public void Apply(CargoItinerarySetEvent aggregateEvent)
        {
            Itinerary = aggregateEvent.Itinerary;
        }
    }
}
