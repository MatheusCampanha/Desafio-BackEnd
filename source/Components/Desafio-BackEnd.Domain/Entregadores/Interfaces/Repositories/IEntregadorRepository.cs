using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.DTO;
using Microsoft.AspNetCore.Http;

namespace Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories
{
    public interface IEntregadorRepository
    {
        Task<Result<Entregador>> GetById(string id);

        Task<List<EntregadorDTO>> GetAll();
        Task<List<string>> GetAvaiable();

        Task<EntregadorDTO> GetByIdResult(string id);
        Task<EntregadorDTO> GetByUserIdResult(string userId);

        Task<Result<EntregadorDTO>> GetByCNPJResult(string cnpj);

        Task<Result<EntregadorDTO>> GetByNumeroCNHResult(string numeroCNH);

        Task<Result<EntregadorDTO>> Insert(EntregadorDTO entregador);

        Task<CommandResult> Update(EntregadorDTO entregador);

        Task<CommandResult> Delete(string id);
    }
}