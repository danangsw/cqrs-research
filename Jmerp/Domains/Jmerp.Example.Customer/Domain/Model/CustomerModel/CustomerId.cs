using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CustomerId : SingleValueObject<string>, IIdentity
    {
        private static readonly Regex ValidValues = new Regex("(CS)[0-9]{5}", RegexOptions.Compiled);

        public CustomerId(string value) : base(value)
        {
            if (!ValidValues.IsMatch(value)) throw new ArgumentException($"'{value}' is not a valid customer code.");
        }
    }
}
