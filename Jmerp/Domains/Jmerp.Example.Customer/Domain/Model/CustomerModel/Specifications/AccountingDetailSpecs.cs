using EventFlow.Specifications;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications
{
    public static class AccountingDetailSpecs
    {
        public static ISpecification<List<Account>> IsAnyList { get; } = new IsAnyListSpecification();
        public static ISpecification<Account> IsValidInput { get; } = new IsValidInputSpecification();
        public static ISpecification<string> IsNotNullOrEmptyInput { get; } = new IsNotNullOrEmptyInputSpecification();
        public static ISpecification<AccountId> IsNotNullOrEmptyIdentity { get; } = new IsNotNullOrEmptyIdentitySpecification();

        private class IsAnyListSpecification : Specification<List<Account>>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(List<Account> obj)
            {
                if (!obj.Any())
                {
                    yield return string.Format(CustomerDomainMessageResources.MSG00002, nameof(obj));
                }
            }
        }

        private class IsValidInputSpecification : Specification<Account>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(Account obj)
            {
                if (obj.CustomerId == null) yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj.CustomerId));
                if (string.IsNullOrEmpty(obj.AccountNumber)) yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj.AccountNumber));
                if (string.IsNullOrEmpty(obj.AccountDescription)) yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj.AccountDescription));
                if (string.IsNullOrEmpty(obj.AccountType)) yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj.AccountType));
                if (string.IsNullOrEmpty(obj.FirstName)) yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj.FirstName));
                if (string.IsNullOrEmpty(obj.LastName)) yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj.LastName));
            }
        }

        private class IsNotNullOrEmptyInputSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (String.IsNullOrEmpty(obj))
                {
                    yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj));
                }
            }
        }

        private class IsNotNullOrEmptyIdentitySpecification : Specification<AccountId>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(AccountId obj)
            {
                if (obj == null)
                {
                    yield return string.Format(CustomerDomainMessageResources.MSG00001, nameof(obj));
                }
            }
        }
    }
}
