using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos
{
    public class CargoReadModel : IMssqlReadModel,
        IAmReadModelFor<CargoAggregate, CargoId, CargoItinerarySetEvent>,
        IAmReadModelFor<CargoAggregate, CargoId, CargoBookedEvent>
    {

        [MsSqlReadModelIdentityColumn]
        public CargoId Id { get; private set; }

        public int MsSqlReadModelVersionColumn { get; private set; }

        public HashSet<VoyageId> DependentVoyageIds { get; } = new HashSet<VoyageId>();
        public Itinerary Itinerary { get; private set; }
        public Route Route { get; private set; }

        public string AggregateId
        {
            get; set;
        }

        public DateTimeOffset CreateTime
        {
            get; set;
        }

        public DateTimeOffset UpdatedTime
        {
            get; set;
        }

        public int LastAggregateSequenceNumber
        {
            get; set;
        }

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
