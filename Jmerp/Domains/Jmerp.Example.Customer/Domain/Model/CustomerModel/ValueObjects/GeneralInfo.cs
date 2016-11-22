using EventFlow.Extensions;
using EventFlow.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using System;
using System.Text.RegularExpressions;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects
{
    public class GeneralInfo : ValueObject
    {
        public GeneralInfo(
            string organizationName,
            string contactPerson,
            string phone,
            string fax,
            string email,
            string web)
        {
            GeneralInfoSpecs.IsNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(organizationName);
            GeneralInfoSpecs.IsNullOrEmptyInput.ThrowDomainErrorIfNotStatisfied(contactPerson);

            OrganizationName = organizationName;
            ContactPerson = contactPerson;
            Phone = phone;
            Fax = fax;
            Email = email;
            Web = web;
        }

        public string OrganizationName { get;}
        public string ContactPerson { get;}
        public string Phone { get;}
        public string Fax { get;}
        public string Email { get; }
        public string Web { get; }
    }
}
