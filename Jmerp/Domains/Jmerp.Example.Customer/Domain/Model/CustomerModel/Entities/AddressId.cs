using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities
{

    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class AddressId : Identity<AddressId>
    {
        public AddressId(
            string value
            ) : base(value)
        {
        }
    }
}
