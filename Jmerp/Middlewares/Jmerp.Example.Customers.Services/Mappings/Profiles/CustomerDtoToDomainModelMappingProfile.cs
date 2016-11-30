using AutoMapper;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Customers.Middlewares.Models;
using System.Collections.Generic;

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
            CreateMap<string, CustomerId>()
                .ConstructUsing(s => new CustomerId(s));
            CreateMap<string, AddressId>()
                .ConstructUsing(s =>
                new AddressId(
                    string.IsNullOrEmpty(s)
                    ? AddressId.New.Value : s)
                    );
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
                .ConstructUsing(s =>
                new Customer(new CustomerId(s.Id),
                new GeneralInfo(
                    s.GeneralInfo.OrganizationName,
                    s.GeneralInfo.ContactPerson,
                    s.GeneralInfo.Phone,
                    s.GeneralInfo.Fax,
                    s.GeneralInfo.Email,
                    s.GeneralInfo.Web
                    )));
            CreateMap<AddressDto, Address>()
                .ConstructUsing(s =>
                new Address(new AddressId(
                string.IsNullOrEmpty(s.Id) ? AddressId.New.Value : s.Id),
                new CustomerId(s.CustomerId),
                s.AddressType,
                s.AddressLine1,
                s.AddressLine2,
                s.City,
                s.StateProvince,
                s.PostalCode,
                s.SetDefault));
            CreateMap<AddressDetailDto, AddressDetail>()
                .ConstructUsing(s =>
                new AddressDetail(
                    Mapper.Map<List<AddressDto>, List<Address>>(s.Addresses))
                    );
        }
    }
}
