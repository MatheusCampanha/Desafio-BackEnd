using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Motos.Commands;
using Desafio_BackEnd.Domain.Motos.DTO;

namespace Desafio_BackEnd.Domain.Motos.Interfaces.Handlers
{
    public interface IMotoHandler
    {
        Task<Result<MotoDTO>> Handle(InsertMotoCommand command);

        Task<CommandResult> Handle(UpdatePlacaCommand command);

        Task<CommandResult> Handle(string id);
    }
}