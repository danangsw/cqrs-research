using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class GeneralInfoUpdateCommand : Command<CustomerAggregate, CustomerId>
    {
        public GeneralInfoUpdateCommand(
            CustomerId id
            ) : base(id)
        {

        }
    }

    public class GeneralInfoUpdateCommandHandler :
    CommandHandler<CustomerAggregate, CustomerId, CustomerCreateCommand>
    {
        public override Task ExecuteAsync(
            CustomerAggregate aggregate,
            CustomerCreateCommand command,
            CancellationToken cancellationToken)
        {
            //aggregate.Create(command.GeneralInfo);
            return Task.FromResult(0);
        }
    }
}
