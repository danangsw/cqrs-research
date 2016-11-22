using EventFlow.Commands;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Commands
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
            aggregate.Book(command.Route);
            return Task.FromResult(0);
        }
    }
}
