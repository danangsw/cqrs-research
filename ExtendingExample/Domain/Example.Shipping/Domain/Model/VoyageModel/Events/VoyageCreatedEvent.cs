using EventFlow.Aggregates;
using EventFlow.EventStores;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Events
{
    [EventVersion("VoyageCreated", 1)]
    public class VoyageCreatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public VoyageCreatedEvent(
            )
        {
        }

    }
}
