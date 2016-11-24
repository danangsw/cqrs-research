using EventFlow.MsSql.ReadStores;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Example.Shipping.Domain.Model.VoyageModel;
using Example.Shipping.Domain.Model.VoyageModel.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using Example.Shipping.Domain.Model.VoyageModel.Entities;

namespace Example.Shipping.Queries.Mssql.Voyage
{
    [Table("CarrierMovement")]
    public class CarrierMovementReadModel : IReadModel,
        IAmReadModelFor<VoyageAggregate, VoyageId, CarrierMovementAddedEvent>,
        IAmReadModelFor<VoyageAggregate, VoyageId, CarrierMovementUpdatedEvent>
    {

        public string VoyageId { get; set; }
        [MsSqlReadModelIdentityColumn]
        public string CarrierMovementId { get; set; }
        public string DepartureLocationId { get; set; }
        public string ArrivalLocationId { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }



        public void Apply(IReadModelContext context, EventFlow.Aggregates.IDomainEvent<VoyageAggregate, VoyageId, CarrierMovementAddedEvent> domainEvent)
        {
            VoyageId = domainEvent.AggregateIdentity.Value;
            var CarrierMovement = domainEvent.AggregateEvent.CarrierMovement;
            CarrierMovementId = CarrierMovement.Id.Value;
            DepartureLocationId = CarrierMovement.DepartureLocationId.Value;
            ArrivalLocationId = CarrierMovement.ArrivalLocationId.Value;
            DepartureTime = CarrierMovement.DepartureTime;
            ArrivalTime = CarrierMovement.ArrivalTime;
        }

        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, CarrierMovementUpdatedEvent> domainEvent)
        {
            var CarrierMovement = domainEvent.AggregateEvent.CarrierMovement;
            DepartureTime = CarrierMovement.DepartureTime;
            ArrivalTime = CarrierMovement.ArrivalTime;
        }

        public Domain.Model.VoyageModel.Entities.CarrierMovement ToCarrierMovement()
        {
            return new Domain.Model.VoyageModel.Entities.CarrierMovement(
                   new CarrierMovementId(CarrierMovementId),
                   new Domain.Model.LocationModel.LocationId(DepartureLocationId),
                   new Domain.Model.LocationModel.LocationId(ArrivalLocationId),
                   DepartureTime,
                   ArrivalTime
              );
        }
    }
}
