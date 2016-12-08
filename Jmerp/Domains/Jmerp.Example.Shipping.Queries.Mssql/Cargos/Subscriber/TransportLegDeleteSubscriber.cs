using EventFlow.Aggregates;
using EventFlow.Queries;
using EventFlow.Subscribers;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Events;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos.Subscriber
{
    public class TransportLegDeleteSubscriber
        : ISubscribeSynchronousTo<CargoAggregate, CargoId, TransportLegDeletedEvent>
    {

        private readonly IQueryProcessor _queryProcessor;

        public TransportLegDeleteSubscriber(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }


        public async Task HandleAsync(IDomainEvent<CargoAggregate, CargoId, TransportLegDeletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var schedules = await _queryProcessor.ProcessAsync(new DeleteTransportLegQuery(domainEvent.AggregateEvent.TransportLeg.Id), cancellationToken).ConfigureAwait(false);
        }
    }
}
