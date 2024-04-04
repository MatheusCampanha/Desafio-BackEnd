using Desafio_BackEnd.Domain.HFS;
using Hangfire;
using Hangfire.MemoryStorage;

namespace Desafio_BackEnd.WebAPI.Configurations.HangFire
{
    public static class HangFireSetup
    {
        public static void AddHangfireSetup(this IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseMemoryStorage();
            });

            services.AddHangfireServer();
        }

        public static void UseHangFireSetup(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<HangfireJob>(x => x.StartListening(), Cron.Minutely, TimeZoneInfo.Local);
        }
    }
}