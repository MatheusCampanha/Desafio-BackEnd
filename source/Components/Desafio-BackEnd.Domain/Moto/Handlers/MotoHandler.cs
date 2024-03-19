﻿using Desafio_BackEnd.Domain.Core.Commands;
using Desafio_BackEnd.Domain.Core.Results;
using Desafio_BackEnd.Domain.Moto.Commands;
using Desafio_BackEnd.Domain.Moto.DTO;
using Desafio_BackEnd.Domain.Moto.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Moto.Interfaces.Repositories;
using System.Net;

namespace Desafio_BackEnd.Domain.Moto.Handlers
{
    public class MotoHandler(IMotoRepository motoRepository) : IMotoHandler
    {
        private readonly IMotoRepository _motoRepository = motoRepository;

        public async Task<Result<MotoDTO>> Handle(InsertMotoCommand command)
        {
            var errorResult = new Result<MotoDTO>(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var uniqueResult = await _motoRepository.GetResult(command.Placa);
            if (uniqueResult.StatusCode == HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotification(nameof(command.Placa), "Duplicada");
                return errorResult;
            }

            var moto = new Moto(command.Ano, command.Modelo, command.Placa);

            if (moto.Invalid)
            {
                errorResult.AddNotifications(moto);
                return errorResult;
            }

            var motoDTO = new MotoDTO(moto);
            return await _motoRepository.Insert(motoDTO);
        }

        public async Task<CommandResult> Handle(UpdateMotoCommand command)
        {
            var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var motoResult = await _motoRepository.GetById(command.Id);
            if (motoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(motoResult);
                return errorResult;
            }
            var moto = motoResult.QueryResult.Registros.First();

            var uniqueResult = await _motoRepository.GetResult(command.Placa);
            var unique = uniqueResult.QueryResult?.Registros?.First();
            if (uniqueResult.StatusCode == HttpStatusCode.OK.GetHashCode() && unique != null && unique.Id.Equals(command.Id))
            {
                errorResult.AddNotification(nameof(command.Placa), "Duplicada");
                return errorResult;
            }

            moto.SetAno(command.Ano);
            moto.SetModelo(command.Modelo);
            moto.SetPlaca(command.Placa);

            if (moto.Invalid)
            {
                errorResult.AddNotifications(moto);
                return errorResult;
            }

            var motoDTO = new MotoDTO(moto);
            return await _motoRepository.Update(motoDTO);
        }

        public async Task<CommandResult> Handle(UpdatePlacaCommand command)
        {
            var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

            if (!command.IsValid())
            {
                errorResult.AddNotifications(command);
                return errorResult;
            }

            var motoResult = await _motoRepository.GetById(command.Id!);
            if (motoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(motoResult);
                return errorResult;
            }
            var moto = motoResult.QueryResult.Registros.First();

            var uniqueResult = await _motoRepository.GetResult(command.Placa);
            var unique = uniqueResult.QueryResult?.Registros?.First();
            if (uniqueResult.StatusCode == HttpStatusCode.OK.GetHashCode() && unique != null && unique.Id.Equals(command.Id))
            {
                errorResult.AddNotification(nameof(command.Placa), "Duplicada");
                return errorResult;
            }

            moto.SetPlaca(command.Placa);

            if (moto.Invalid)
            {
                errorResult.AddNotifications(moto);
                return errorResult;
            }

            var motoDTO = new MotoDTO(moto);
            return await _motoRepository.Update(motoDTO);
        }

        public async Task<CommandResult> Handle(string id)
        {
            var errorResult = new CommandResult(HttpStatusCode.UnprocessableEntity.GetHashCode());

            var motoResult = await _motoRepository.GetById(id);
            if (motoResult.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                errorResult.AddNotifications(motoResult);
                return errorResult;
            }

            return await _motoRepository.Delete(id);
        }
    }
}