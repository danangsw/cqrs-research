
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using System.Collections.Generic;

namespace Jmerp.Example.Shipping.Queries.InMemory.Cargos
{
    public class CargoReadModel : IReadModel,
        IAmReadModelFor<CargoAggregate, CargoId, CargoItinerarySetEvent>,
        IAmReadModelFor<CargoAggregate, CargoId, CargoBookedEvent>
    {
        public CargoId Id { get; private set; }
        public HashSet<VoyageId> DependentVoyageIds { get; } = new HashSet<VoyageId>();
        public Itinerary Itinerary { get; private set; }
        public Route Route { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, CargoBookedEvent> domainEvent)
        {

            Id = domainEvent.AggregateIdentity;
            Route = domainEvent.AggregateEvent.Route;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, CargoItinerarySetEvent> domainEvent)
        {
            Itinerary = domainEvent.AggregateEvent.Itinerary;
            foreach (var transportLeg in domainEvent.AggregateEvent.Itinerary.TransportLegs)
            {
                DependentVoyageIds.Add(transportLeg.VoyageId);
            }
        }

        public Cargo ToCargo()
        {
            return new Cargo(
                Id,
                Route,
                Itinerary);
        }
    }
}
