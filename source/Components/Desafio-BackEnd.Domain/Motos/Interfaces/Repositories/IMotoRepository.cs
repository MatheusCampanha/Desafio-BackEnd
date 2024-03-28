using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Motos.DTO;

namespace Desafio_BackEnd.Domain.Motos.Interfaces.Repositories
{
    public interface IMotoRepository
    {
        Task<Result<Moto>> GetById(string id);

        Task<List<MotoDTO>> GetResult(string? placa);

        Task<Result<MotoDTO>> Insert(MotoDTO moto);

        Task<CommandResult> Update(MotoDTO moto);

        Task<CommandResult> Delete(string id);
    }
}