namespace Desafio_BackEnd.WebAPP.Models.Moto
{
    public class MotoViewModel
    {
        public string Id { get; set; } = default!;
        public int Ano { get; set; }
        public string Modelo { get; set; } = default!;
        public string Placa { get; set; } = default!;
        public bool Alugada { get; set; }
    }
}