using Hangfire.Server;

namespace Maniceraf.Hangfire.JobChecker
{
    public class JobCheckerFilter : IServerFilter
    {
        private readonly IJobChecker _jobHunter;
        public JobCheckerFilter(IJobChecker jobHunter)
        {
            _jobHunter = jobHunter;
        }

        public void OnPerforming(PerformingContext context)
        {
        }

        [Obsolete]
        public void OnPerformed(PerformedContext context)
        {
            _jobHunter.ExecuteAync(context);
            Console.WriteLine($"Job Hunter: Checking {context.Job} completed !!!");
        }
    }
}
