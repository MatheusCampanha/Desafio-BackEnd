using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Moto.DTO;
using Desafio_BackEnd.Domain.Moto.Queries;

namespace Desafio_BackEnd.Domain.Moto.Interfaces.Repositories
{
    public interface IMotoRepository
    {
        Task<Result<Moto>> GetById(string id);

        Task<Result<MotoDTO>> GetByIdResult(string id);

        Task<Result<MotoDTO>> GetResult(string placa);

        Task<Result<MotoDTO>> GetResult(GetMotoQuery query);

        Task<Result<MotoDTO>> Insert(MotoDTO moto);

        Task<CommandResult> Update(MotoDTO moto);

        Task<CommandResult> Delete(string id);
    }
}