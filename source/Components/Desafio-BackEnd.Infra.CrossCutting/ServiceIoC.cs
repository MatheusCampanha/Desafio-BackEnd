﻿using Desafio_BackEnd.Domain.Entregadores.Handlers;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.HFS;
using Desafio_BackEnd.Domain.Locacoes.Handlers;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Motos.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Notificacoes.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Pedidos.Handlers;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Pedidos.Interfaces.Repositories;
using Desafio_BackEnd.Domain.S3.Interfaces;
using Desafio_BackEnd.Domain.Users.Handlers;
using Desafio_BackEnd.Domain.Users.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Users.Interfaces.Repositories;
using Desafio_BackEnd.Infra.Data.Helpers.Jwt;
using Desafio_BackEnd.Infra.Data.Helpers.S3;
using Desafio_BackEnd.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio_BackEnd.Infra.CrossCutting
{
    public static class ServiceIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<HangfireJob>();

            #region Helpers

            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IS3Helper, S3Helper>();

            #endregion Helpers

            #region Handlers

            services.AddScoped<IEntregadorHandler, EntregadorHandler>();
            services.AddScoped<ILocacaoHandler, LocacaoHandler>();
            services.AddScoped<IMotoHandler, MotoHandler>();
            services.AddScoped<IPedidoHandler, PedidoHandler>();
            services.AddScoped<IPedidoEventHandler, PedidoEventHandler>();
            services.AddScoped<IUserHandler, UserHandler>();

            #endregion Handlers

            #region Repositories

            services.AddScoped<IEntregadorRepository, EntregadorRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion Repositories

            return services;
        }
    }
}