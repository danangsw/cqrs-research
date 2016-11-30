using EventFlow;
using Jmerp.Example.Customers.Middlewares.Mappings;
using Jmerp.Example.Customers.Middlewares.Services;
using Jmerp.Example.Customers.Queries.InMemory;

namespace Jmerp.Example.Customers.Middlewares
{
    public static class CustomerBootstrapper
    {
        public static IEventFlowOptions CustomerBootstrapperConfiguration(this IEventFlowOptions options)
        {
            CustomerMapperConfiguration.Configure();

            return options
                .CustomerConfigurationDomain()
                .ConfigureCustomerQueriesInMemory()
                .RegisterServices(sr =>
                {
                    sr.Register<ICreateGeneralInfoApplicationServices, CreateGeneralInfoApplicationServices>();
                    sr.Register<IUpdateGeneralInfoApplicationServices, UpdateGeneralInfoApplicationServices>();
                    sr.Register<IAddAddressApplicationServices, AddAddressApplicationServices>();
                    sr.Register<IUpdateAddressApplicationServices, UpdateAddressApplicationServices>();
                    sr.Register<ISetAddressAsDefaultApplicationServices, SetAddressAsDefaultApplicationServices>();
                    sr.Register<IRemoveAddressApplicationServices, RemoveAddressApplicationServices>();
                });
        }
    }
}
