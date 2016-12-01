using EventFlow.Entities;
using EventFlow.Extensions;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using System;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities
{
    public class Address : Entity<AddressId>
    {
        public Address(
            AddressId id,
            CustomerId customerId,
            string addressType,
            string addressLine1,
            string addressLine2,
            string city,
            string stateProvince,
            string postalCode,
            bool setDefault
            ) : base(id)
        {
            CustomerSpecs.IsNotNullOrEmptyIdentity.ThrowDomainErrorIfNotStatisfied(customerId);
            AddressDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(addressType);
            AddressDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(addressLine1);
            AddressDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(addressLine2);
            AddressDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(city);
            AddressDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(stateProvince);
            AddressDetailSpecs.IsNotNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(postalCode);

            CustomerId = customerId;
            AddressType = addressType;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            StateProvince = stateProvince;
            PostalCode = postalCode;
            SetDefault = setDefault;
        }

        public CustomerId CustomerId { get; }
        public string AddressType { get; }
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string City { get; }
        public string StateProvince { get; }
        public string PostalCode { get; }
        public bool SetDefault { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ToString();
        }
    }
}
