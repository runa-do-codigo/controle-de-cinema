using ControleDeCinema.Dominio.ModuloAutenticacao;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Infraestrutura.Orm.Compartilhado;
using ControleDeCinema.Testes.Integracao.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Testes.Integracao.ModuloAutenticacao;
public class FakeTenantProvider : ITenantProvider
{
    public Guid? UsuarioId { get; set; }
    public bool IsInRole(string role) => true;
}

[TestClass]
[TestCategory("Testes de Integração de Autenticacao")]
public sealed class RepositorioAutenticacaoEmOrmTests : TestFixture
{

    private DbContextOptions options = new DbContextOptionsBuilder<ControleDeCinemaDbContext>()
        .UseNpgsql("Host=localhost;Port=5432;Database=CONTROLECINE;Username=postgres;Password=CINEE")
        .Options;

    [TestMethod]
    public void Deve_Cadastrar_Filme_Filtrando_Por_Usuario()
    {
        var tenantProvider = new FakeTenantProvider();

        dbContext = new ControleDeCinemaDbContext(options, tenantProvider);
        dbContext.Database.EnsureCreated();

        var usuario = new Usuario { UserName = "teste", Email = "teste@email.com" };

        // Act
        dbContext.Users.Add(usuario);
        dbContext.SaveChanges();

        // Assert
        var usuarioRegistrado = dbContext.Users.FirstOrDefault(u => u.Email == "teste@email.com");
        Assert.IsNotNull(usuarioRegistrado);
        Assert.AreEqual(usuario.UserName, usuarioRegistrado!.UserName);
    }

    [TestMethod]
    public void Deve_Excluir_Usuario_Corretamente()
    {
        var tenantProvider = new FakeTenantProvider();
        dbContext = new ControleDeCinemaDbContext(options, tenantProvider);
        dbContext.Database.EnsureCreated();

        var usuario = new Usuario { UserName = "teste2", Email = "teste2@email.com" };
        dbContext.Users.Add(usuario);
        dbContext.SaveChanges();

        // Act
        dbContext.Users.Remove(usuario);
        dbContext.SaveChanges();

        // Assert
        var usuarioExcluido = dbContext.Users.FirstOrDefault(u => u.Email == "teste2@email.com");
        Assert.IsNull(usuarioExcluido);
    }

    [TestMethod]
    public void Deve_Atualizar_Usuario_Corretamente()
    {
        var tenantProvider = new FakeTenantProvider();
        dbContext = new ControleDeCinemaDbContext(options, tenantProvider);
        dbContext.Database.EnsureCreated();

        var usuario = new Usuario { UserName = "teste3", Email = "teste3@email.com" };
        dbContext.Users.Add(usuario);
        dbContext.SaveChanges();

        // Act
        usuario.UserName = "teste3Atualizado";
        dbContext.Users.Update(usuario);
        dbContext.SaveChanges();

        // Assert
        var usuarioAtualizado = dbContext.Users.FirstOrDefault(u => u.Email == "teste3@email.com");
        Assert.IsNotNull(usuarioAtualizado);
        Assert.AreEqual("teste3Atualizado", usuarioAtualizado!.UserName);
    }

    [TestMethod]
    public void Deve_Selecionar_Usuario_Por_Id()
    {
        var tenantProvider = new FakeTenantProvider();
        dbContext = new ControleDeCinemaDbContext(options, tenantProvider);
        dbContext.Database.EnsureCreated();

        var usuario = new Usuario { UserName = "teste4", Email = "teste4@email.com" };
        dbContext.Users.Add(usuario);
        dbContext.SaveChanges();

        // Act
        var usuarioSelecionado = dbContext.Users.Find(usuario.Id);

        // Assert
        Assert.IsNotNull(usuarioSelecionado);
        Assert.AreEqual(usuario.Email, usuarioSelecionado!.Email);
    }
}