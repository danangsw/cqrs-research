using EventFlow.TestHelpers;
using FluentAssertions;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.ExternalServices.Routing;
using Jmerp.Example.Shipping.Tests.Moqs;
using NUnit.Framework;

namespace Jmerp.Example.Shipping.Tests.UnitTests.ExternalServices.Routing
{
    [Category(Categories.Unit)]
    public class RoutingServiceTests : TestsFor<RoutingService>
    {
        [Test]
        public void Itinerary()
        {
            // Arrange
            var hongkongToNewYork = new Voyage(Voyages.HongkongToNewYorkId, Voyages.HongkongToNewYorkSchedule);
            var newYorkToDallas = new Voyage(Voyages.NewYorkToDallasId, Voyages.NewYorkToDallasSchedule);

            // Act
            var itineraries = Sut.CalculateItineraries(
                new Route(
                    Locations.Tokyo,
                    Locations.Chicago,
                    1.October(2008).At(11, 00),
                    1.January(2014)),
                new[] { hongkongToNewYork, newYorkToDallas });

            // Assert
            // TODO: Assert list of legs
            itineraries.Should().HaveCount(1);
        }
    }
}
