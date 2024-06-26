﻿using Desafio_BackEnd.Domain.Core.Entities;

namespace Desafio_BackEnd.Domain.Motos
{
    public class Moto : Entity
    {
        public Moto(string? id, int ano, string modelo, string placa, bool alugada)
        {
            SetId(id);
            SetAno(ano);
            SetModelo(modelo);
            SetPlaca(placa);
            SetAlugada(alugada);
        }

        public Moto(int ano, string modelo, string placa, bool alugada)
        {
            SetAno(ano);
            SetModelo(modelo);
            SetPlaca(placa);
            SetAlugada(alugada);
        }

        public string? Id { get; private set; }
        public int Ano { get; private set; }
        public string Modelo { get; private set; } = default!;
        public string Placa { get; private set; } = default!;
        public bool Alugada { get; private set; }

        public void SetId(string? id)
        {
            if (Valid)
                Id = id;
        }

        public void SetAno(int ano)
        {
            if (ano <= 0)
                AddNotification("Moto.ano", "Deve ser maior que zero");

            if (Valid)
                Ano = ano;
        }

        public void SetModelo(string modelo)
        {
            if (Valid)
                Modelo = modelo;
        }

        public void SetPlaca(string placa)
        {
            if (Valid)
                Placa = placa;
        }

        public void SetAlugada(bool alugada)
        {
            if (Valid)
                Alugada = alugada;
        }
    }
}