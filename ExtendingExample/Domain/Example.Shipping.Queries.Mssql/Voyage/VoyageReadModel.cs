using EventFlow.MsSql.ReadStores;
using EventFlow.ReadStores;
using Example.Shipping.Domain.Model.VoyageModel;
using Example.Shipping.Domain.Model.VoyageModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Shipping.Queries.Mssql.Voyage
{
    [Table("Voyage")]
    public class VoyageReadModel : MssqlReadModel,
         IAmReadModelFor<VoyageAggregate, VoyageId, VoyageCreatedEvent>
    {
        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, VoyageCreatedEvent> domainEvent)
        {
           
        }
    }
}
