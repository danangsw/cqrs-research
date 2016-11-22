using EventFlow.Aggregates;
using EventFlow.EventStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.LocationModel.Events
{
    [EventVersion("LocationCreated", 1)]
    public class LocationCreatedEvent : AggregateEvent<LocationAggregate, LocationId>
    {
        public LocationCreatedEvent(
            string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
