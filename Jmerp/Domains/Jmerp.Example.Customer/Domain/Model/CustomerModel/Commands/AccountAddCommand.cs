using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AccountAddCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AccountAddCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            List<Account> accounts)
            : base(aggregateId)
        {
            Accounts = accounts;
        }

        public List<Account> Accounts { get; }
    }

    public class AccountAddCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AccountAddCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AccountAddCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddAccounts(command.Accounts);
            return Task.FromResult(0);
        }
    }
}
