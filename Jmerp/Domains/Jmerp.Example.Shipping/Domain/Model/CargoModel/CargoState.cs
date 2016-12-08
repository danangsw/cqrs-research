

using System;
using EventFlow.Aggregates;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel
{
    public class CargoState : AggregateState<CargoAggregate, CargoId, CargoState>,
        IApply<CargoBookedEvent>,
        IApply<TransportLegAddedEvent>,
        IApply<TransportLegUpdatedEvent>,
        IApply<TransportLegDeletedEvent>,
        IApply<CargoItinerarySetEvent>
    {
        public Route Route { get; private set; }
        public Itinerary Itinerary { get; private set; }

        public void Init()
        {
            if (Itinerary == null)
            {
                Itinerary = new Itinerary();
            }
        }

        public void Apply(CargoBookedEvent aggregateEvent)
        {
            Route = aggregateEvent.Route;
        }

        public void Apply(CargoItinerarySetEvent aggregateEvent)
        {

        }

        public void Apply(TransportLegAddedEvent aggregateEvent)
        {
            Itinerary = Itinerary.AddTransportLeg(aggregateEvent.TransportLeg);
        }

        public void Apply(TransportLegUpdatedEvent aggregateEvent)
        {
            Itinerary = Itinerary.UpdateTransportLeg(aggregateEvent.TransportLeg);
        }

        public void Apply(TransportLegDeletedEvent aggregateEvent)
        {
            Itinerary = Itinerary.DeleteTransportLeg(aggregateEvent.TransportLeg);
        }
    }
}
