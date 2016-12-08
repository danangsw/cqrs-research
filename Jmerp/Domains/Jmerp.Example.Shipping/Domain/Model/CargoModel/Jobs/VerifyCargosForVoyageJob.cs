
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Configuration;
using EventFlow.Jobs;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using EventFlow.Queries;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Jobs
{
    [JobVersion("VerifyCargosForVoyage", 1)]
    public class VerifyCargosForVoyageJob : IJob
    {
        public VoyageId VoyageId { get; }

        public VerifyCargosForVoyageJob(
            VoyageId voyageId)
        {
            VoyageId = voyageId;
        }

        public async Task ExecuteAsync(IResolver resolver, CancellationToken cancellationToken)
        {
            // Consideration: Fetching all cargos that are affected by an updated
            // schedule could potentially fetch several thousands. Each of these
            // potential re-routes would then take a considerable amount of time
            // and will thus be required to be executed in parallel

            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var jobScheduler = resolver.Resolve<IJobScheduler>();

            var cargos = await queryProcessor.ProcessAsync(new GetCargosDependentOnVoyageQuery(VoyageId), cancellationToken).ConfigureAwait(false);
            var jobs = cargos.Select(c => new VerifyCargoItineraryJob(c.Id));

            await Task.WhenAll(jobs.Select(j => jobScheduler.ScheduleNowAsync(j, cancellationToken))).ConfigureAwait(false);
        }
    }
}
