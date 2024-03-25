using Desafio_BackEnd.Domain.Users;
using Desafio_BackEnd.Domain.Users.Commands;
using Desafio_BackEnd.Domain.Users.Interfaces.Handlers;
using Desafio_BackEnd.Domain.Users.Results;
using Desafio_BackEnd.Infra.Data.Helpers.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPI.Controllers
{
    public class AuthController(IJwtHelper jwtHelper, IUserHandler userHandler) : ControllerBase
    {
        private readonly IJwtHelper _jwtHelper = jwtHelper;
        private readonly IUserHandler _userHandler = userHandler;

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLogin request)
        {
            var user = await _userHandler.VerifyUser(request.Username, request.Password);

            if (user == null)
                return Unauthorized();

            var token = _jwtHelper.GenerateToken(user.Username, nameof(user.Role));
            return Ok(new { Token = token });
        }

        [HttpPost("/register")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(CreateUserCommandResult))]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var result = await _userHandler.CreateUser(command);
            return Ok(result);
        }
    }
}