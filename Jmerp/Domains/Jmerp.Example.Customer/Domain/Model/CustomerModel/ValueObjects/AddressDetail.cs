using EventFlow.Extensions;
using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Helpers;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects
{
    public class AddressDetail : ValueObject
    {
        public AddressDetail(
            IEnumerable<Address> addresses)
        {
            var addressList = (addresses ?? Enumerable.Empty<Address>()).ToList();

            AddressDetailSpecs.IsAnyList.ThrowDomainErrorIfNotStatisfied(addressList);

            if (Addresses == null || Addresses.Any())
                Addresses = new List<Address>();

            Addresses.AddRange(addressList);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TestProperty;

            foreach (var item in Addresses.OrderBy(o => o.Id).ToList())
            {
                yield return item;
            }
        }
        
        public List<Address> Addresses { get; private set; }

        public int TestProperty { get; private set; }

        public AddressDetail SetAsDefaultShippingAddress(AddressId addressId)
        {

            return SetAsDefaultFor(addressId, CustomerAddressTypeConstants.ShippingAddress);
        }

        public AddressDetail SetAsDefaultMailingAddress(AddressId addressId)
        {
            return SetAsDefaultFor(addressId, CustomerAddressTypeConstants.MailingAddress);
        }

        public AddressDetail UpdateAddress(Address address)
        {
            var addressList = new List<Address>();

            var updatedAddress = Addresses
                .Where(a => a.Id == address.Id)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    a.AddressType,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    a.StateProvince,
                    a.PostalCode));
            addressList.AddRange(updatedAddress);

            var otherAddresses = Addresses
                .Where(a => a.Id != address.Id)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    a.AddressType,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    a.StateProvince,
                    a.PostalCode));
            addressList.AddRange(otherAddresses);

            return new AddressDetail(addressList);
        }

        private AddressDetail SetAsDefaultFor(AddressId addressId, string asAddressType)
        {
            var addressList = new List<Address>();

            var defaultAddresses = Addresses
                .Where(a => a.Id == addressId && a.AddressType == asAddressType)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    a.AddressType,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    a.StateProvince,
                    a.PostalCode,
                    true));
            addressList.AddRange(defaultAddresses);

            var otherAddresses = Addresses
                .Where(a => a.Id != addressId && a.AddressType == asAddressType)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    a.AddressType,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    a.StateProvince,
                    a.PostalCode,
                    false));
            addressList.AddRange(otherAddresses);

            return new AddressDetail(addressList);
        }
    }
}
