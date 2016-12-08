using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Voyages
{
    [Table("Voyage")]
    public class VoyageReadModel : MssqlReadModel,
         IAmReadModelFor<VoyageAggregate, VoyageId, VoyageCreatedEvent>
    {
        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, VoyageCreatedEvent> domainEvent)
        {

        }

        public Domain.Model.VoyageModel.Voyage ToVoyage(VoyageId AggregateId, Schedule Schedule)
        {
            return new Domain.Model.VoyageModel.Voyage(
               AggregateId,
               Schedule
            );
        }
    }
}
