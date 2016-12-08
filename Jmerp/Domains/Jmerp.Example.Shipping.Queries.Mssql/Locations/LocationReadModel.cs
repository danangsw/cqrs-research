using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using Jmerp.Example.Shipping.Domain.Model.LocationModel.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Locations
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
