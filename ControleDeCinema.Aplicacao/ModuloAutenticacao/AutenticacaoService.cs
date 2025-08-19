using ControleDeCinema.Aplicacao.Compartilhado;
using ControleDeCinema.Dominio.ModuloAutenticacao;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace ControleDeCinema.Aplicacao.ModuloAutenticacao;

public class AutenticacaoAppService
{
    private readonly UserManager<Usuario> userManager;
    private readonly SignInManager<Usuario> signInManager;
    private readonly RoleManager<Cargo> roleManager;

    public AutenticacaoAppService(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Cargo> roleManager
    )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    public async Task<Result> RegistrarAsync(Usuario usuario, string senha, TipoUsuario tipo)
    {
        var usuarioResult = await userManager.CreateAsync(usuario, senha);

        if (!usuarioResult.Succeeded)
        {
            var erros = usuarioResult
                .Errors
                .Select(failure => failure.Description)
                .ToList();


            return Result.Fail(ResultadosErro.RequisicaoInvalidaErro(erros));
        }

        return Result.Ok();
    }

    public async Task<Result> LoginAsync(string email, string senha)
    {
        var resultadoLogin = await signInManager.PasswordSignInAsync(
            email,
            senha,
            isPersistent: true,
            lockoutOnFailure: false
        );

        if (!resultadoLogin.Succeeded)
            return Result.Fail("Login ou senha incorretos.");

        return Result.Ok();
    }
}
