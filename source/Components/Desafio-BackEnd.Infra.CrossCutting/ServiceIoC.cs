using Amazon.S3;
using Desafio_BackEnd.Domain.Entregadores.Handlers;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Motos.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using Desafio_BackEnd.Infra.Data.Helpers;
using Desafio_BackEnd.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio_BackEnd.Infra.CrossCutting
{
    public static class ServiceIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Helpers

            services.AddAWSService<IAmazonS3>();
            services.AddScoped<IS3Helper, S3Helper>();

            #endregion Helpers

            #region Handlers

            services.AddScoped<IEntregadorHandler, EntregadorHandler>();
            services.AddScoped<IMotoHandler, MotoHandler>();

            #endregion Handlers

            #region Repositories

            services.AddScoped<IEntregadorRepository, EntregadorRepository>();
            services.AddScoped<IMotoRepository, MotoRepository>();

            #endregion Repositories

            return services;
        }
    }
}