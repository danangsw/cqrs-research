using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using EventFlow.TestHelpers;
using Example.Db.Infrastructure;
using Example.Shipping.Application;
using Example.Shipping.Domain.Model.VoyageModel;
using Example.Shipping.Domain.Model.VoyageModel.Commands;
using Example.Shipping.Queries.Mssql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Tests.IntegrationTests
{
    [Category(Categories.Integration)]
    public class IntegrationVoyageTest : Test
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
                .ConfigureShipingQueriesMssql()
                .CreateResolver();

            ExampleMigrator.Migrate();

            var msSqlDatabaseMigrator = _resolver.Resolve<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);

            _aggregateStore = _resolver.Resolve<IAggregateStore>();
            _commandBus = _resolver.Resolve<ICommandBus>();
        }

        [TearDown]
        public void TearDown()
        {
            _resolver.DisposeSafe(new ConsoleLog(), "");
        }


        [Test]
        public async Task Simple()
        {
            await CreateVoyageAggregatesAsync().ConfigureAwait(true);

            var voyage = _resolver.Resolve<IScheduleApplicationService>();

            await voyage.DelayScheduleAsync(
                Voyages.DallasToHelsinkiId,
                TimeSpan.FromDays(7),
                CancellationToken.None)
                .ConfigureAwait(true);
        }

        public Task CreateVoyageAggregateAsync(Voyage voyage)
        {
            return  _commandBus.PublishAsync(new VoyageCreateCommand(voyage.Id, voyage.Schedule), CancellationToken.None);
        }

        public Task CreateVoyageAggregatesAsync()
        {
            return Task.WhenAll(Voyages.GetVoyages().Select(CreateVoyageAggregateAsync));
        }

    }
}
