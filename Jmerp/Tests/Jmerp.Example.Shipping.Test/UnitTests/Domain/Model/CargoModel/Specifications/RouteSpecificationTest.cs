using EventFlow.TestHelpers;
using FluentAssertions;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Specifications;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using Jmerp.Example.Shipping.Tests.Mocks;
using NUnit.Framework;

namespace Jmerp.Example.Shipping.Tests.UnitTests.Domain.Model.CargoModel.Specifications
{
    [Category(Categories.Unit)]
    public class RouteSpecificationTest : Test
    {
        [Test]
        public void Valid()
        {
            //Arrange
            var route = new Route(Locations.NewYork, Locations.Chicago, 1.January(2000), 4.January(2000));
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 2.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Chicago, 3.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };
            var itinerary = new Itinerary(transportLegs);

            var spec = new RouteSpecification(route);

            //Act
            var isSatisfiedBy = spec.IsSatisfiedBy(itinerary);
            var why = spec.WhyIsNotSatisfiedBy(itinerary);

            //Assert
            isSatisfiedBy.Should().BeTrue();
            why.Should().HaveCount(0);
        }

        [Test]
        public void RouteOriginLocationAndItineraryDepartureLocationIsDifferent()
        {
            //Arrange
            var route = new Route(Locations.Dallas, Locations.Chicago, 1.January(2000), 4.January(2000));
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 2.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Chicago, 3.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };
            var itinerary = new Itinerary(transportLegs);

            var spec = new RouteSpecification(route);

            //Act
            var isSatisfiedBy = spec.IsSatisfiedBy(itinerary);
            var why = spec.WhyIsNotSatisfiedBy(itinerary);

            //Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }

        [Test]
        public void RouteDepartureTimeIsAfterItineraryDepartureTime()
        {
            //Arrange
            var route = new Route(Locations.NewYork, Locations.Chicago, 2.January(2000), 4.January(2000));
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 1.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Chicago, 3.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };
            var itinerary = new Itinerary(transportLegs);

            var spec = new RouteSpecification(route);

            //Act
            var isSatisfiedBy = spec.IsSatisfiedBy(itinerary);
            var why = spec.WhyIsNotSatisfiedBy(itinerary);

            //Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }

        [Test]
        public void RouteDestinationLocationAndItineraryArrivalLocationIsDifferent()
        {
            //Arrange
            var route = new Route(Locations.NewYork, Locations.Chicago, 1.January(2000), 4.January(2000));
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 2.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Gothenburg, 3.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };
            var itinerary = new Itinerary(transportLegs);

            var spec = new RouteSpecification(route);

            //Act
            var isSatisfiedBy = spec.IsSatisfiedBy(itinerary);
            var why = spec.WhyIsNotSatisfiedBy(itinerary);

            //Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }

        [Test]
        public void RouteArrivalDeadlineIsBeforeItineraryArrivalTime()
        {
            //Arrange
            var route = new Route(Locations.NewYork, Locations.Chicago, 1.January(2000), 4.January(2000));
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 2.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Chicago, 3.January(2000), 5.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };
            var itinerary = new Itinerary(transportLegs);

            var spec = new RouteSpecification(route);

            //Act
            var isSatisfiedBy = spec.IsSatisfiedBy(itinerary);
            var why = spec.WhyIsNotSatisfiedBy(itinerary);

            //Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }
    }
}
