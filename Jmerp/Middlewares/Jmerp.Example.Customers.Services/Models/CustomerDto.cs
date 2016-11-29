using System.Collections.Generic;

namespace Jmerp.Example.Customers.Middlewares.Models
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public GeneralInfoDto GeneralInfo { get; set; }
        public AddressDetailDto AddressDetail { get; set; }
    }

    public class GeneralInfoDto
    {
        public string OrganizationName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
    }

    public class AddressDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public bool SetDefault { get; set; }
    }

    public class AddressDetailDto
    {
        public List<AddressDto> Addresses { get; set; }
    }
}
