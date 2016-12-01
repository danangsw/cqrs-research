using EventFlow.Specifications;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications
{
    public static class AddressDetailSpecs
    {
        public static ISpecification<List<Address>> IsAnyList { get; } = new IsAnyListSpecification();
        public static ISpecification<Address> IsValidInput { get; } = new IsValidInputSpecification();
        public static ISpecification<string> IsNotNullOrEmptyInput { get; } = new IsNotNullOrEmptyInputSpecification();
        public static ISpecification<CustomerId> IsNotNullOrEmptyIdentity { get; } = new IsNotNullOrEmptyIdentitySpecification();

        private class IsAnyListSpecification : Specification<List<Address>>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(List<Address> obj)
            {
                if (!obj.Any())
                {
                    yield return ($"'{obj}' can not be Null or Empty list.");
                }
            }
        }

        private class IsValidInputSpecification : Specification<Address>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(Address obj)
            {                
                if (obj.CustomerId == null) yield return $"'{nameof(obj.CustomerId)}' Cannot be empty or null.";
                if (string.IsNullOrEmpty(obj.AddressType)) yield return $"'{nameof(obj.AddressType)}' Cannot be empty or null.";
                if (string.IsNullOrEmpty(obj.AddressLine1)) yield return $"'{nameof(obj.AddressLine1)}' Cannot be empty or null.";
                if (string.IsNullOrEmpty(obj.AddressLine2)) yield return $"'{nameof(obj.AddressLine2)}' Cannot be empty or null.";
                if (string.IsNullOrEmpty(obj.City)) yield return $"'{nameof(obj.City)}' Cannot be empty or null.";
                if (string.IsNullOrEmpty(obj.StateProvince)) yield return $"'{nameof(obj.StateProvince)}' Cannot be empty or null.";
                if (string.IsNullOrEmpty(obj.PostalCode)) yield return $"'{nameof(obj.PostalCode)}' Cannot be empty or null.";
            }
        }

        private class IsNotNullOrEmptyInputSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (String.IsNullOrEmpty(obj))
                {
                    yield return $"'{nameof(obj)}' Cannot be empty or null.";
                }
            }
        }

        private class IsNotNullOrEmptyIdentitySpecification : Specification<CustomerId>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(CustomerId obj)
            {
                if (obj == null)
                {
                    yield return $"'{nameof(obj)}' Cannot be empty or null.";
                }
            }
        }
    }
}
