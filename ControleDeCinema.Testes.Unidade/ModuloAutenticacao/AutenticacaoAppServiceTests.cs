
using ControleDeCinema.Aplicacao.ModuloAutenticacao;
using ControleDeCinema.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Testing.Platform.Logging;
using Moq;
namespace ControleDeCinema.Testes.Unidade.ModuloAutenticacao;

[TestClass]
[TestCategory("Testes de Unidade de Autenticacao")]
public sealed class AutenticacaoAppServiceTests
{
    private Mock<UserManager<Usuario>>? userManager;
    private Mock<SignInManager<Usuario>>? signManager;
    private Mock<RoleManager<Cargo>>? roleManager;

    private AutenticacaoAppService? autenticacaoAppService;

    [TestInitialize]
    public void Setup()
    {
        userManager = new Mock<UserManager<Usuario>>(
            Mock.Of<IUserStore<Usuario>>(),
            null, null, null, null, null, null, null, null
        );

        signManager = new Mock<SignInManager<Usuario>>(
            userManager,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<Usuario>>(),
            Mock.Of<IOptions<IdentityOptions>>(),
            Mock.Of<ILogger<SignInManager<Usuario>>>(),
            Mock.Of<IAuthenticationSchemeProvider>(),
            Mock.Of<IUserConfirmation<Usuario>>()
        );

        roleManager = new Mock<RoleManager<Cargo>>(
            Mock.Of<IRoleStore<Cargo>>(),
            null, null, null, null
        );

        autenticacaoAppService = new AutenticacaoAppService(
            userManager.Object,
            signManager.Object,
            roleManager.Object
        );
    }

    [TestMethod]
    public async Task Registrar_Deve_Retornar_Ok_Quando_Usuario_For_Valido()
    {
        // Arrange
        var usuario = new Usuario { UserName = "teste", Email = "teste@email.com" };
        var senha = "Senha@123";

        userManager?
            .Setup(u => u.CreateAsync(usuario, senha))
            .ReturnsAsync(IdentityResult.Success);

        roleManager?
            .Setup(r => r.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((Cargo?)null);

        userManager?
            .Setup(u => u.AddToRoleAsync(usuario, It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        signManager?
            .Setup(s => s.PasswordSignInAsync(usuario.Email, senha, true, false))
            .ReturnsAsync(SignInResult.Success);

        // Act
        var resultado = await autenticacaoAppService!.RegistrarAsync(usuario, senha, TipoUsuario.Cliente);

        // Assert
        userManager?.Verify(u => u.CreateAsync(usuario, senha), Times.Once);
        userManager?.Verify(u => u.AddToRoleAsync(usuario, TipoUsuario.Cliente.ToString()), Times.Once);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public async Task Registrar_Deve_Retornar_Falha_Quando_Email_For_Duplicado()
    {
        // Arrange
        var usuario = new Usuario { UserName = "teste", Email = "teste@email.com" };

        userManager?
            .Setup(u => u.CreateAsync(usuario, "123"))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "DuplicateEmail" }));

        // Act
        var resultado = await autenticacaoAppService!.RegistrarAsync(usuario, "123", TipoUsuario.Cliente);

        // Assert
        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);

        var mensagem = resultado.Errors.First().Message;
        Assert.AreEqual("Já existe um usuário com esse e-mail.", mensagem);
    }

    [TestMethod]
    public async Task Login_Deve_Retornar_Ok_Quando_Credenciais_Forem_Validas()
    {
        // Arrange
        signManager?
            .Setup(s => s.PasswordSignInAsync("teste@email.com", "123", true, false))
            .ReturnsAsync(SignInResult.Success);

        // Act
        var resultado = await autenticacaoAppService!.LoginAsync("teste@email.com", "123");

        // Assert
        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public async Task Login_Deve_Retornar_Falha_Quando_Senha_For_Incorreta()
    {
        // Arrange
        signManager?
            .Setup(s => s.PasswordSignInAsync("teste@email.com", "123", true, false))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var resultado = await autenticacaoAppService!.LoginAsync("teste@email.com", "123");

        // Assert
        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);

        var mensagem = resultado.Errors.First().Message;
        Assert.AreEqual("Login ou senha incorretos.", mensagem);
    }

    [TestMethod]
    public async Task Logout_Deve_Chamar_SignOutAsync()
    {
        // Arrange
        signManager?
            .Setup(s => s.SignOutAsync())
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        var resultado = await autenticacaoAppService!.LogoutAsync();

        // Assert
        signManager?.Verify(s => s.SignOutAsync(), Times.Once);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }
}