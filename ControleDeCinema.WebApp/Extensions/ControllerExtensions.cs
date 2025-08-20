using ControleDeCinema.WebApp.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Extensions;

public static class ControllerExtensions
{
    public static IActionResult RedirecionarParaNotificacao(this Controller controller, Result resultado)
    {
        foreach (var error in resultado.Errors)
        {
            var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                error.Message,
                error.Reasons[0].Message
            );

            controller.TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
        }

        return controller.RedirectToAction("Index");
    }

    public static IActionResult PreencherErrosModelState(this Controller controller, Result resultado, object viewModel)
    {
        foreach (var erro in resultado.Errors)
        {
            var chave = erro.Metadata.TryGetValue("TipoErro", out var tipo) ?
                tipo.ToString() ?? "ErroInesperado" : "ErroInesperado";

            foreach (var reason in erro.Reasons)
                controller.ModelState.AddModelError(chave, reason.Message);
        }

        return controller.View(viewModel);
    }
}
