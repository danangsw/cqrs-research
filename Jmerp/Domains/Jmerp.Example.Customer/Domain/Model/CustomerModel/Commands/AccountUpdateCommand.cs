using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AccountUpdateCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AccountUpdateCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            Account account)
            : base(aggregateId)
        {
            Account = account;
        }

        public Account Account { get; }
    }

    public class AccountUpdateCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AccountUpdateCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AccountUpdateCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateAccount(command.Account);
            return Task.FromResult(0);
        }
    }
}
