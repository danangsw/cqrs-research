

using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CargoId : Identity<CargoId>
    {
        public CargoId(string value) : base(value)
        {
        }
    }
}
