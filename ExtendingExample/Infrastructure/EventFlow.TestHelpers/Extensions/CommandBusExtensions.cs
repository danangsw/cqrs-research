using EventFlow.Aggregates;
using EventFlow.Commands;
using EventFlow.Core;
using System.Threading;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Extensions
{
    public static class CommandBusExtensions
    {
        public static Task<ISourceId> PublishAsync<TAggregate, TIdentity, TSourceIdentity>(
            this ICommandBus commandBus,
            ICommand<TAggregate, TIdentity, TSourceIdentity> command)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TSourceIdentity : ISourceId
        {
            return commandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}
