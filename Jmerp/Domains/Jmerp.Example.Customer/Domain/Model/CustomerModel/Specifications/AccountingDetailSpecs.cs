using EventFlow.Specifications;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications
{
    public static class AccountingDetailSpecs
    {
        public static ISpecification<List<Account>> IsAnyList { get; } = new IsAnyListSpecification();
        public static ISpecification<Account> IsValidInput { get; } = new IsValidInputSpecification();
        public static ISpecification<string> IsNotNullOrEmptyInput { get; } = new IsNotNullOrEmptyInputSpecification();
        public static ISpecification<AccountId> IsNotNullOrEmptyIdentity { get; } = new IsNotNullOrEmptyIdentitySpecification();
        public static ISpecification<string> IsValidCode { get; } = new IsValidIdSpecification();

        /// <summary>
        /// VALID NUMBERS:
        /// 12345678912 (11 digits)
        /// 12-345-678912 (11 digits separated by hyphens)
        /// ----
        /// INVALID NUMBERS:
        /// 1223 (less than 11 digits)
        /// 111111111111 ( more than 11 digits)
        /// 123-23-678912 (11 digits , but not separated correctly, it should be 2 digits-3 digits-6 digits)
        /// </summary>
        private static readonly Regex ValidValues = new Regex(@"^[0-9]{2}(?:[0-9]{9}|-[0-9]{3}-[0-9]{6})$", RegexOptions.Compiled);

        private class IsValidIdSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (!ValidValues.IsMatch(obj))
                {
                    yield return (string.Format(CustomerDomainMessageResources.MSG00003, obj));
                }
            }
        }

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
