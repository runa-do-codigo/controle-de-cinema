using ControleDeCinema.Aplicacao.ModuloSessao;
using ControleDeCinema.Dominio.ModuloAutenticacao;
using ControleDeCinema.Dominio.ModuloSessao;
using Microsoft.Extensions.Logging;
using Moq;

[TestClass]
[TestCategory("Testes de Unidade de Ingresso")]
public class IngressoAppServiceTests
{
    private Mock<ITenantProvider>? tenantProviderMock;
    private Mock<IRepositorioIngresso>? repositorioIngressoMock;
    private Mock<ILogger<IngressoAppService>>? loggerMock;

    private IngressoAppService? imgressoAppService;

    [TestInitialize]
    public void Setup()
    {
        tenantProviderMock = new Mock<ITenantProvider>();
        repositorioIngressoMock = new Mock<IRepositorioIngresso>();
        loggerMock = new Mock<ILogger<IngressoAppService>>();

        imgressoAppService = new IngressoAppService(
            tenantProviderMock.Object,
            repositorioIngressoMock.Object,
            loggerMock.Object
        );
    }

    [TestMethod]
    public void SelecionarTodos_Deve_Retornar_Lista_De_Ingressos()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        tenantProviderMock?.Setup(t => t.UsuarioId).Returns(usuarioId);

        var ingressos = new List<Ingresso>
        {
            new Ingresso(1, false, new Sessao(DateTime.UtcNow, 50, null!, null!)) { UsuarioId = usuarioId },
            new Ingresso(2, true, new Sessao(DateTime.UtcNow, 60, null!, null!)) { UsuarioId = usuarioId }
        };

        repositorioIngressoMock?
            .Setup(r => r.SelecionarRegistros(usuarioId))
            .Returns(ingressos);

        // Act
        var resultado = imgressoAppService?.SelecionarTodos();

        // Assert
        Assert.IsTrue(resultado?.IsSuccess);
        Assert.AreEqual(2, resultado?.Value.Count);
        Assert.AreEqual(usuarioId, resultado?.Value[0].UsuarioId);
        Assert.AreEqual(usuarioId, resultado?.Value[1].UsuarioId);
    }

    [TestMethod]
    public void SelecionarTodos_Deve_Retornar_Falha_Quando_Excecao_For_Lancada()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        tenantProviderMock?.Setup(t => t.UsuarioId).Returns(usuarioId);

        repositorioIngressoMock?
            .Setup(r => r.SelecionarRegistros(usuarioId))
            .Throws(new Exception("Erro inesperado"));

        // Act
        var resultado = imgressoAppService?.SelecionarTodos();

        // Assert
        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);

        var mensagemErro = resultado.Errors.First().Message;
        Assert.AreEqual("Ocorreu um erro interno do servidor", mensagemErro);
    }
}