using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers;

[Route("autenticacao")]
public class AutenticacaoController : Controller
{
    [HttpGet("registro")]
    public IActionResult Registro()
    {
        var registroVm = new RegistroViewModel();

        return View(registroVm);
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registro(RegistroViewModel registroVm)
    {
        return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var loginVm = new LoginViewModel();

        return View(loginVm);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel loginVm)
    {
        return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction(nameof(Login));
    }
}