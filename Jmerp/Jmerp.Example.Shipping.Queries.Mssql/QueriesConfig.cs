using EventFlow;
using EventFlow.Extensions;
using EventFlow.MsSql.Extensions;
using Jmerp.Example.Shipping.Queries.Mssql.Cargos;
using Jmerp.Example.Shipping.Queries.Mssql.Voyages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql
{
    public static class QueriesConfig
    {
        public static Assembly Assembly { get; } = typeof(QueriesConfig).Assembly;

        public static IEventFlowOptions ConfigureShippingQueriesMssql(
            this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddQueryHandlers(Assembly)
                .UseMssqlReadModel<VoyageReadModel>()
                .UseMssqlReadModel<CargoReadModel>();
        }

        //public static IEventFlowOptions ConfigureShippingRepository(this IEventFlowOptions eventFlowOptions)
        //{
        //    return eventFlowOptions
        //         .AddDefaults(Assembly)
        //         .RegisterServices(sr =>
        //         {
        //             sr.Register<IDbFactory, DbFactory>();
        //             sr.Register<IUnitOfWork, UnitOfWork>();
        //             sr.Register<ICargoRepository, CargoRepository>();
        //             sr.Register<IVoyageRepository, VoyageRepository>();
        //         });
        //}
    }
}
