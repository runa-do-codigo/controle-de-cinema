using ControleDeCinema.Dominio.ModuloGeneroFilme;
using TesteFacil.Testes.Integracao.Compartilhado;

namespace ControleDeCinema.Testes.Integracao.ModuloSessao;

[TestClass]
[TestCategory("Testes de Integração de Genero de Filme")]
public sealed class RepositorioGeneroFilmeEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Genero_Filme_Corretamente()
    {
        // Arrange
        var generoFilme = new GeneroFilme("Suspense");

        // Act
        repositorioGeneroFilme?.Cadastrar(generoFilme);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioGeneroFilme?.SelecionarRegistroPorId(generoFilme.Id);

        Assert.AreEqual(generoFilme, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Genero_Filme_Corretamente()
    {
        // Arrange
        var generoFilme = new GeneroFilme("Suspense");
        repositorioGeneroFilme?.Cadastrar(generoFilme);
        dbContext?.SaveChanges();

        var generoFilmeEditada = new GeneroFilme("Terror");

        // Act
        var conseguiuEditar = repositorioGeneroFilme?.Editar(generoFilme.Id, generoFilmeEditada);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioGeneroFilme?.SelecionarRegistroPorId(generoFilme.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(generoFilme, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Genero_Filme_Corretamente()
    {
        // Arrange
        var generoFilme = new GeneroFilme("Suspense");
        repositorioGeneroFilme?.Cadastrar(generoFilme);
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = repositorioGeneroFilme?.Excluir(generoFilme.Id);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioGeneroFilme?.SelecionarRegistroPorId(generoFilme.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Generos_Filme_Corretamente()
    {
        // Arrange - Arranjo
        var genero = new GeneroFilme("Suspense");
        var genero2 = new GeneroFilme("Terror");
        var genero3 = new GeneroFilme("Ficção");

        List<GeneroFilme> generoFilmesEsperados = [genero, genero2, genero3];

        repositorioGeneroFilme?.CadastrarEntidades(generoFilmesEsperados);
        dbContext?.SaveChanges();

        var generoEsperadasOrdenadas = generoFilmesEsperados
            .OrderBy(d => d.Descricao)
            .ToList();

        // Act - Ação
        var generosRecebidas = repositorioGeneroFilme?.SelecionarRegistros();

        // Assert - Asseção
        CollectionAssert.AreEqual(generoEsperadasOrdenadas, generosRecebidas);
    }
}