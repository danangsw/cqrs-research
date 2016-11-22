﻿using AutoMapper;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
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
            CreateMap<Customer, CustomerDto>();
        }
    }
}
