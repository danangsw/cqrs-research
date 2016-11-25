using EventFlow.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using System;

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
            bool setDefault = false
            ) : base(id)
        {
            if (customerId == null) throw new ArgumentNullException(nameof(customerId));
            if (string.IsNullOrEmpty(addressType)) throw new ArgumentNullException(nameof(addressType));
            if (string.IsNullOrEmpty(addressLine1)) throw new ArgumentNullException(nameof(addressLine1));
            if (string.IsNullOrEmpty(addressLine2)) throw new ArgumentNullException(nameof(addressLine2));
            if (string.IsNullOrEmpty(city)) throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrEmpty(stateProvince)) throw new ArgumentNullException(nameof(stateProvince));
            if (string.IsNullOrEmpty(postalCode)) throw new ArgumentNullException(nameof(postalCode));

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
    }
}
