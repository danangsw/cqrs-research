using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using Newtonsoft.Json;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class AccountId : SingleValueObject<string>, IIdentity
    {
        public AccountId(
            string value
            ) : base(value)
        {
            AccountingDetailSpecs.IsValidCode.ThrowDomainErrorIfNotStatisfied(value);
        }
    }
}
