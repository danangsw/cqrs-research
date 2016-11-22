using AutoMapper;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Middlewares.Models;

namespace Jmerp.Example.Customers.Middlewares.Mappings.Profiles
{
    public class CustomerDtoToDomainModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "CustomerDtoToDomainModelMapping";
            }
        }

        public CustomerDtoToDomainModelMappingProfile()
        {
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
            CreateMap<CustomerDto, Customer>();
        }
    }
}
