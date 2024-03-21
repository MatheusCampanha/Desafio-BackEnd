using Desafio_BackEnd.Domain.Core.Entities;
using Desafio_BackEnd.Domain.Core.Enums;

namespace Desafio_BackEnd.Domain.Entregadores
{
    public class Entregador : Entity
    {
        public Entregador(string? id, string nome, string cnpj, DateTime dataNascimento, string numeroCNH, TipoCNHEnum tipoCNH, string caminhoImagemCNH)
        {
            SetId(id);
            SetNome(nome);
            SetCNPJ(cnpj);
            SetDataNascimento(dataNascimento);
            SetNumeroCNH(numeroCNH);
            SetTipoCNH(tipoCNH);
            SetCaminhoImagemCNH(caminhoImagemCNH);
        }

        public Entregador(string nome, string cnpj, DateTime dataNascimento, string numeroCNH, TipoCNHEnum tipoCNH, string caminhoImagemCNH)
        {
            SetNome(nome);
            SetCNPJ(cnpj);
            SetDataNascimento(dataNascimento);
            SetNumeroCNH(numeroCNH);
            SetTipoCNH(tipoCNH);
            SetCaminhoImagemCNH(caminhoImagemCNH);
        }

        public string Id { get; private set; } = default!;
        public string Nome { get; private set; } = default!;
        public string CNPJ { get; private set; } = default!;
        public DateTime DataNascimento { get; private set; }
        public string NumeroCNH { get; private set; } = default!;
        public TipoCNHEnum TipoCNH { get; private set; }
        public string CaminhoImagemCNH { get; private set; } = default!;

        public void SetId(string id)
        {
            if (Valid)
                Id = id;
        }

        public void SetNome(string nome)
        {
            if (Valid)
                Nome = nome;
        }

        public void SetCNPJ(string cnpj)
        {
            if (Valid)
                CNPJ = cnpj;
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            if (Valid)
                DataNascimento = dataNascimento;
        }

        public void SetNumeroCNH(string numeroCNH)
        {
            if (Valid)
                NumeroCNH = numeroCNH;
        }

        public void SetTipoCNH(TipoCNHEnum tipoCNH)
        {
            if (Valid)
                TipoCNH = tipoCNH;
        }

        public void SetCaminhoImagemCNH(string caminhoImagemCNH)
        {
            if (Valid)
                CaminhoImagemCNH = caminhoImagemCNH;
        }
    }
}