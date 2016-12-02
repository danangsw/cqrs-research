using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Helpers;
using Jmerp.Example.Customers.Middlewares.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jmerp.Example.Customers.Middlewares.Tests.Mocks
{
    public class CustomerDtoList
    {
        public static readonly CustomerDto Customer_CS00001 = new CustomerDto() { Id = "CS00001", GeneralInfo = CustomerGeneralInfos.GeneralInfo_CS00001 };
        public static readonly CustomerDto Customer_CS00002 = new CustomerDto() { Id = "CS00002", GeneralInfo = CustomerGeneralInfos.GeneralInfo_CS00002 };
        public static readonly CustomerDto Customer_CS00003 = new CustomerDto() { Id = "CS00003", GeneralInfo = CustomerGeneralInfos.GeneralInfo_CS00003 };

        public static IEnumerable<CustomerDto> GetCustomers()
        {
            var fieldInfos = typeof(CustomerDtoList).GetFields(BindingFlags.Public | BindingFlags.Static);
            return fieldInfos.Select(fi => (CustomerDto)fi.GetValue(null));
        }
    }

    public static class CustomerGeneralInfos
    {
        public static readonly GeneralInfoDto GeneralInfo_CS00001 = new GeneralInfoDto() { 
                                                                OrganizationName = "PT. ABC",
                                                                ContactPerson = "Mr. John",
                                                                Phone = "0123456789",
                                                                Fax = "",
                                                                Email = "john@abc.com",
                                                                Web = "http://www.abc.co.id"
                                                                };
        public static readonly GeneralInfoDto GeneralInfo_CS00002 = new GeneralInfoDto() {
                                                                OrganizationName = "PT. DEF",
                                                                ContactPerson = "Mr. Smith",
                                                                Phone = "(012)8888888",
                                                                Fax = "012-888-8888",
                                                                Email = "smith@mail.def.co.id",
                                                                Web = "http://www.def.co.id"
                                                                 };
        public static readonly GeneralInfoDto GeneralInfo_CS00003 = new GeneralInfoDto() { 
                                                                OrganizationName = "PT. BLABLAH INDONESIA",
                                                                ContactPerson = "Mrs. Ginny",
                                                                Phone = "012-345-6789",
                                                                Fax = "012 345 6789",
                                                                Email = "john@infoblabla.wordpress.org",
                                                                Web = "http://infoblabla.wordpress.org"
                                                                };
    }

    public static class CustomerAddressDetails
    {
        public static readonly AddressDto AddressDto1_CS00001 = new AddressDto() {
            //Id = null,
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AddressType = CustomerAddressTypeConstants.ShippingAddress,
            AddressLine1 = "Jl. Mawar",
            AddressLine2 = "Cilandak",
            City = "Jakarta Selatan",
            StateProvince = "DKI Jakarta",
            PostalCode = "120345",
            SetDefault = false
        };
        public static readonly AddressDto AddressDto2_CS00001 = new AddressDto()
        {
            //Id = null,
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AddressType = CustomerAddressTypeConstants.BillingAddress,
            AddressLine1 = "Jl. Mawar",
            AddressLine2 = "Cilandak",
            City = "Jakarta Selatan",
            StateProvince = "DKI Jakarta",
            PostalCode = "120345",
            SetDefault = true
        };
        public static readonly AddressDto AddressDto3_CS00001 = new AddressDto()
        {
            //Id = null,
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AddressType = CustomerAddressTypeConstants.ShippingAddress,
            AddressLine1 = "Jl. Melati",
            AddressLine2 = "Cinere",
            City = "Depok",
            StateProvince = "Jawa Barat",
            PostalCode = "345678",
            SetDefault = true
        };
        public static readonly AddressDto AddressDto4_CS00001 = new AddressDto()
        {
            //Id = null,
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AddressType = CustomerAddressTypeConstants.BillingAddress,
            AddressLine1 = "Jl. Tokek",
            AddressLine2 = "Cibubur",
            City = "Jakarta Timur",
            StateProvince = "DKI Jakarta",
            PostalCode = "140014",
            SetDefault = false
        };
    }

    public static class CustomerAccountingDetails
    {
        public static readonly AccountDto AccountDto1_CS00001 = new AccountDto()
        {
            Id = "12345678910",
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AccountNumber = "111",
            AccountType = CustomerAccountTypeConstants.PersonalAccounts,
            AccountDescription = "Bank Mandiri a/c",
            FirstName = "Wak",
            LastName = "Doyok",
            AccountBalance = 100000000.0M
        };
        public static readonly AccountDto AccountDto2_CS00001 = new AccountDto()
        {
            Id = "12345678911",
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AccountNumber = "222",
            AccountType = CustomerAccountTypeConstants.RealAccounts,
            AccountDescription = "Cash a/c",
            FirstName = "Wak",
            LastName = "Doyok",
            AccountBalance = 200000000.0M
        };
        public static readonly AccountDto AccountDto3_CS00001 = new AccountDto()
        {
            Id = "12345678912",
            CustomerId = CustomerDtoList.Customer_CS00001.Id,
            AccountNumber = "222",
            AccountType = CustomerAccountTypeConstants.NominalAccounts,
            AccountDescription = "Profit on sale of Asset a/c",
            FirstName = "Wak",
            LastName = "Doyok",
            AccountBalance = 300000000.0M
        };
    }
}
