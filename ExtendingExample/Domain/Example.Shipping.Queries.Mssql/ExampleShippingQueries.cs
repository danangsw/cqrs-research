﻿using EventFlow;
using EventFlow.Extensions;
using EventFlow.MsSql.Extensions;
using Example.Shipping.Domain.Model.VoyageModel.Entities;
using Example.Shipping.Queries.Mssql.Cargos;
using Example.Shipping.Queries.Mssql.Locations;
using Example.Shipping.Queries.Mssql.Voyage;
using Example.Shipping.Queries.Mssql.Voyage.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Queries.Mssql
{
    public static class ExampleShippingQueries
    {
        public static Assembly Assembly { get; } = typeof(ExampleShippingQueries).Assembly;

        public static IEventFlowOptions ConfigureShipingQueriesMssql(
            this IEventFlowOptions eventFlowOptions )
        {
            return eventFlowOptions
                .RegisterServices(sr => {
                    sr.RegisterType(typeof(CarrierMovementLocator));
                    sr.Register<IVoyageQueries, VoyageQueries>();
                    sr.Register<ICarrierMovementQueries, CarrierMovementQueries>();
                 })
                .AddQueryHandlers(Assembly)
                .UseMssqlReadModel<LocationReadModel>()
                .UseMssqlReadModel<VoyageReadModel>()
                .UseMssqlReadModel<CargoReadModel>()
                .UseMssqlReadModel<CarrierMovementReadModel, CarrierMovementLocator>();
        }

    }
}
