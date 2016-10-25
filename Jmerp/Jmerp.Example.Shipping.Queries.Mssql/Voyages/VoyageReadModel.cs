using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Voyages
{
    public class VoyageReadModel : IReadModel,
        IAmReadModelFor<VoyageAggregate, VoyageId, VoyageCreatedEvent>,
        IAmReadModelFor<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent>
    {
        public VoyageId Id { get; private set; }
        public Schedule Schedule { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, VoyageCreatedEvent> e)
        {
            Id = e.AggregateIdentity;
            Schedule = e.AggregateEvent.Schedule;
        }

        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent> domainEvent)
        {
            Schedule = domainEvent.AggregateEvent.Schedule;
        }

        public Voyage ToVoyage()
        {
            return new Voyage(
                Id,
                Schedule);
        }
    }
}
