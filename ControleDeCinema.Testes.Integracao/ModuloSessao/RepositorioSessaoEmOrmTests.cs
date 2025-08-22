using Microsoft.Extensions.Configuration;
using ControleDeCinema.Infraestrutura.Orm.Compartilhado;
using ControleDeCinema.Infraestrutura.Orm.ModuloSessao;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Testes.Integracao.ModuloSessao;

[TestClass]
[TestCategory("Testes de Integração de Sessao")]

public sealed class RepositorioSessaoEmOrmTests
{
    private ControleDeCinemaDbContext dbContext;
    private RepositorioSessaoEmOrm repositorioSessao;
    
    [TestMethod]
    public void Deve_Cadastrar_Sessao_Corretamente()
    {
        var configuracao = new ConfigurationBuilder().Build();

        var connectionString = configuracao["SQL_CONNECTION_STRING"];

        var options = new DbContextOptionsBuilder<ControleDeCinemaDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        dbContext = new ControleDeCinemaDbContext(options);

        repositorioSessao = new RepositorioSessaoEmOrm(dbContext);
    }
}
