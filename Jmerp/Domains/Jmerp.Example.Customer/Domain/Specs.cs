﻿
using EventFlow.Aggregates;
using EventFlow.Provided.Specifications;
using EventFlow.Specifications;
using System.Collections.Generic;

namespace Jmerp.Example.Shipping.Domain
{
    public static class Specs
    {
        public static ISpecification<IAggregateRoot> AggregateIsNew { get; } = new AggregateIsNewSpecification();
        public static ISpecification<IAggregateRoot> AggregateIsCreated { get; } = new AggregateIsCreatedSpecification();

        private class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
            {
                if (obj.IsNew)
                {
                    yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is New.";
                }
            }
        }
    }
}
