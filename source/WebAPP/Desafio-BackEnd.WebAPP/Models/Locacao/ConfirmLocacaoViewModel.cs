using Desafio_BackEnd.WebAPP.Models.Moto;

namespace Desafio_BackEnd.WebAPP.Models.Locacao
{
    public class ConfirmLocacaoViewModel
    {
        public LocacaoViewModel? LocacaoViewModel { get; set; }
        public List<MotoViewModel>? MotoViewModel { get; set; }
    }
}