using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Infraestrutura.Orm.ModuloSessao;
using ControleDeCinema.Testes.Integracao.Compartilhado;

namespace ControleDeCinema.Testes.Integracao.ModuloIngresso;

[TestClass]
[TestCategory("Testes de Integração de Ingresso")]
public sealed class RepositorioIngressoEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Selecionar_Ingressos_Filtrando_Por_Usuario()
    {
        // Arrange
        var tenantId = Guid.NewGuid(); // usuário filtrado

        var sessao = new Sessao(
            new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc),
            50,
            new Filme("O Mano", 120, true, new GeneroFilme("Suspense")),
            new Sala(1, 200)
        );

        var ingressoUsuario = new Ingresso(1, false, sessao) { UsuarioId = tenantId };
        var ingressoOutroUsuario = new Ingresso(2, true, sessao) { UsuarioId = Guid.NewGuid() };

        dbContext!.Ingressos.AddRange(ingressoUsuario, ingressoOutroUsuario);
        dbContext.SaveChanges();

        var repositorioIngresso = new RepositorioIngressoEmOrm(dbContext);

        // Act
        var ingressosFiltrados = repositorioIngresso.SelecionarRegistros(tenantId);

        // Assert
        Assert.AreEqual(1, ingressosFiltrados.Count);
        Assert.AreEqual(tenantId, ingressosFiltrados[0].UsuarioId);
        Assert.AreEqual(ingressoUsuario.Id, ingressosFiltrados[0].Id);
    }

    [TestMethod]
    public void Deve_Retornar_Vazio_Se_Nao_Houver_Ingressos_Para_Usuario()
    {
        // Arrange
        var tenantId = Guid.NewGuid();

        var sessao = new Sessao(
            new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc),
            50,
            new Filme("O Mano", 120, true, new GeneroFilme("Suspense")),
            new Sala(1, 200)
        );

        var ingressoOutroUsuario = new Ingresso(2, true, sessao) { UsuarioId = Guid.NewGuid() };
        dbContext!.Ingressos.Add(ingressoOutroUsuario);
        dbContext.SaveChanges();

        var repositorioIngresso = new RepositorioIngressoEmOrm(dbContext);

        // Act
        var ingressosFiltrados = repositorioIngresso.SelecionarRegistros(tenantId);

        // Assert
        Assert.AreEqual(0, ingressosFiltrados.Count);
    }

    [TestMethod]
    public void Deve_Retornar_Varios_Ingressos_Do_Mesmo_Usuario()
    {
        // Arrange
        var tenantId = Guid.NewGuid();

        var sessao1 = new Sessao(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc), 50,
            new Filme("O Mano", 120, true, new GeneroFilme("Suspense")),
            new Sala(1, 200)
        );

        var sessao2 = new Sessao(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc), 60,
            new Filme("O Mano 2", 90, false, new GeneroFilme("Terror")),
            new Sala(2, 150)
        );

        var ingresso1 = new Ingresso(1, false, sessao1) { UsuarioId = tenantId };
        var ingresso2 = new Ingresso(2, true, sessao2) { UsuarioId = tenantId };

        dbContext!.Ingressos.AddRange(ingresso1, ingresso2);
        dbContext.SaveChanges();

        var repositorioIngresso = new RepositorioIngressoEmOrm(dbContext);

        // Act
        var ingressosFiltrados = repositorioIngresso.SelecionarRegistros(tenantId);

        // Assert
        Assert.AreEqual(2, ingressosFiltrados.Count);
        Assert.IsTrue(ingressosFiltrados.All(i => i.UsuarioId == tenantId));
    }
}