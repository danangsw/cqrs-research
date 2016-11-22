using EventFlow.Aggregates;
using Example.Shipping.Domain.Model.LocationModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.LocationModel
{
    public class LocationState : AggregateState<LocationAggregate, LocationId, LocationState>,
        IApply<LocationCreatedEvent>
    {
        public string Name { get; private set; }


        public void Apply(LocationCreatedEvent aggregateEvent)
        {
            Name = aggregateEvent.Name;
        }
    }
}
