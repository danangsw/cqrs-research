using AutoMapper;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Customers.Middlewares.Models;

namespace Jmerp.Example.Customers.Middlewares.Mappings.Profiles
{
    public class CustomerDomainModelToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "CustomerDomainModelToDtoMapping";
            }
        }

        public CustomerDomainModelToDtoMappingProfile()
        {
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
            CreateMap<AddressId, string>().ConvertUsing(s => s.Value);
            CreateMap<CustomerId, string>().ConvertUsing(s => s.Value);
            CreateMap<Customer, CustomerDto>();
            CreateMap<GeneralInfo, GeneralInfoDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDetail, AddressDetailDto>();
        }
    }
}
