using EventFlow.MsSql.ReadStores;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;
using Example.Shipping.Domain.Model.LocationModel;
using Example.Shipping.Domain.Model.LocationModel.Events;

namespace Example.Shipping.Queries.Mssql.Locations
{
    [Table("Location")]
    public class LocationReadModel : MssqlReadModel,
        IAmReadModelFor<LocationAggregate, LocationId, LocationCreatedEvent>
    {

        public string Name { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<LocationAggregate, LocationId, LocationCreatedEvent> domainEvent)
        {
            Name = domainEvent.AggregateEvent.Name;
        }
    }
}
