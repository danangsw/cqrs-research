using System;
using System.Collections.Generic;
using EventFlow.Specifications;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using System.Text.RegularExpressions;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications
{
    public static class GeneralInfoSpecs
    {
        public static ISpecification<GeneralInfo> IsValidInput { get; } = new IsValidInputSpecification();
        public static ISpecification<string> IsNotNullOrEmptyInput { get; } = new IsNotNullOrEmptyInputSpecification();
        public static ISpecification<string> IsValidEmailInput { get; } = new IsValidEmailInputSpecification();
        public static ISpecification<string> IsValidUrlInput { get; } = new IsValidUrlInputSpecification();
        public static ISpecification<string> IsValidPhoneFaxlInput { get; } = new IsValidPhoneFaxlInputSpecification();

        private static readonly string emailRegex = @"(^$)|(^[a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6}$)";
        private static readonly string phoneFaxRegex = @"^$|^\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})$";
        private static readonly string urlRegex = @"(^$)|((https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\\w \.-]*)*\/?)";

        private class IsValidInputSpecification : Specification<GeneralInfo>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(GeneralInfo obj)
            {
                if (String.IsNullOrEmpty(obj.OrganizationName))
                {
                    yield return $"'{nameof(obj.OrganizationName)}' Cannot be empty or null.";
                }
                if (String.IsNullOrEmpty(obj.Phone))
                {
                    yield return $"'{nameof(obj.Phone)}' Cannot be empty or null.";
                }
                if (!(new Regex(emailRegex, RegexOptions.Compiled).IsMatch(obj.Email)))
                {
                    yield return ($"'{obj.Email}' is not a valid email.");
                }
                if (!(new Regex(urlRegex, RegexOptions.Compiled).IsMatch(obj.Web)))
                {
                    yield return ($"'{obj.Web}' is not a valid URL.");
                }
                if (!(new Regex(phoneFaxRegex, RegexOptions.Compiled).IsMatch(obj.Phone)))
                {
                    yield return ($"'{obj.Phone}' is not a valid phone/fax.");
                }
                if (!(new Regex(phoneFaxRegex, RegexOptions.Compiled).IsMatch(obj.Fax)))
                {
                    yield return ($"'{obj.Fax}' is not a valid phone/fax.");
                }
            }
        }

        private class IsValidEmailInputSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (!(new Regex(emailRegex, RegexOptions.Compiled).IsMatch(obj)))
                {
                    yield return ($"'{obj}' is not a valid email.");
                }
            }
        }

        private class IsNotNullOrEmptyInputSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (String.IsNullOrEmpty(obj))
                {
                    yield return $"'{nameof(obj)}' Cannot be empty or null.";
                }
            }
        }

        private class IsValidUrlInputSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (!(new Regex(urlRegex, RegexOptions.Compiled).IsMatch(obj)))
                {
                    yield return ($"'{obj}' is not a valid URL.");
                }
            }
        }

        private class IsValidPhoneFaxlInputSpecification : Specification<string>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(string obj)
            {
                if (!(new Regex(phoneFaxRegex, RegexOptions.Compiled).IsMatch(obj)))
                {
                    yield return ($"'{obj}' is not a valid phone/fax.");
                }
            }
        }
    }
}
