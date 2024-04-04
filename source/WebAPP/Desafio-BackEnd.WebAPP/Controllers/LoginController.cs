using Desafio_BackEnd.WebAPP.Interfaces;
using Desafio_BackEnd.WebAPP.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_BackEnd.WebAPP.Controllers
{
    public class LoginController(ILoginRepository loginRepository) : Controller
    {
        private readonly ILoginRepository _loginRepository = loginRepository;

        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            try
            {
                var token = await _loginRepository.Login(username, password);

                HttpContext.Session.SetString("JWTToken", token);

                return RedirectToAction("Index", "Home");
            }
            catch (ApplicationException)
            {
                return RedirectToAction("Index", new { error = "Usuário ou senha inválidos" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel user)
        {
            try
            {
                var result = await _loginRepository.Register(user);

                if (result.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Index", new { success = "Usuário cadastrado com sucesso!" });
                else
                    return RedirectToAction("Register", new { error = "Usuário já cadastrado" });
            }
            catch (ApplicationException)
            {
                return RedirectToAction("Index", new { error = "Usuário ou senha inválidos" });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}