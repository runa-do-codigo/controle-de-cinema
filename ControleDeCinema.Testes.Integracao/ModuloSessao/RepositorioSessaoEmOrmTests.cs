using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Testes.Integracao.Compartilhado;

namespace ControleDeCinema.Testes.Integracao.ModuloSessao;

[TestClass]
[TestCategory("Testes de Integração de Sessao")]

public sealed class RepositorioSessaoEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Sessao_Corretamente()
    {
        // Arrange
        var sessao = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));

        // Act
        repositorioSessao?.Cadastrar(sessao);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioSessao?.SelecionarRegistroPorId(sessao.Id);

        Assert.AreEqual(sessao, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Sessao_Corretamente()
    {

        // Arrange
        var sessao = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));
        repositorioSessao?.Cadastrar(sessao);
        dbContext?.SaveChanges();

        var sessaoEditada = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano 2", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));

        // Act
        var conseguiuEditar = repositorioSessao?.Editar(sessao.Id, sessaoEditada);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioSessao?.SelecionarRegistroPorId(sessao.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(sessao, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Sessao_Corretamente()
    {
        // Arrange
        var sessao = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));
        repositorioSessao?.Cadastrar(sessao);
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = repositorioSessao?.Excluir(sessao.Id);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioSessao?.SelecionarRegistroPorId(sessao.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Sessoes_Corretamente()
    {

        // Arrange - Arranjo
        var sessao = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));
        var sessao2 = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano 2", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));
        var sessao3 = new Sessao(new DateTime(2000, 1, 1, 12, 12, 0, DateTimeKind.Utc), 50, new Filme("O Mano 3", 129, true, new GeneroFilme("Suspense")), new Sala(001, 200));

        List<Sessao> sessaoEsperados = [sessao, sessao2, sessao3];

        repositorioSessao?.CadastrarEntidades(sessaoEsperados);
        dbContext?.SaveChanges();

        var filmesEsperadasOrdenadas = sessaoEsperados
            .OrderBy(d => d.Inicio)
            .ToList();

        // Act - Ação
        var filmesRecebidas = repositorioSessao?.SelecionarRegistros();

        // Assert - Asseção
        CollectionAssert.AreEquivalent(filmesEsperadasOrdenadas, filmesRecebidas);
    }
}