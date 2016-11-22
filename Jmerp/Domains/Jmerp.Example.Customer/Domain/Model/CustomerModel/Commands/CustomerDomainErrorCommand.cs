using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Commons;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class CustomerDomainErrorCommand : Command<CustomerAggregate, CustomerId>
    {
        public SubscriberResult SubscriberResult { get; private set; }

        public CustomerDomainErrorCommand(
          CustomerId id,
          SubscriberResult subscriberResult)
          : base(id)
        {
            SubscriberResult = subscriberResult;
        }
    }

    public class CustomerDomainErrorCommandHandler :
    CommandHandler<CustomerAggregate, CustomerId, CustomerDomainErrorCommand>
    {
        public override Task ExecuteAsync(
            CustomerAggregate aggregate,
            CustomerDomainErrorCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.CustomerDomainError(command.SubscriberResult);
            return Task.FromResult(0);
        }
    }
}
