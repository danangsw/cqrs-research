using AutoMapper;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
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
            CreateMap<GeneralInfoDto, GeneralInfo>()
                .ConstructUsing(s => new GeneralInfo(
                    s.OrganizationName,
                    s.ContactPerson,
                    s.Phone,
                    s.Fax,
                    s.Email,
                    s.Web
                    ));
            CreateMap<CustomerDto, Customer>()
                .ConstructUsing(s=>new Customer(new CustomerId(s.Id), 
                new GeneralInfo(
                    s.GeneralInfo.OrganizationName,
                    s.GeneralInfo.ContactPerson,
                    s.GeneralInfo.Phone,
                    s.GeneralInfo.Fax,
                    s.GeneralInfo.Email,
                    s.GeneralInfo.Web
                    )));
        }
    }
}
