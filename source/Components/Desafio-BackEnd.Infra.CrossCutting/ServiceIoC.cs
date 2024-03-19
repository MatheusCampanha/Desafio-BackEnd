using Desafio_BackEnd.Domain.Motos.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using Desafio_BackEnd.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio_BackEnd.Infra.CrossCutting
{
    public static class ServiceIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Handlers

            services.AddScoped<IMotoHandler, MotoHandler>();

            #endregion Handlers

            #region Repositories

            services.AddScoped<IMotoRepository, MotoRepository>();

            #endregion Repositories

            return services;
        }
    }
}