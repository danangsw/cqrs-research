using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities
{
    public class TransportLegLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            var transportLegAddedEvent = domainEvent as IDomainEvent<CargoAggregate, CargoId, TransportLegAddedEvent>;
            if (transportLegAddedEvent != null)
            {
                yield return transportLegAddedEvent.AggregateEvent.TransportLeg.Id.Value;
            }

        }
    }
}
