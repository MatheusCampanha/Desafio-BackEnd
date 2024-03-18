using Desafio_BackEnd.Domain.Core.Notifications;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Desafio_BackEnd.WebAPI.Configurations.Swagger.Filters
{
    public class NotifiablePropertyFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var type = typeof(Notifiable);
            var ignoredProperties = type.GetProperties().Select(x => x.Name);

            // Find all properties by name which need to be removed
            // and not shown on the swagger spec.
            operation.Parameters
                .Where(param => ignoredProperties.Any(exec => string.Equals(exec, param.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(prExclude => prExclude)
                .ToList()
                .ForEach(key => operation.Parameters.Remove(key));
        }
    }
}