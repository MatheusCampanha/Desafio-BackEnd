using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Moto.Commands;
using Desafio_BackEnd.Domain.Moto.DTO;

namespace Desafio_BackEnd.Domain.Moto.Interfaces.Handlers
{
    public interface IMotoHandler
    {
        Task<Result<MotoDTO>> Handle(InsertMotoCommand command);

        Task<CommandResult> Handle(UpdateMotoCommand command);

        Task<CommandResult> Handle(UpdatePlacaCommand command);

        Task<CommandResult> Handle(string id);
    }
}