using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects
{
    public class AccountDetail : ValueObject
    {
        public AccountDetail(
            IEnumerable<Account> accounts)
        {
            var accountList = (accounts ?? Enumerable.Empty<Account>()).ToList();

            if (!accountList.Any()) throw new ArgumentException(nameof(accounts));

            Accounts = accountList;
        }

        public IReadOnlyList<Account> Accounts { get; private set; }
    }
}
