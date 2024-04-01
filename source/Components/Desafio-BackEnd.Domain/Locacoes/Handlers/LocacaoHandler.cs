using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Locacoes.Commands;
using Desafio_BackEnd.Domain.Locacoes.DTO;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Motos.Interfaces.Repositories;
using System.Net;

namespace Desafio_BackEnd.Domain.Locacoes.Handlers
{
    public class LocacaoHandler(ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository, IMotoRepository motoRepository) : ILocacaoHandler
    {
        private readonly ILocacaoRepository _locacaoRepository = locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository = entregadorRepository;
        private readonly IMotoRepository _motoRepository = motoRepository;

        public async Task<Result<LocacaoDTO>> Handle(InsertLocacaoCommand command)
        {
            var errorResult = new Result<LocacaoDTO>(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var entregadorResult = await _entregadorRepository.GetByIdResult(command.EntregadorId);
            if (entregadorResult == null) 
            {
                errorResult.AddNotification(nameof(command.EntregadorId), "Não encontrado");
                return errorResult;
            }

            var motoResult = await _motoRepository.GetById(command.MotoId);
            if (motoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotification(nameof(command.MotoId), "Não encontrada");
                return errorResult;
            }

            var locacao = new Locacao(command.EntregadorId, command.MotoId, command.DataInicial, command.DataFinal, command.DataPrevisaoEntrega, command.ValorTotal, command.Plano, command.Finalizada);

            if (locacao.Invalid)
            {
                errorResult.AddNotifications(locacao);
                return errorResult;
            }

            var locacaoDTO = new LocacaoDTO(locacao);
            return await _locacaoRepository.Insert(locacaoDTO);
        }
    }
}
