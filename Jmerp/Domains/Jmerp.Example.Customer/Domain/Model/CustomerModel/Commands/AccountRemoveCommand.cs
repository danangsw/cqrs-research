using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AccountRemoveCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AccountRemoveCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            List<AccountId> accountIds)
            : base(aggregateId)
        {
            AccountIds = accountIds;
        }

        public List<AccountId> AccountIds { get; }
    }

    public class AccountRemoveCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AccountRemoveCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AccountRemoveCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemoveAccounts(command.AccountIds);
            return Task.FromResult(0);
        }
    }
}
