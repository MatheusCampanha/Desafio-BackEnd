﻿using Desafio_BackEnd.Domain.Locacoes.Commands;
using Desafio_BackEnd.Domain.Locacoes.DTO;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Locacoes.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class LocacaoController(ILocacaoHandler locacaoHandler, ILocacaoRepository locacaoRepository) : BaseController
    {
        private readonly ILocacaoHandler _locacaoHandler = locacaoHandler;
        private readonly ILocacaoRepository _locacaoRepository = locacaoRepository;

        [HttpPost]
        [Route("locacoes")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(LocacaoDTO))]
        public async Task<IActionResult> Post([FromBody] InsertLocacaoCommand command)
        {
            var result = await _locacaoHandler.Handle(command);
            return ResultFirst(result);
        }
    }
}