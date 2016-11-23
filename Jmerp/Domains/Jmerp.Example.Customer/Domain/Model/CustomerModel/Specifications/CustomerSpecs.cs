using EventFlow.Specifications;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications
{
    public static class CustomerSpecs
    {
        public static ISpecification<string> IsValidCode { get; } = new IsValidIdSpecification();
        private static readonly Regex ValidValues = new Regex("(CS)[0-9]{5}", RegexOptions.Compiled);

        private class IsValidIdSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (!ValidValues.IsMatch(obj)) {
                    yield return ($"'{obj}' is not a valid customer code.");
                }
            }
        }
    }
}
