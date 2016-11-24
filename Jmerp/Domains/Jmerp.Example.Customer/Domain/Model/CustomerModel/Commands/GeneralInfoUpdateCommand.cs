using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class GeneralInfoUpdateCommand : Command<CustomerAggregate, CustomerId>
    {
        public string OrganizationName { get; }
        public string ContactPerson { get; }
        public string Phone { get; }
        public string Fax { get; }
        public string Email { get; }
        public string Web { get; }

        public GeneralInfoUpdateCommand(
            CustomerId id,
            ISourceId sourceId,
            string organizationName,
            string contactPerson,
            string phone,
            string fax,
            string email,
            string web
            ) : base(id, sourceId)
        {
            OrganizationName = organizationName;
            ContactPerson = contactPerson;
            Phone = phone;
            Fax = fax;
            Email = email;
            Web = web;
        }
    }

    public class GeneralInfoUpdateCommandHandler :
    CommandHandler<CustomerAggregate, CustomerId, GeneralInfoUpdateCommand>
    {
        public override Task ExecuteAsync(
            CustomerAggregate aggregate,
            GeneralInfoUpdateCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.Update(command.OrganizationName, command.ContactPerson, 
                command.Phone, command.Fax, command.Email, command.Web);
            return Task.FromResult(0);
        }
    }
}
