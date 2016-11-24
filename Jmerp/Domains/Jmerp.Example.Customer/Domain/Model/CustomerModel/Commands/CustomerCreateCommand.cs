using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class CustomerCreateCommand : Command<CustomerAggregate, CustomerId>
    {
        public GeneralInfo GeneralInfo { get; private set; }

        public CustomerCreateCommand(
          CustomerId id,
          GeneralInfo generalInfo)
          : base(id)
        {
            GeneralInfo = generalInfo;
        }
    }

    public class CustomerCreateCommandHandler :
    CommandHandler<CustomerAggregate, CustomerId, CustomerCreateCommand>
    {
        public override Task ExecuteAsync(
            CustomerAggregate aggregate, 
            CustomerCreateCommand command, 
            CancellationToken cancellationToken)
        {
            aggregate.Create(command.GeneralInfo);
            return Task.FromResult(0);
        }
    }
}
