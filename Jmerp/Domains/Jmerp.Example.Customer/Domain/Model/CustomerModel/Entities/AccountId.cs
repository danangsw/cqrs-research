using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class AccountId : Identity<AccountId>
    {
        public AccountId(
            string value
            ) : base(value)
        {
        }
    }
}
