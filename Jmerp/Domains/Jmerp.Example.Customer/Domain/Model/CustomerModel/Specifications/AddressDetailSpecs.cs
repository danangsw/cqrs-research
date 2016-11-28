using EventFlow.Specifications;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications
{
    public static class AddressDetailSpecs
    {
        public static ISpecification<List<Address>> IsAnyList { get; } = new IsAnyListSpecification();

        private class IsAnyListSpecification : Specification<List<Address>>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(List<Address> obj)
            {
                if (!obj.Any())
                {
                    yield return ($"'{obj}' can not be Null or Empty list.");
                }
            }
        }
    }
}
