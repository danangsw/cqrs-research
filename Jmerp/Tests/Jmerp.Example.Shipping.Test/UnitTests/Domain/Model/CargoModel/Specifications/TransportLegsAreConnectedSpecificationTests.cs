using EventFlow.TestHelpers;
using FluentAssertions;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Specifications;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using Jmerp.Example.Shipping.Tests.Mocks;
using NUnit.Framework;

namespace Jmerp.Example.Shipping.Tests.UnitTests.Domain.Model.CargoModel.Specifications
{
    [Category(Categories.Unit)]
    public class TransportLegsAreConnectedSpecificationTests : Test
    {
        [Test]
        public void Valid()
        {
            // Arrange
            var sut = new TransportLegsAreConnectedSpecification();
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 2.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Chicago, 3.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };

            // Act
            var isSatisfiedBy = sut.IsSatisfiedBy(transportLegs);
            var why = sut.WhyIsNotSatisfiedBy(transportLegs);

            // Assert
            isSatisfiedBy.Should().BeTrue();
            why.Should().HaveCount(0);
        }

        [Test]
        public void UnloadIsAfterLoad()
        {
            // Arrange
            var sut = new TransportLegsAreConnectedSpecification();
            var transportLegs = new[]
                {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 3.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Dallas, Locations.Chicago, 2.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };

            // Act
            var isSatisfiedBy = sut.IsSatisfiedBy(transportLegs);
            var why = sut.WhyIsNotSatisfiedBy(transportLegs);

            // Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }

        [Test]
        public void UnloadAndLoadLocationAreDifferent()
        {
            //Arrange
            var spec = new TransportLegsAreConnectedSpecification();
            var transportLegs = new[]
              {
                    new TransportLeg(TransportLegId.New, Locations.NewYork, Locations.Dallas, 1.January(2000), 2.January(2000), A<VoyageId>(), CarrierMovementId.New),
                    new TransportLeg(TransportLegId.New, Locations.Helsinki, Locations.Chicago, 3.January(2000), 4.January(2000), A<VoyageId>(), CarrierMovementId.New),
                };

            //Act
            var isSatisfiedBy = spec.IsSatisfiedBy(transportLegs);
            var why = spec.WhyIsNotSatisfiedBy(transportLegs);

            //Assert
            isSatisfiedBy.Should().BeFalse();
            why.Should().HaveCount(1);
        }
    }
}
