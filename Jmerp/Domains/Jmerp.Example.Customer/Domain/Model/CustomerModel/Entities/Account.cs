using EventFlow.Entities;
using EventFlow.Extensions;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using System;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities
{
    public class Account : Entity<AccountId>
    {
        public Account(
            AccountId id,
            CustomerId customerId,
            string accountNumber,
            string accountType,
            string accountDescription,
            string firstName,
            string lastName,
            decimal accountBalance
            ) : base(id)
        {
            CustomerSpecs.IsNotNullOrEmptyIdentity.ThrowDomainErrorIfNotStatisfied(customerId);
            AccountingDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(accountNumber);
            AccountingDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(accountNumber);
            AccountingDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(accountDescription);
            AccountingDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(accountType);
            AccountingDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(firstName);
            AccountingDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(lastName);

            CustomerId = customerId;
            AccountNumber = accountNumber;
            AccountType = accountType;
            AccountDescription = accountDescription;
            FirstName = firstName;
            LastName = lastName;
            AccountBalance = accountBalance;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ToString();
        }

        public CustomerId CustomerId { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string AccountDescription { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public decimal AccountBalance { get; }
    }
}
