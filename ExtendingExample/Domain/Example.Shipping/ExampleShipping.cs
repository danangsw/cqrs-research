using EventFlow;
using EventFlow.Extensions;
using Example.Shipping.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping
{
    public static class ExampleShipping
    {
        public static Assembly Assembly { get; } = typeof(ExampleShipping).Assembly;

        public static IEventFlowOptions ConfigureShipping(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly)
                .RegisterServices(sr =>
                {
                    sr.Register<IScheduleApplicationService, ScheduleApplicationService>();
                });
        }
    }
}
