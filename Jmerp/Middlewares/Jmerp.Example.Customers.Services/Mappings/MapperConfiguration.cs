using AutoMapper;
using Jmerp.Example.Customers.Middlewares.Mappings.Profiles;

namespace Jmerp.Example.Customers.Middlewares.Mappings
{
    public class MapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //TODO: Add mapper profiles here
                cfg.AddProfile<CustomerDomainModelToDtoMappingProfile>();
                cfg.AddProfile<CustomerDtoToDomainModelMappingProfile>();
            });
        }
    }
}
