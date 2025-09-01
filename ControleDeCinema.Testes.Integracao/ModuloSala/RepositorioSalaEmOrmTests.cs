using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Testes.Integracao.Compartilhado;

namespace ControleDeCinema.Testes.Integracao.ModuloSala;

[TestClass]
[TestCategory("Testes de Integração de Sala")]
public sealed class RepositorioSalaEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Sala_Corretamente()
    {
        // Arrange
        var sala = new Sala(001, 200);

        // Act
        repositorioSala?.Cadastrar(sala);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioSala?.SelecionarRegistroPorId(sala.Id);

        Assert.AreEqual(sala, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Sala_Corretamente()
    {
        // Arrange
        var sala = new Sala(001, 200);
        repositorioSala?.Cadastrar(sala);
        dbContext?.SaveChanges();

        var salaEditada = new Sala(002, 150);

        // Act
        var conseguiuEditar = repositorioSala?.Editar(sala.Id, salaEditada);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioSala?.SelecionarRegistroPorId(sala.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual(sala, registroSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Sala_Corretamente()
    {
        // Arrange
        var sala = new Sala(001, 200);
        repositorioSala?.Cadastrar(sala);
        dbContext?.SaveChanges();

        // Act
        var conseguiuExcluir = repositorioSala?.Excluir(sala.Id);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioSala?.SelecionarRegistroPorId(sala.Id);

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(registroSelecionado);
    }

    [TestMethod]
    public void Deve_Selecionar_Sala_Corretamente()
    {
        // Arrange - Arranjo
        var sala = new Sala(001, 200);
        var sala2 = new Sala(002, 150);
        var sala3 = new Sala(003, 220);

        List<Sala> salasEsperados = [sala, sala2, sala3];

        repositorioSala?.CadastrarEntidades(salasEsperados);
        dbContext?.SaveChanges();

        var salaEsperadasOrdenadas = salasEsperados
            .OrderBy(d => d.Numero)
            .ToList();

        // Act - Ação
        var salasRecebidas = repositorioSala?.SelecionarRegistros();

        // Assert - Asseção
        CollectionAssert.AreEqual(salaEsperadasOrdenadas, salasEsperados);
    }
}
