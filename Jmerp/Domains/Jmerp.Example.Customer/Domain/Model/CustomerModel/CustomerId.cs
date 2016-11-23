using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using Newtonsoft.Json;
using System;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CustomerId : SingleValueObject<string>, IIdentity
    {
        public CustomerId(string value) : base(value)
        {
            CustomerSpecs.IsValidCode.ThrowDomainErrorIfNotStatisfied(value);
        }
    }
}
