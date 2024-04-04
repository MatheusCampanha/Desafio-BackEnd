using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.Commands;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.S3.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Desafio_BackEnd.Domain.Entregadores.Handlers
{
    public class EntregadorHandler(IEntregadorRepository entregadorRepository, IS3Helper s3Repository) : IEntregadorHandler
    {
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;
        private readonly IS3Helper _s3Repository = s3Repository;

        public async Task<Result<EntregadorDTO>> Handle(InsertEntregadorCommand command)
        {
            try
            {
                var errorResult = new Result<EntregadorDTO>(HttpStatusCode.UnprocessableEntity.GetHashCode());

                if (!command.IsValid())
                {
                    errorResult.AddNotifications(command);
                    return errorResult;
                }

                var uniqueCNPJResult = await _entregadorRepository.GetByCNPJResult(command.CNPJ);
                if (uniqueCNPJResult.StatusCode == HttpStatusCode.OK.GetHashCode())
                {
                    errorResult.AddNotification(nameof(command.CNPJ), "Duplicado");
                    return errorResult;
                }

                var uniqueNumeroCNHResult = await _entregadorRepository.GetByNumeroCNHResult(command.NumeroCNH);
                if (uniqueNumeroCNHResult.StatusCode == HttpStatusCode.OK.GetHashCode())
                {
                    errorResult.AddNotification(nameof(command.NumeroCNH), "Duplicado");
                    return errorResult;
                }

                var entregador = new Entregador(command.UserId, command.Nome, command.CNPJ, command.DataNascimento, command.NumeroCNH, command.TipoCNH);

                if (entregador.Invalid)
                {
                    errorResult.AddNotifications(entregador);
                    return errorResult;
                }

                var entregadorDTO = new EntregadorDTO(entregador);
                return await _entregadorRepository.Insert(entregadorDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommandResult> Handle(string id, IFormFile imagemCNH)
        {
            try
            {
                var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

                var entregadorResult = await _entregadorRepository.GetById(id);
                if (entregadorResult.StatusCode != HttpStatusCode.OK.GetHashCode())
                {
                    errorResult.AddNotifications(entregadorResult);
                    return errorResult;
                }
                var entregador = entregadorResult.QueryResult.Registros.First();

                var caminhoImagemCNH = await _s3Repository.UploadFile(entregador.Id, imagemCNH);
                entregador.SetCaminhoImagemCNH(caminhoImagemCNH);

                if (entregador.Invalid)
                {
                    errorResult.AddNotifications(entregador);
                    return errorResult;
                }

                var entregadorDTO = new EntregadorDTO(entregador);
                return await _entregadorRepository.Update(entregadorDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommandResult> Handle(string id)
        {
            var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

            var entregadorResult = await _entregadorRepository.GetById(id);
            if (entregadorResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(entregadorResult);
                return errorResult;
            }
            var entregador = entregadorResult.QueryResult.Registros.First();

            return await _entregadorRepository.Delete(id);
        }
    }
}