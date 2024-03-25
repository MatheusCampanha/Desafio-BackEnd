using Desafio_BackEnd.WebAPI.Configurations.Swagger.Filters;
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

                c.OperationFilter<NotifiablePropertyFilter>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
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