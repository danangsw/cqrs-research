using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.TestHelpers;
using FluentAssertions;
using Jmerp.Example.Shipping.Application;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Commands;
using Jmerp.Example.Shipping.Queries.InMemory;
using Jmerp.Example.Shipping.Tests.Moqs;
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
    public class Scenarios : Test
    {
        private IRootResolver _resolver;
        private IAggregateStore _aggregateStore;
        private ICommandBus _commandBus;

        [SetUp]
        public void SetUp()
        {
            _resolver = EventFlowOptions.New
                .ConfigureShippingDomain()
                .ConfigureShippingQueriesInMemory()
                .CreateResolver();
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
            await CreateLocationAggregatesAsync().ConfigureAwait(false);
            await CreateVoyageAggregatesAsync().ConfigureAwait(false);

            var route = new Route(
                Locations.Tokyo,
                Locations.Helsinki,
                1.October(2008).At(11, 00),
                6.November(2008).At(12, 00));

            var booking = _resolver.Resolve<IBookingApplicationService>();
            await booking.BookCargoAsync(route, CancellationToken.None).ConfigureAwait(false);

            var voyage = _resolver.Resolve<IScheduleApplicationService>();

            await voyage.DelayScheduleAsync(
                Voyages.DallasToHelsinkiId,
                TimeSpan.FromDays(7),
                CancellationToken.None)
                .ConfigureAwait(false);
        }

        private Task CreateVoyageAggregatesAsync()
        {
            return Task.WhenAll(Voyages.GetVoyages().Select(CreateVoyageAggregateAsync));
        }

        private Task CreateVoyageAggregateAsync(Voyage voyage)
        {
            return _commandBus.PublishAsync(new VoyageCreateCommand(voyage.Id, voyage.Schedule), CancellationToken.None);
        }

        private Task CreateLocationAggregatesAsync()
        {
            return Task.WhenAll(Locations.GetLocations().Select(CreateLocationAggregateAsync));
        }

        private Task CreateLocationAggregateAsync(Location location)
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
