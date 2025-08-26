using Moq;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloFilme;
using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Aplicacao.ModuloGeneroFilme;
using Microsoft.Extensions.Logging;
using ControleDeCinema.Dominio.ModuloAutenticacao;

namespace ControleDeCinema.Testes.Unidade.ModuloGeneroFilme;

[TestClass]
[TestCategory("Testes de Unidade de Genero de Filme")]
public sealed class GeneroFilmeAppServiceTests
{
    private Mock<ITenantProvider>? tenantProviderMock;
    private Mock<IRepositorioGeneroFilme>? repositorioGeneroFilmeMock;
    private Mock<IRepositorioFilme>? repositorioFilmeMock;
    private Mock<IUnitOfWork>? unitOfWorkMock;
    private Mock<ILogger<GeneroFilmeAppService>>? loggerMock;

    private GeneroFilmeAppService? generoFilmeAppService;

    [TestInitialize]
    public void Setup()
    {
        tenantProviderMock = new Mock<ITenantProvider>();
        repositorioGeneroFilmeMock = new Mock<IRepositorioGeneroFilme>();
        unitOfWorkMock = new Mock<IUnitOfWork>();
        loggerMock = new Mock<ILogger<GeneroFilmeAppService>>();
        generoFilmeAppService = new GeneroFilmeAppService(
            tenantProviderMock.Object,
            repositorioGeneroFilmeMock.Object,
            unitOfWorkMock.Object,
            loggerMock.Object);
    }

    [TestMethod]
    public void Cadastrar_Deve_Retornar_Ok_Quando_Genero_Filme_For_Valida()
    {
        // Arrange
        var generoFilme = new GeneroFilme("Suspense");

        var generoFilmeTeste = new GeneroFilme("Teste");

        repositorioGeneroFilmeMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<GeneroFilme>() { generoFilmeTeste });

        // Act
        var resultado = generoFilmeAppService?.Cadastrar(generoFilme);

        // Assert
        repositorioGeneroFilmeMock?.Verify(r => r.Cadastrar(generoFilme), Times.Once);

        unitOfWorkMock?.Verify(u => u.Commit(), Times.Once);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public void Cadastrar_Deve_Retornar_Falha_Quando_Genero_Filme_For_Duplicado()
    {
        // Arrange
        var generoFilme = new GeneroFilme("Suspense");

        var generoFilmeTeste = new GeneroFilme("Suspense");

        repositorioGeneroFilmeMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<GeneroFilme>() { generoFilmeTeste });

        // Act
        var resultado = generoFilmeAppService?.Cadastrar(generoFilme);

        // Assert
        repositorioGeneroFilmeMock?.Verify(r => r.Cadastrar(generoFilme), Times.Never);

        unitOfWorkMock?.Verify(u => u.Commit(), Times.Never);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);
    }

    [TestMethod]
    public void Cadastrar_Deve_Retornar_Falha_Quando_Excecao_For_Lancada()
    {
        // Arrange
        var generoFilme = new GeneroFilme("Suspense");

        repositorioGeneroFilmeMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<GeneroFilme>());

        unitOfWorkMock?
            .Setup(r => r.Commit())
            .Throws(new Exception("Erro inesperado"));

        // Act
        var resultado = generoFilmeAppService?.Cadastrar(generoFilme);

        // Assert
        unitOfWorkMock?.Verify(u => u.Rollback(), Times.Once);

        Assert.IsNotNull(resultado);

        var mensagemErro = resultado.Errors.First().Message;

        Assert.AreEqual("Ocorreu um erro interno do servidor", mensagemErro);

        Assert.IsTrue(resultado.IsFailed);
    }
}