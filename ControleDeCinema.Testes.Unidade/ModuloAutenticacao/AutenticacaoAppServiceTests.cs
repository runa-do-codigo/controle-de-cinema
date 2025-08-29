using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Aplicacao.ModuloAutenticacao;
using ControleDeCinema.Aplicacao.ModuloFilme;
using ControleDeCinema.Dominio.ModuloAutenticacao;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace ControleDeCinema.Testes.Unidade.ModuloAutenticacao;

[TestClass]
[TestCategory("Testes de Unidade de Autenticacao")]
public sealed class AutenticacaoAppServiceTests
{
    private Mock<UserManager<Usuario>>? userManager;
    private Mock<SignInManager<Usuario>>? signInManager;
    private Mock<RoleManager<Cargo>>? roleManager;

    private AutenticacaoAppService? autenticacaoAppService;

    [TestInitialize]
    public void Setup()
    {
        userManager = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>().Object, null, null, null, null, null, null, null, null);
        signInManager = new Mock<SignInManager<Usuario>>(userManager, Mock.Of<IHttpContextAccessor>(), new Mock<IUserClaimsPrincipalFactory<Usuario>>(), null, null, null);
        roleManager = new Mock<RoleManager<Cargo>>(new Mock<IRoleStore<Cargo>>().Object, null, null, null, null);

        autenticacaoAppService = new AutenticacaoAppService(
            userManager.Object,
            signInManager.Object,
            roleManager.Object);
    }


}