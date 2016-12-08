using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities
{
    public class CarrierMovementLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            var carrierMovementAddedEvent = domainEvent as IDomainEvent<VoyageAggregate, VoyageId, CarrierMovementAddedEvent>;
            if (carrierMovementAddedEvent != null)
            {
                yield return carrierMovementAddedEvent.AggregateEvent.CarrierMovement.Id.Value;
            }

            var carrierMovementUpdatedEvent = domainEvent as IDomainEvent<VoyageAggregate, VoyageId, CarrierMovementUpdatedEvent>;
            if (carrierMovementUpdatedEvent != null)
            {
                yield return carrierMovementUpdatedEvent.AggregateEvent.CarrierMovement.Id.Value;
            }
        }
    }
}
