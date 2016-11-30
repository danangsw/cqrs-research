using EventFlow.ReadStores;
using System.Collections.Generic;
using EventFlow.Aggregates;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;

namespace Jmerp.Example.Customers.Queries.InMemory.Customers.ReadModelLocators
{
    public class CustomerAddressReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            var addressAdded =  domainEvent 
                as IDomainEvent<CustomerAggregate, CustomerId, AddressAddedEvent>;

            if (addressAdded == null)
            {
                yield break;
            }

            foreach (var item in addressAdded.AggregateEvent.AddressDetail.Addresses)
            {
                yield return item.Id.Value;
            }
        }
    }
}
