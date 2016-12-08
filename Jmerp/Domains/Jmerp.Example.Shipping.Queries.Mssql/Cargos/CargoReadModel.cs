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
    [Table("Cargo")]
    public class CargoReadModel : MssqlReadModel,
        IAmReadModelFor<CargoAggregate, CargoId, CargoBookedEvent>
    {
        public string OriginLocationId { get; set; }
        public string DestinationLocationId { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalDeadline { get; set; }


        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, CargoBookedEvent> domainEvent)
        {
            OriginLocationId = domainEvent.AggregateEvent.Route.OriginLocationId.Value;
            DestinationLocationId = domainEvent.AggregateEvent.Route.DestinationLocationId.Value;
            DepartureTime = domainEvent.AggregateEvent.Route.DepartureTime;
            ArrivalDeadline = domainEvent.AggregateEvent.Route.ArrivalDeadline;

        }


        public Route ToRoute()
        {
            return new Route(new Domain.Model.LocationModel.LocationId(OriginLocationId),
                            new Domain.Model.LocationModel.LocationId(DestinationLocationId),
                            DepartureTime,
                            ArrivalDeadline);
        }

        public Domain.Model.CargoModel.Cargo ToCargo(CargoId AggregateId, Route Route, Itinerary Itinerary)
        {
            return new Domain.Model.CargoModel.Cargo(
               AggregateId,
               Route,
               Itinerary
            );
        }

    }
}
