using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.LocationModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class LocationId : SingleValueObject<string>, IIdentity
    {
        private static readonly Regex ValidValues = new Regex("[a-zA-Z]{2}[a-zA-Z2-9]{3}", RegexOptions.Compiled);

        public LocationId(string value) : base(value)
        {
            if (!ValidValues.IsMatch(value)) throw new ArgumentException($"'{value} is not a valid UN location code'");
        }
    }
}
