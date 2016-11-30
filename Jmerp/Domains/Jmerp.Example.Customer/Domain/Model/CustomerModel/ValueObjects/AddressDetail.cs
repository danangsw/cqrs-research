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
        public List<Address> Addresses { get; private set; }

        public int TestProperty { get; private set; }

        public AddressDetail(
            IEnumerable<Address> addresses)
        {
            var addressList = (addresses ?? Enumerable.Empty<Address>()).ToList();

            AddressDetailSpecs.IsAnyList.ThrowDomainErrorIfNotStatisfied(addressList);

            Addresses = addressList;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TestProperty;
            yield return Addresses;
        }

        public AddressDetail AddAddress(IEnumerable<Address> addresses)
        {
            var addressList = Addresses ?? new List<Address>();
            addressList.AddRange(addresses);
            return new AddressDetail(addressList);
        }

        public AddressDetail RemoveAddress(AddressId addressId)
        {
            var addressList = Addresses ?? new List<Address>();
            var itemRemove = Addresses.SingleOrDefault(i => i.Id == addressId);
            addressList.Remove(itemRemove);

            return new AddressDetail(addressList);
        }

        public AddressDetail SetAsDefaultShippingAddress(AddressId addressId)
        {
            return SetAsDefaultFor(addressId, CustomerAddressTypeConstants.ShippingAddress);
        }

        public AddressDetail SetAsDefaultBillingAddress(AddressId addressId)
        {
            return SetAsDefaultFor(addressId, CustomerAddressTypeConstants.BillingAddress);
        }

        public AddressDetail UpdateAddress(Address address)
        {
            var addressList = new List<Address>();

            addressList.AddRange(Addresses
                .Where(a => a.Id == address.Id)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    address.AddressType,
                    address.AddressLine1,
                    address.AddressLine2,
                    address.City,
                    address.StateProvince,
                    address.PostalCode,
                    address.SetDefault)));

            addressList.AddRange(Addresses
                .Where(a => a.Id != address.Id)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    a.AddressType,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    a.StateProvince,
                    a.PostalCode,
                    a.SetDefault)));

            return new AddressDetail(addressList);
        }

        private AddressDetail SetAsDefaultFor(AddressId addressId, string asAddressType)
        {
            var addressList = new List<Address>();

            addressList.AddRange(Addresses
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
                    true)));

            addressList.AddRange(Addresses
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
                    false)));

            addressList.AddRange(Addresses
                .Where(a => a.Id != addressId && a.AddressType != asAddressType)
                .Select(a => new Address(
                    a.Id,
                    a.CustomerId,
                    a.AddressType,
                    a.AddressLine1,
                    a.AddressLine2,
                    a.City,
                    a.StateProvince,
                    a.PostalCode,
                    a.SetDefault)));

            return new AddressDetail(addressList);
        }
    }
}
