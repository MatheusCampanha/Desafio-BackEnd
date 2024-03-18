using Desafio_BackEnd.WebAPI.Configurations.Swagger.Filters;
using Desafio_BackEnd.WebAPI.Configurations.Swagger.HeaderParameters;
using Microsoft.OpenApi.Models;

namespace Desafio_BackEnd.WebAPI.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services, string applicationName)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = applicationName });

                c.OperationFilter<AuthenticationHeaderParameter>();

                c.OperationFilter<NotifiablePropertyFilter>();
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentException(null, nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("../swagger/v1/swagger.json", "v1");
            });
        }
    }
}