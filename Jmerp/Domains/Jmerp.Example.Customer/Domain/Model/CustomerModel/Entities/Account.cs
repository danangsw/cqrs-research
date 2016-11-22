using EventFlow.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using System;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities
{
    public class Account : Entity<AccountId>
    {
        public Account(
            AccountId id,
            CustomerId customer,
            string accountNumber,
            string accountType,
            string accountDescription,
            string firstName,
            string lastName
            ) : base(id)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (string.IsNullOrEmpty(accountNumber)) throw new ArgumentNullException(nameof(accountNumber));
            if (string.IsNullOrEmpty(accountDescription)) throw new ArgumentNullException(nameof(accountDescription));
            if (string.IsNullOrEmpty(accountType)) throw new ArgumentNullException(nameof(accountType));
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException(nameof(lastName));

            Customer = customer;
            AccountNumber = accountNumber;
            AccountType = accountType;
            AccountDescription = accountDescription;
            FirstName = firstName;
            LastName = lastName;
        }

        public CustomerId Customer { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string AccountDescription { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
