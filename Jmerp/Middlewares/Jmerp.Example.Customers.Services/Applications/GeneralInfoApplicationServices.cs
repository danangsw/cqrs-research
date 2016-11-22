using System;
using System.Threading;
using System.Threading.Tasks;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Commons;
using EventFlow.Configuration;
using EventFlow.Aggregates;
using EventFlow;
using Jmerp.Example.Customers.Queries.InMemory;
using Jmerp.Example.Customers.Middlewares.Models;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands;

namespace Jmerp.Example.Customers.Middlewares.Applications
{
    public class GeneralInfoApplicationServices : IGeneralInfoApplicationServices
    {
        private IRootResolver _resolver;
        private IAggregateStore _aggregateStore;
        private ICommandBus _commandBus;

        public GeneralInfoApplicationServices()
        {
            _resolver = EventFlowOptions.New
                .ConfigureCustomerServices()
                .ConfigureCustomerQueriesInMemory()
                .CreateResolver();
            _aggregateStore = _resolver.Resolve<IAggregateStore>();
            _commandBus = _resolver.Resolve<ICommandBus>();
        }

        public Task<ResponseResult> CreateGeneralInfo(CustomerDto customer, CancellationToken cancellationToken)
        {
            var customerModel = AutoMapper.Mapper.Map<Customer>(customer);
            //_commandBus.PublishAsync(new CustomerCreateCommand(customerId, generalInfo), cancellationToken);
            throw new NotImplementedException();
        }

        public Task<ResponseResult> UpdateGeneralInfo(string customerId, GeneralInfoDto generalInfo, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult> GetGeneralInfo(string customerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
