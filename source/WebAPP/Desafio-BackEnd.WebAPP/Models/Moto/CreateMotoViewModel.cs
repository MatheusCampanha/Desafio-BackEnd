namespace Desafio_BackEnd.WebAPP.Models.Moto
{
    public class CreateMotoViewModel
    {
        public int Ano { get; set; }
        public string Modelo { get; set; } = default!;
        public string Placa { get; set; } = default!;
    }
}
