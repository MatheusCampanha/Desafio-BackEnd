using Desafio_BackEnd.Domain.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Desafio_BackEnd.Domain.Entregadores
{
    public class Entregador : Entity
    {
        public Entregador(string userId, string nome, string cnpj, DateTime dataNascimento, string numeroCNH, string tipoCNH, string? caminhoImagemCNH = null)
        {
            SetUserId(userId);
            SetNome(nome);
            SetCNPJ(cnpj);
            SetDataNascimento(dataNascimento);
            SetNumeroCNH(numeroCNH);
            SetTipoCNH(tipoCNH);
            SetCaminhoImagemCNH(caminhoImagemCNH);
        }

        public Entregador(string id, string userId, string nome, string cnpj, DateTime dataNascimento, string numeroCNH, string tipoCNH, string? caminhoImagemCNH = null)
        {
            SetId(id);
            SetUserId(userId);
            SetNome(nome);
            SetCNPJ(cnpj);
            SetDataNascimento(dataNascimento);
            SetNumeroCNH(numeroCNH);
            SetTipoCNH(tipoCNH);
            SetCaminhoImagemCNH(caminhoImagemCNH);
        }

        public string Id { get; private set; } = default!;
        public string UserId { get; private set; } = default!;
        public string Nome { get; private set; } = default!;
        public string CNPJ { get; private set; } = default!;
        public DateTime DataNascimento { get; private set; }
        public string NumeroCNH { get; private set; } = default!;
        public string TipoCNH { get; private set; } = default!;
        public string? CaminhoImagemCNH { get; private set; }

        public void SetId(string id)
        {
            if (Valid)
                Id = id;
        }

        public void SetUserId(string userId)
        {
            if (Valid)
                UserId = userId;
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

        public void SetTipoCNH(string tipoCNH)
        {
            if (Valid)
                TipoCNH = tipoCNH;
        }

        public void SetCaminhoImagemCNH(string? caminhoImagemCNH)
        {
            if (Valid)
                CaminhoImagemCNH = caminhoImagemCNH;
        }
    }
}