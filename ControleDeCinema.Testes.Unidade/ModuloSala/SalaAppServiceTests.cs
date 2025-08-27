using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Aplicacao.ModuloGeneroFilme;
using ControleDeCinema.Aplicacao.ModuloSala;
using ControleDeCinema.Dominio.ModuloAutenticacao;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;
using Microsoft.Extensions.Logging;
using Moq;

namespace ControleDeCinema.Testes.Unidade.ModuloSala;

[TestClass]
[TestCategory("Testes de Unidade de Sala")]
public sealed class SalaAppServiceTests
{
    private Mock<ITenantProvider>? tenantProviderMock;
    private Mock<IRepositorioSala>? repositorioSalaMock;
    private Mock<IUnitOfWork>? unitOfWorkMock;
    private Mock<ILogger<SalaAppService>>? loggerMock;

    private SalaAppService? salaAppService;

    [TestInitialize]
    public void Setup()
    {
        tenantProviderMock = new Mock<ITenantProvider>();
        repositorioSalaMock = new Mock<IRepositorioSala>();
        unitOfWorkMock = new Mock<IUnitOfWork>();
        loggerMock = new Mock<ILogger<SalaAppService>>();
        salaAppService = new SalaAppService(
            tenantProviderMock.Object,
            repositorioSalaMock.Object,
            unitOfWorkMock.Object,
            loggerMock.Object);
    }

    [TestMethod]
    public void Cadastrar_Deve_Retornar_Ok_Quando_Sala_For_Valida()
    {
        // Arrange
        var sala = new Sala(001, 200);

        var salaTeste = new Sala(002, 150);

        repositorioSalaMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Sala>() { salaTeste });

        // Act
        var resultado = salaAppService?.Cadastrar(sala);

        // Assert
        repositorioSalaMock?.Verify(r => r.Cadastrar(sala), Times.Once);

        unitOfWorkMock?.Verify(u => u.Commit(), Times.Once);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public void Cadastrar_Deve_Retornar_Falha_Quando_Sala_For_Duplicado()
    {
        // Arrange
        var sala = new Sala(001, 200);

        var salaTeste = new Sala(001, 200);

        repositorioSalaMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Sala>() { salaTeste });

        // Act
        var resultado = salaAppService?.Cadastrar(sala);

        // Assert
        repositorioSalaMock?.Verify(r => r.Cadastrar(sala), Times.Never);

        unitOfWorkMock?.Verify(u => u.Commit(), Times.Never);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);
    }

    [TestMethod]
    public void Cadastrar_Deve_Retornar_Falha_Quando_Excecao_For_Lancada()
    {
        // Arrange
        var sala = new Sala(001, 200);

        repositorioSalaMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Sala>());

        unitOfWorkMock?
            .Setup(r => r.Commit())
            .Throws(new Exception("Erro inesperado"));

        // Act
        var resultado = salaAppService?.Cadastrar(sala);

        // Assert
        unitOfWorkMock?.Verify(u => u.Rollback(), Times.Once);

        Assert.IsNotNull(resultado);

        var mensagemErro = resultado.Errors.First().Message;

        Assert.AreEqual("Ocorreu um erro interno do servidor", mensagemErro);

        Assert.IsTrue(resultado.IsFailed);
    }
}
