using ControleDeCinema.Aplicacao.ModuloFilme;
using ControleDeCinema.Aplicacao.ModuloSala;
using ControleDeCinema.Aplicacao.ModuloSessao;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers;

[Route("sessoes")]
[Authorize]
public class SessaoController : Controller
{
    private readonly SessaoAppService sessaoAppService;
    private readonly FilmeAppService filmeAppService;
    private readonly SalaAppService salaAppService;

    public SessaoController(
        SessaoAppService sessaoAppService,
        FilmeAppService filmeAppService,
        SalaAppService salaAppService
    )
    {
        this.sessaoAppService = sessaoAppService;
        this.filmeAppService = filmeAppService;
        this.salaAppService = salaAppService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var resultado = sessaoAppService.SelecionarTodos();

        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacaoHome(resultado.ToResult());

        var sessoesVm = new VisualizarSessoesViewModel(resultado.Value);

        this.ObterNotificacaoPendente();

        return View(sessoesVm);
    }

    [HttpGet("cadastrar")]
    [Authorize(Roles = "Empresa")]
    public IActionResult Cadastrar()
    {
        var filmesDisponiveis = filmeAppService.SelecionarTodos().ValueOrDefault;
        var salasDisponiveis = salaAppService.SelecionarTodos().ValueOrDefault;

        var cadastrarVm = new CadastrarSessaoViewModel(filmesDisponiveis, salasDisponiveis);

        cadastrarVm.Inicio = DateTime.UtcNow.Date;

        return View(cadastrarVm);
    }

    [HttpPost("cadastrar")]
    [Authorize(Roles = "Empresa")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarSessaoViewModel vm)
    {
        var filmesDisponiveis = filmeAppService.SelecionarTodos().ValueOrDefault;
        var salasDisponiveis = salaAppService.SelecionarTodos().ValueOrDefault;

        var entidade = FormularioSessaoViewModel.ParaEntidade(vm, filmesDisponiveis, salasDisponiveis);
        
        entidade.Inicio = entidade.Inicio.ToUniversalTime();

        var resultado = sessaoAppService.Cadastrar(entidade);

        if (resultado.IsFailed)
            return this.PreencherErrosModelState(resultado, vm);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    [Authorize(Roles = "Empresa")]
    public IActionResult Editar(Guid id)
    {
        var resultado = sessaoAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacao(resultado.ToResult());

        var filmesDisponiveis = filmeAppService.SelecionarTodos().ValueOrDefault;
        var salasDisponiveis = salaAppService.SelecionarTodos().ValueOrDefault;

        var editarVm = new EditarSessaoViewModel(
            resultado.Value.Id,
            resultado.Value.Inicio.ToLocalTime(),
            resultado.Value.NumeroMaximoIngressos,
            filmesDisponiveis,
            salasDisponiveis
        );

        return View(editarVm);
    }

    [HttpPost("editar/{id:guid}")]
    [Authorize(Roles = "Empresa")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarSessaoViewModel vm)
    {
        var filmesDisponiveis = filmeAppService.SelecionarTodos().ValueOrDefault;
        var salasDisponiveis = salaAppService.SelecionarTodos().ValueOrDefault;

        var entidade = FormularioSessaoViewModel.ParaEntidade(vm, filmesDisponiveis, salasDisponiveis);

        entidade.Inicio = entidade.Inicio.ToUniversalTime();

        var resultado = sessaoAppService.Editar(id, entidade);

        if (resultado.IsFailed)
            return this.PreencherErrosModelState(resultado, vm);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("encerrar/{id:guid}")]
    [Authorize(Roles = "Empresa")]
    [ValidateAntiForgeryToken]
    public IActionResult Encerrar(Guid id)
    {
        var resultado = sessaoAppService.Encerrar(id);
        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacao(resultado);

        return RedirectToAction(nameof(Detalhes), new { id });
    }

    [HttpGet("excluir/{id:guid}")]
    [Authorize(Roles = "Empresa")]
    public IActionResult Excluir(Guid id)
    {
        var resultado = sessaoAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacao(resultado.ToResult());

        var vm = new ExcluirSessaoViewModel(
            resultado.Value.Id,
            resultado.Value.Inicio,
            resultado.Value.Filme.Titulo,
            resultado.Value.Sala.Numero
        );

        return View(vm);
    }

    [HttpPost("excluir/{id:guid}")]
    [Authorize(Roles = "Empresa")]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(ExcluirSessaoViewModel vm)
    {
        var resultado = sessaoAppService.Excluir(vm.Id);

        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacao(resultado);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var resultado = sessaoAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacao(resultado.ToResult());

        var detalhesVm = DetalhesSessaoViewModel.ParaDetalhesVm(resultado.Value);

        return View(detalhesVm);
    }

    [HttpPost("vender-ingresso")]
    [ValidateAntiForgeryToken]
    public IActionResult VenderIngresso(VenderIngressoViewModel venderIngressoVm)
    {
        var resultado = sessaoAppService.VenderIngresso(
            venderIngressoVm.SessaoId,
            venderIngressoVm.Assento,
            venderIngressoVm.MeiaEntrada
        );

        if (resultado.IsFailed)
            return this.RedirecionarParaNotificacao(resultado.ToResult());

        return RedirectToAction(nameof(Detalhes), new { id = venderIngressoVm.SessaoId });
    }
}