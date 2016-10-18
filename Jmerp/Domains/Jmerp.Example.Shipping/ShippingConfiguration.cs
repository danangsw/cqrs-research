using EventFlow;
using EventFlow.Extensions;
using Jmerp.Example.Shipping.Application;
using Jmerp.Example.Shipping.Domain.Services;
using Jmerp.Example.Shipping.ExternalServices.Routing;
using System.Reflection;

namespace Jmerp.Example.Shipping
{
    public static class ShippingConfiguration
    {
        public static Assembly Assembly { get; } = typeof(ShippingConfiguration).Assembly;

        public static IEventFlowOptions ConfigureShippingDomain(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                 .AddDefaults(Assembly)
                 .RegisterServices(sr =>
                 {
                     sr.Register<IBookingApplicationService, BookingApplicationService>();
                     sr.Register<IScheduleApplicationService, ScheduleApplicationService>();
                     sr.Register<IUpdateItineraryService, UpdateItineraryService>();
                     sr.Register<IRoutingService, RoutingService>();
                 });
        }
    }
}
