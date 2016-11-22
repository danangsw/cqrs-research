using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.Core;
using EventFlow.EventStores;
using EventFlow.Logs;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace EventFlow.TestHelpers
{
    public abstract class Test
    {
        protected IFixture Fixture { get; private set; }
        protected IDomainEventFactory DomainEventFactory;
        protected ILog Log => LogHelper.Log;

        [SetUp]
        public void SetUpTest()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());

            Fixture.Customize<EventId>(c => c.FromFactory(() => EventId.New));
            Fixture.Customize<Label>(s => s.FromFactory(() => Label.Named($"label-{Guid.NewGuid():D}")));

            DomainEventFactory = new DomainEventFactory();
        }

        protected T A<T>()
        {
            return Fixture.Create<T>();
        }

        protected List<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count).ToList();
        }

        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        protected T Inject<T>(T instance)
            where T : class
        {
            Fixture.Inject(instance);
            return instance;
        }

        protected Mock<T> InjectMock<T>()
            where T : class
        {
            var mock = new Mock<T>();
            Fixture.Inject(mock.Object);
            return mock;
        }

        protected Mock<Func<T>> CreateFailingFunction<T>(T result, params Exception[] exceptions)
        {
            var function = new Mock<Func<T>>();
            var exceptionStack = new Stack<Exception>(exceptions.Reverse());
            function
                .Setup(f => f())
                .Returns(() =>
                {
                    if (exceptionStack.Any())
                    {
                        throw exceptionStack.Pop();
                    }
                    return result;
                });
            return function;
        }
    }
}
