using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.TestHelpers;
using FluentAssertions;
using Jmerp.Example.Customers.Application;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands;
using Jmerp.Example.Customers.Queries.InMemory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jmerp.Example.Customers.Tests.Mocks;
using EventFlow.Queries;
using Jmerp.Example.Customers.Queries.InMemory.Customers.QueryHandlers;
using Jmerp.Example.Customers.Queries.InMemory.Customers;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Queries;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Subscribers;
using EventFlow.Subscribers;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using Jmerp.Commons;

namespace Jmerp.Example.Customers.Tests.IntegrationTests
{
    public class CustomerDomainSubscriber : ISubscribeSynchronousTo<CustomerAggregate, CustomerId, CustomerDomainRespondedEvent>
    {
        public Task HandleAsync(IDomainEvent<CustomerAggregate, CustomerId, CustomerDomainRespondedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine(domainEvent.AggregateEvent.Result);
            return Task.FromResult(0);
        }
    }

    public class Scenarios : Test
    {
        private IRootResolver _resolver;
        private IAggregateStore _aggregateStore;
        private ICommandBus _commandBus;

        [SetUp]
        public void SetUp()
        {
            _resolver = EventFlowOptions.New
                .CustomerConfigurationDomain()
                .ConfigureCustomerQueriesInMemory()
                .AddSubscribers(typeof(CustomerDomainSubscriber))
                .CreateResolver();
            _aggregateStore = _resolver.Resolve<IAggregateStore>();
            _commandBus = _resolver.Resolve<ICommandBus>();
        }

        [TearDown]
        public void TearDown()
        {
            _resolver.DisposeSafe(new ConsoleLog(), "");
        }

        [Test]
        public async Task Simple() {
            //Arrange
            var customer = CustomerLists.Customer_CS00001;

            //Act
            await CreateCustomerAggregateBulkAsync();

            var queryProcessor = _resolver.Resolve<IQueryProcessor>();
            var customerReadModel = await queryProcessor.ProcessAsync(
                new GetCustomersQuery(new List<CustomerId> { customer.Id }), CancellationToken.None)
                .ConfigureAwait(false);

            //Assert
            customerReadModel.Select(rm => rm.Id == customer.Id).Should().HaveCount(1);
        }

        [Test]
        public async Task Simple_CustomerErorrAfterCreatedEvent()
        {
            //Arrange
            var customer = CustomerLists.Customer_CS00001;
            var eventStore = _resolver.Resolve<IAggregateStore>();

            //Act
            await CreateCustomerAggregateBulkAsync();
            await CreateCustomerAggregateAsync(customer);
            var testAggregate = eventStore.Load<CustomerAggregate, CustomerId>(customer.Id, CancellationToken.None);

            var queryProcessor = _resolver.Resolve<IQueryProcessor>();
            var customerReadModel = await queryProcessor.ProcessAsync(
                new GetCustomersQuery(new List<CustomerId> { customer.Id }), CancellationToken.None)
                .ConfigureAwait(false);

            //Assert
            customerReadModel.Select(rm => rm.Id == customer.Id).Should().HaveCount(1);
            testAggregate.SubscriberResult.Should().NotBeNull();
            testAggregate.SubscriberResult.IsError.Should().BeTrue();
            testAggregate.SubscriberResult.Errors.Should().HaveCount(1);
        }

        [Test]
        public async Task Simple_CustomerDomainClientSubscriber()
        {
            //Arrange
            var customer = CustomerLists.Customer_CS00001;

            //Act
            await CreateCustomerAggregateBulkAsync();
            await CreateCustomerAggregateAsync(customer);

            var queryProcessor = _resolver.Resolve<IQueryProcessor>();
            var customerReadModel = await queryProcessor.ProcessAsync(
                new GetCustomersQuery(new List<CustomerId> { customer.Id }), CancellationToken.None)
                .ConfigureAwait(false);

            //Assert
            customerReadModel.Select(rm => rm.Id == customer.Id).Should().HaveCount(1);
        }

        private Task CreateCustomerAggregateBulkAsync()
        {
            return Task.WhenAll(CustomerLists.GetCustomers().Select(CreateCustomerAggregateAsync));
        }

        private Task CreateCustomerAggregateAsync(Customer customer)
        {
            var booking = _resolver.Resolve<ICustomerApplicationService>();
            return booking.CreateCustomerAsync(customer.Id, customer.GeneralInfo, CancellationToken.None);
        }
    }
}
