using Hangfire.Server;

namespace Maniceraf.Hangfire.JobChecker
{
    public interface IJobChecker
    {
        Task ExecuteAync(PerformedContext context);
    }
}
