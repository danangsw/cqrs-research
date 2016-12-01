using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects
{
    public class AccountingDetail : ValueObject
    {
        public List<Account> Accounts { get; private set; }

        public AccountingDetail(
            IEnumerable<Account> accounts)
        {
            var accountList = (accounts ?? Enumerable.Empty<Account>()).ToList();

            if (!accountList.Any()) throw new ArgumentException(nameof(accounts));

            Accounts = accountList;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Accounts;
        }

        public AccountingDetail AddAccount(IEnumerable<Account> accounts)
        {
            var accountList = Accounts ?? new List<Account>();
            accountList.AddRange(accounts);
            return new AccountingDetail(accountList);
        }

        public AccountingDetail RemoveAccount(IEnumerable<AccountId> accountIds)
        {
            var accountList = Accounts ?? new List<Account>();
            accountList.RemoveAll(a => accountIds.Contains(a.Id));

            return new AccountingDetail(accountList);
        }

        public AccountingDetail UpdateAccount(Account account)
        {
            var accountList = new List<Account>();

            accountList.AddRange(Accounts
                .Where(a => a.Id == account.Id)
                .Select(a => new Account(
                    a.Id,
                    a.CustomerId,
                    account.AccountNumber,
                    account.AccountType,
                    account.AccountDescription,
                    account.FirstName,
                    account.LastName,
                    account.AccountBalance)));

            accountList.AddRange(Accounts
                .Where(a => a.Id != account.Id)
                .Select(a => new Account(
                    a.Id,
                    a.CustomerId,
                    a.AccountNumber,
                    a.AccountType,
                    a.AccountDescription,
                    a.FirstName,
                    a.LastName,
                    a.AccountBalance)));

            return new AccountingDetail(accountList);
        }
    }
}
