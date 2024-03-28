using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Entregadores.DTO;

namespace Desafio_BackEnd.Domain.Entregadores.Interfaces.Repositories
{
    public interface IEntregadorRepository
    {
        Task<Result<Entregador>> GetById(string id);

        Task<Result<EntregadorDTO>> GetByIdResult(string id);

        Task<Result<EntregadorDTO>> GetByCNPJResult(string cnpj);

        Task<Result<EntregadorDTO>> GetByNumeroCNHResult(string numeroCNH);

        string SaveImagemCNH(string numeroCNH, string imagemBase64, string nomeArquivo);

        byte[] GetImagemCNH(string caminhoImagemCNH);

        void ReplaceImage(string filePath, byte[] newImageBytes);

        Task<Result<EntregadorDTO>> Insert(EntregadorDTO entregador);

        Task<CommandResult> Update(EntregadorDTO entregador);

        Task<CommandResult> Delete(string id);
    }
}