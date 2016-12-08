using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Application
{
    public interface IScheduleApplicationService
    {
        Task DelayScheduleAsync(VoyageId voyageId, TimeSpan delay, CancellationToken cancellationToken);
    }
}
