using EventFlow;
using Example.Shipping.Domain.Model.VoyageModel;
using Example.Shipping.Domain.Model.VoyageModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Application
{
    public class ScheduleApplicationService : IScheduleApplicationService
    {
        private readonly ICommandBus _commandBus;

        public ScheduleApplicationService(
            ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public Task DelayScheduleAsync(VoyageId voyageId, TimeSpan delay, CancellationToken cancellationToken)
        {
            return _commandBus.PublishAsync(new VoyageDelayCommand(voyageId, delay), cancellationToken);
        }
    }
}
