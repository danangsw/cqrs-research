using EventFlow;
using EventFlow.Extensions;
using EventFlow.MsSql.Extensions;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Entities;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using Jmerp.Example.Shipping.Queries.Mssql.Cargos;
using Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueriesSql;
using Jmerp.Example.Shipping.Queries.Mssql.Cargos.Subscriber;
using Jmerp.Example.Shipping.Queries.Mssql.Locations;
using Jmerp.Example.Shipping.Queries.Mssql.Voyages;
using Jmerp.Example.Shipping.Queries.Mssql.Voyages.QueriesSql;
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
                .RegisterServices(sr => {
                    sr.RegisterType(typeof(CarrierMovementLocator));
                    sr.Register<IVoyageQueries, VoyageQueries>();
                    sr.Register<ICarrierMovementQueries, CarrierMovementQueries>();
                    sr.RegisterType(typeof(TransportLegLocator));
                    sr.Register<ICargoQueries, CargoQueries>();
                    sr.Register<ITransportLegQueries, TransportLegQueries>();
                })
                .AddQueryHandlers(Assembly)
                .AddSubscribers(typeof(TransportLegDeleteSubscriber))
                .UseMssqlReadModel<LocationReadModel>()
                .UseMssqlReadModel<VoyageReadModel>()
                .UseMssqlReadModel<CarrierMovementReadModel, CarrierMovementLocator>()
                .UseMssqlReadModel<CargoReadModel>()
                .UseMssqlReadModel<TransportLegReadModel>()
                .UseMssqlReadModel<TransportLegReadModel, TransportLegLocator>();
        }
    }
}
