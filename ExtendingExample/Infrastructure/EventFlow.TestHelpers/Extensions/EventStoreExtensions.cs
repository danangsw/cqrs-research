using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.EventStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Extensions
{
    public static class EventStoreExtensions
    {
        [Obsolete("Use IAggregateStore.LoadAsync instead")]
        public static Task<TAggregate> LoadAggregateAsync<TAggregate, TIdentity>(
            this IEventStore eventStore,
            TIdentity id)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            return eventStore.LoadAggregateAsync<TAggregate, TIdentity>(id, CancellationToken.None);
        }
    }
}
