using Desafio_BackEnd.WebAPP.Models.Moto;

namespace Desafio_BackEnd.WebAPP.Interfaces
{
    public interface IMotoRepository
    {
        Task<List<MotoViewModel>> GetAll(string? placa, string token);

        Task SaveEdit(EditMotoViewModel model, string token);

        Task SaveNew(CreateMotoViewModel model, string token);

        Task DeleteMoto(string id, string token);
    }
}