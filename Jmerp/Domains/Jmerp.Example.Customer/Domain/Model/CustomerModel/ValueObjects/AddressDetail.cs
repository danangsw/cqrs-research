using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
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

            if (!addressList.Any()) throw new ArgumentException(nameof(addresses));

            Addresses = addressList;
        }

        public IReadOnlyList<Address> Addresses { get; private set; }
    }
}
