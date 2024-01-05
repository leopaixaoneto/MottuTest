using MottuKGS.Modules.GeneratedKey.Jobs;
using Quartz;

namespace MottuKGS.BackgroundJobs
{
    public static class BackgroundJobsStartup
    {
        public static void Config(IServiceCollection service)
        {
            service.AddQuartz( options =>
            {
                var AvailabilitCheckRoutine = JobKey.Create(nameof(AvailabilityCheckRoutine));
                var InMemoryAvailabilitCheckRoutine = JobKey.Create(nameof(InMemoryAvailabilityCheckRoutine));

                options
                .AddJob<AvailabilityCheckRoutine>(AvailabilitCheckRoutine)
                .AddTrigger(trigger => 
                    trigger.ForJob(AvailabilitCheckRoutine)
                    .WithSimpleSchedule(options =>
                        options.WithIntervalInSeconds(5).RepeatForever()));

                options
                .AddJob<InMemoryAvailabilityCheckRoutine>(InMemoryAvailabilitCheckRoutine)
                .AddTrigger(trigger =>
                    trigger.ForJob(InMemoryAvailabilitCheckRoutine)
                    .WithSimpleSchedule(options =>
                        options.WithIntervalInSeconds(5).RepeatForever()));
            });

            service.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
