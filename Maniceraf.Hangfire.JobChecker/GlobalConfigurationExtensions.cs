using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Maniceraf.Hangfire.JobChecker
{
    public static class GlobalConfigurationExtensions
    {
        public static IServiceCollection AddHangfireJobHunter<T>(this IServiceCollection services) where T : class
        {
            services.AddScoped(typeof(IJobChecker), typeof(T));

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetService<IJobChecker>();
                    if (scopedService != null)
                    {
                        GlobalJobFilters.Filters.Add(new JobCheckerFilter(scopedService));
                    }
                }
            }

            return services;
        }
    }
}
