using ControleDeCinema.Dominio.ModuloGeneroFilme;

namespace ControleDeCinema.Testes.Unidade.ModuloGeneroFilme;

[TestClass]
[TestCategory("Testes de Unidade de Genero de Filme")]
public sealed class GeneroFilmeTests
{
    private GeneroFilme? generoFilme;

    [TestMethod]
    public void DeveCriarGeneroFilme()
    {
        // Arrange
        string descricao = "Ação";
        // Act
        generoFilme = new GeneroFilme(descricao);
        // Assert
        Assert.IsNotNull(generoFilme);
        Assert.AreEqual(descricao, generoFilme.Descricao);
        Assert.AreNotEqual(Guid.Empty, generoFilme.Id);
    }

    [TestMethod]
    public void DeveAtualizarGeneroFilme()
    {
        // Arrange
        string descricaoInicial = "Ação";
        string descricaoAtualizada = "Comédia";
        generoFilme = new GeneroFilme(descricaoInicial);
        var generoFilmeEditado = new GeneroFilme(descricaoAtualizada) { Id = generoFilme.Id };
        // Act
        generoFilme.AtualizarRegistro(generoFilmeEditado);
        // Assert
        Assert.AreEqual(descricaoAtualizada, generoFilme.Descricao);
    }

    [TestMethod]
    public void DeveTerListaDeFilmesInicializada()
    {
        // Arrange & Act
        generoFilme = new GeneroFilme("Drama");
        // Assert
        Assert.IsNotNull(generoFilme.Filmes);
        Assert.AreEqual(0, generoFilme.Filmes.Count);
    }
}