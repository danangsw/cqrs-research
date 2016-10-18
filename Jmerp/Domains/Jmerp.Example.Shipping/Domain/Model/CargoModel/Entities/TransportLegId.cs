

using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class TransportLegId : Identity<TransportLegId>
    {
        public TransportLegId(
            string id
            ) : base(id)
        {
        }
    }
}
