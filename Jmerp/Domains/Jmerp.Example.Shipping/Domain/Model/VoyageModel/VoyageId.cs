using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class VoyageId : SingleValueObject<string>, IIdentity
    {
        public VoyageId(string value) : base(value)
        {
        }
    }
}
