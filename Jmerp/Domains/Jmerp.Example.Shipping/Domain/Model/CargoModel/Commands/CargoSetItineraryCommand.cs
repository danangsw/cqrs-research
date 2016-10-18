

using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Commands
{
    public class CargoSetItineraryCommand : Command<CargoAggregate, CargoId>
    {
        public CargoSetItineraryCommand(
            CargoId aggregateId,
            Itinerary itinerary)
            : base(aggregateId)
        {
            Itinerary = itinerary;
        }

        public Itinerary Itinerary { get; }
    }

    public class CargoSetItineraryCommandHandler : CommandHandler<CargoAggregate, CargoId, CargoSetItineraryCommand>
    {
        public override Task ExecuteAsync(CargoAggregate aggregate, CargoSetItineraryCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetItinerary(command.Itinerary);

            return Task.FromResult(0);
        }
    }
}
