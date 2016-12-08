using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using EventFlow.TestHelpers;
using Jmerp.Example.Shipping.Application;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Commands;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
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
                .ConfigureShippingQueriesMssql()
                .CreateResolver();

            //ExampleMigrator.Migrate();

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
            await CreateVoyageAggregatesAsync().ConfigureAwait(true);

            var voyage = _resolver.Resolve<IScheduleApplicationService>();

            await voyage.DelayScheduleAsync(
                Voyages.DallasToHelsinkiId,
                TimeSpan.FromDays(7),
                CancellationToken.None)
                .ConfigureAwait(true);
        }

        public async Task CreateVoyageAggregateAsync(Voyage voyage, Schedule schedule)
        {
            await _commandBus.PublishAsync(new VoyageCreateCommand(voyage.Id), CancellationToken.None);

            foreach (var carrierMovement in schedule.CarrierMovements)
            {
                await _commandBus.PublishAsync(new ScheduleCarrierMovementAddCommand(voyage.Id, carrierMovement), CancellationToken.None);
            }

        }

        public Task CreateVoyageAggregatesAsync()
        {
            return Task.WhenAll(
                Voyages.GetVoyages().Select(x =>
                    CreateVoyageAggregateAsync(x, x.Schedule)
                ));
        }

    }
}
