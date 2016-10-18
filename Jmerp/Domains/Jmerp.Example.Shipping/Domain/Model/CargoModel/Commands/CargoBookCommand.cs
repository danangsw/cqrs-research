

using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Commands
{
    public class CargoBookCommand : Command<CargoAggregate, CargoId>
    {
        public CargoBookCommand(
            CargoId aggregateId,
            Route route)
            : base(aggregateId)
        {
            Route = route;
        }

        public Route Route { get; }
    }

    public class CargoBookCommandHandler : CommandHandler<CargoAggregate, CargoId, CargoBookCommand>
    {
        public override Task ExecuteAsync(CargoAggregate aggregate, CargoBookCommand command, CancellationToken cancellationToken)
        {
            aggregate.BookRoute(command.Route);

            return Task.FromResult(0);
        }
    }
}
