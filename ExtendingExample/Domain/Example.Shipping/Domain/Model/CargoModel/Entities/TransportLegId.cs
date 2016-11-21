using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class TransportLegId : Identity<TransportLegId>
    {
        public TransportLegId(string value) : base(value)
        {
        }
    }
}
