using Desafio_BackEnd.Domain.Core.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Desafio_BackEnd.WebAPI.Configurations.Swagger.HeaderParameters
{
    public class AuthenticationHeaderParameter(Settings settings) : IOperationFilter
    {
        private readonly Settings _settings = settings;

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = nameof(_settings.Token),
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" },
                Required = true
            });
        }
    }
}