namespace Jmerp.Example.Customers.Middlewares.Models
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public GeneralInfoDto GeneralInfo { get; set; }
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
}
