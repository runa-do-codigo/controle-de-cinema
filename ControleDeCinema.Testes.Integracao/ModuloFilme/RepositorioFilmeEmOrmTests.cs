using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using TesteFacil.Testes.Integracao.Compartilhado;

namespace ControleDeCinema.Testes.Integracao.ModuloFilme;

[TestClass]
[TestCategory("Testes de Integração de Filme")]
public sealed class RepositorioFilmeEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Filme_Corretamente()
    {
        var generoFilme = new GeneroFilme("Suspense");
        // Arrange
        var filme = new Filme("O Mano", 129, true, generoFilme);

        // Act
        repositorioFilme?.Cadastrar(filme);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioFilme?.SelecionarRegistroPorId(filme.Id);

        Assert.AreEqual(filme, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Filme_Corretamente()
    {
        var generoFilme = new GeneroFilme("Suspense");
        // Arrange
        var filme = new Filme("O Mano", 129, true, generoFilme);
        repositorioFilme?.Cadastrar(filme);
        dbContext?.SaveChanges();

        var filmeEditada = new Filme("O Mano", 109, false, generoFilme);

        // Act
        var conseguiuEditar = repositorioFilme?.Editar(filme.Id, filmeEditada);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioFilme?.SelecionarRegistroPorId(filme.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(filme, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Filme_Corretamente()
    {
        var generoFilme = new GeneroFilme("Suspense");
        // Arrange
        var filme = new Filme("O Mano", 129, true, generoFilme);
        repositorioFilme?.Cadastrar(filme);
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = repositorioFilme?.Excluir(filme.Id);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioFilme?.SelecionarRegistroPorId(filme.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Filmes_Corretamente()
    {
        var generoFilme = new GeneroFilme("Suspense");
        // Arrange - Arranjo
        var filme = new Filme("O Mano", 129, false, generoFilme);
        var filme2 = new Filme("O Mano 2", 1008, false, generoFilme);
        var filme3 = new Filme("O Mano 3", 159, true, generoFilme);

        List<Filme> filmesEsperados = [filme, filme2, filme3];

        repositorioFilme?.CadastrarEntidades(filmesEsperados);
        dbContext?.SaveChanges();

        var filmesEsperadasOrdenadas = filmesEsperados
            .OrderBy(d => d.Titulo)
            .ToList();

        // Act - Ação
        var filmesRecebidas = repositorioFilme?.SelecionarRegistros();

        // Assert - Asseção
        CollectionAssert.AreEqual(filmesEsperadasOrdenadas, filmesRecebidas);
    }
}