using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Events
{
    [EventVersion("TransportLegAdded", 1)]
    public class TransportLegAddedEvent : AggregateEvent<CargoAggregate, CargoId>
    {
        public TransportLegAddedEvent(
            TransportLeg transportLeg
        )
        {
            TransportLeg = transportLeg;
        }

        public TransportLeg TransportLeg { get; }
    }
}
