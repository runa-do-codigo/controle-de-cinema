using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Aplicacao.ModuloFilme;
using ControleDeCinema.Aplicacao.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloAutenticacao;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using Microsoft.Extensions.Logging;
using Moq;

namespace ControleDeCinema.Testes.Unidade.ModuloFilme;

[TestClass]
[TestCategory("Testes de Unidade de Filme")]
public sealed class FilmeAppServiceTests
{
    private Mock<ITenantProvider>? tenantProviderMock;
    private Mock<IRepositorioGeneroFilme>? repositorioGeneroFilmeMock;
    private Mock<IRepositorioFilme>? repositorioFilmeMock;
    private Mock<IUnitOfWork>? unitOfWorkMock;
    private Mock<ILogger<FilmeAppService>>? loggerMock;

    private FilmeAppService? filmeAppService;

    [TestInitialize]
    public void Setup()
    {
        tenantProviderMock = new Mock<ITenantProvider>();
        repositorioGeneroFilmeMock = new Mock<IRepositorioGeneroFilme>();
        unitOfWorkMock = new Mock<IUnitOfWork>();
        loggerMock = new Mock<ILogger<FilmeAppService>>();
        filmeAppService = new FilmeAppService(
            tenantProviderMock.Object,
            repositorioFilmeMock.Object,
            unitOfWorkMock.Object,
            loggerMock.Object);
    }

    [TestMethod]
    public void Cadastrar_DeveRetornarOk_QuandoFilmeForValida()
    {
        // Arrange
        var filme = new Filme("O Mano", 129, true, new GeneroFilme("Susupense"));

        var filmeTeste = new Filme("Teste", 129, true, new GeneroFilme("Susupense"));

        repositorioFilmeMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Filme>() { filmeTeste });

        // Act
        var resultado = filmeAppService?.Cadastrar(filme);

        // Assert
        repositorioFilmeMock?.Verify(r => r.Cadastrar(filme), Times.Once);

        unitOfWorkMock?.Verify(u => u.Commit(), Times.Once);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsSuccess);
    }

    [TestMethod]
    public void Cadastrar_DeveRetornarFalha_QuandoFilmeForDuplicada()
    {
        // Arrange
        var filme = new Filme("O Mano", 129, true, new GeneroFilme("Susupense"));

        var filmeTeste = new Filme("O Mano", 129, true, new GeneroFilme("Susupense"));

        repositorioFilmeMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Filme>() { filmeTeste });

        // Act
        var resultado = filmeAppService?.Cadastrar(filme);

        // Assert
        repositorioFilmeMock?.Verify(r => r.Cadastrar(filme), Times.Never);

        unitOfWorkMock?.Verify(u => u.Commit(), Times.Never);

        Assert.IsNotNull(resultado);
        Assert.IsTrue(resultado.IsFailed);
    }

    [TestMethod]
    public void Cadastrar_DeveRetornarFalha_QuandoExcecaoForLancada()
    {
        // Arrange
        var filme = new Filme("O Mano", 129, true, new GeneroFilme("Susupense"));

        repositorioFilmeMock?
            .Setup(r => r.SelecionarRegistros())
            .Returns(new List<Filme>());

        unitOfWorkMock?
            .Setup(r => r.Commit())
            .Throws(new Exception("Erro inesperado"));

        // Act
        var resultado = filmeAppService?.Cadastrar(filme);

        // Assert
        unitOfWorkMock?.Verify(u => u.Rollback(), Times.Once);

        Assert.IsNotNull(resultado);

        var mensagemErro = resultado.Errors.First().Message;

        Assert.AreEqual("Ocorreu um erro interno do servidor", mensagemErro);

        Assert.IsTrue(resultado.IsFailed);
    }
}