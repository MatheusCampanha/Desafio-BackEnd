using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Motos.DTO;
using Desafio_BackEnd.Domain.Motos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IMotoRepository
    {
        Task<MotoDTO> GetAll([FromQuery] GetMotoQuery query, string token);
    }
}
