using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using EventFlow.TestHelpers;
using Jmerp.Db.Infrastructure;
using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using Jmerp.Example.Shipping.Queries.Mssql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Tests.IntegrationTests
{
    public class IntegrationLocationTest : Test
    {
        private IRootResolver _resolver;
        private IAggregateStore _aggregateStore;
        private ICommandBus _commandBus;

        [SetUp]
        public void SetUp()
        {
            _resolver = EventFlowOptions.New
                .ConfigureMsSql(MsSqlConfiguration.New
                .SetConnectionString(@"Server=.;Database=ExampleDB;User Id=sa;Password=123456"))
                .UseMssqlEventStore()
                .ConfigureShipping()
                .ConfigureShippingQueriesMssql()
                .CreateResolver();

            JmerpMigrator.Migrate();

            var msSqlDatabaseMigrator = _resolver.Resolve<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);

            _aggregateStore = _resolver.Resolve<IAggregateStore>();
            _commandBus = _resolver.Resolve<ICommandBus>();

        }


        [TearDown]
        public void TearDown()
        {
            //ExampleMigrator.Down();
            _resolver.DisposeSafe(new ConsoleLog(), "");
        }

        [Test]
        public async Task Simple()
        {
            await CreateLocationAggregatesAsync().ConfigureAwait(false);
        }

        public Task CreateLocationAggregatesAsync()
        {
            return Task.WhenAll(Locations.GetLocations().Select(CreateLocationAggregateAsync));
        }

        public Task CreateLocationAggregateAsync(Location location)
        {
            return UpdateAsync<LocationAggregate, LocationId>(location.Id, a => a.Create(location.Name));
        }


        private async Task UpdateAsync<TAggregate, TIdentity>(TIdentity id, Action<TAggregate> action)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
        {
            await _aggregateStore.UpdateAsync<TAggregate, TIdentity>(
                id,
                SourceId.New,
                (a, c) =>
                {
                    action(a);
                    return Task.FromResult(0);
                },
                CancellationToken.None)
                .ConfigureAwait(false);
        }

    }
}
