

using EventFlow.Core;
using EventFlow.ValueObjects;
using System;
using System.Text.RegularExpressions;

namespace Jmerp.Example.Shipping.Domain.Model.LocationModel
{
    public class LocationId : SingleValueObject<string>, IIdentity
    {
        private static readonly Regex ValidValues = new Regex("[a-zA-Z]{2}[a-zA-Z2-9]{3}", RegexOptions.Compiled);

        public LocationId(string value) : base(value) {
            if (!ValidValues.IsMatch(value)) throw new ArgumentException($"'{value}' is not a valid UN location code");
        }
    }
}
