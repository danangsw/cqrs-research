using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;
using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos
{
    [Table("TransportLeg")]
    public class TransportLegReadModel : IReadModel,
        IAmReadModelFor<CargoAggregate, CargoId, TransportLegAddedEvent>,
        IAmReadModelFor<CargoAggregate, CargoId, TransportLegUpdatedEvent>
    {

        public string CargoId { get; set; }
        [MsSqlReadModelIdentityColumn]
        public string TransportLegId { get; set; }
        public string LoadLocation { get; set; }
        public string UnloadLocation { get; set; }
        public DateTimeOffset LoadTime { get; set; }
        public DateTimeOffset UnloadTime { get; set; }
        public string VoyageId { get; set; }
        public string CarrierMovementId { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, TransportLegAddedEvent> domainEvent)
        {
            CargoId = domainEvent.AggregateIdentity.Value;
            var TransportLeg = domainEvent.AggregateEvent.TransportLeg;
            TransportLegId = TransportLeg.Id.Value;
            LoadLocation = TransportLeg.LoadLocation.Value;
            UnloadLocation = TransportLeg.UnloadLocation.Value;
            LoadTime = TransportLeg.LoadTime;
            UnloadTime = TransportLeg.LoadTime;
            VoyageId = TransportLeg.VoyageId.Value;
            CarrierMovementId = TransportLeg.CarrierMovementId.Value;
        }

        public Domain.Model.CargoModel.Entities.TransportLeg ToTransportLeg()
        {
            return new Domain.Model.CargoModel.Entities.TransportLeg(
               new TransportLegId(TransportLegId),
               new LocationId(LoadLocation),
               new LocationId(UnloadLocation),
               LoadTime,
               UnloadTime,
               new VoyageId(VoyageId),
               new CarrierMovementId(CarrierMovementId)
            );
        }

        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, TransportLegUpdatedEvent> domainEvent)
        {
            var TransportLeg = domainEvent.AggregateEvent.TransportLeg;
            LoadLocation = TransportLeg.LoadLocation.Value;
            UnloadLocation = TransportLeg.UnloadLocation.Value;
            LoadTime = TransportLeg.LoadTime;
            UnloadTime = TransportLeg.LoadTime;
            VoyageId = TransportLeg.VoyageId.Value;
            CarrierMovementId = TransportLeg.CarrierMovementId.Value;
        }
    }
}
