using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Infraestrutura.Orm.Compartilhado;
using ControleDeCinema.Infraestrutura.Orm.ModuloFilme;
using ControleDeCinema.Infraestrutura.Orm.ModuloGeneroFilme;
using ControleDeCinema.Infraestrutura.Orm.ModuloSala;
using ControleDeCinema.Infraestrutura.Orm.ModuloSessao;
using DotNet.Testcontainers.Containers;
using FizzWare.NBuilder;
using Testcontainers.PostgreSql;
namespace ControleDeCinema.Testes.Integracao.Compartilhado;

[TestClass]
public abstract class TestFixture
{
    protected ControleDeCinemaDbContext? dbContext;

    protected RepositorioFilmeEmOrm? repositorioFilme;
    protected RepositorioGeneroFilmeEmOrm? repositorioGeneroFilme;
    protected RepositorioSalaEmOrm? repositorioSala;
    protected RepositorioSessaoEmOrm? repositorioSessao;
    protected RepositorioIngressoEmOrm? repositorioIngresso;

    private static IDatabaseContainer? container;

    [AssemblyInitialize]
    public static async Task Setup(TestContext _)
    {
        container = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithName("controle-de-cinema")
            .WithDatabase("CONTROLECINE")
            .WithUsername("postgres")
            .WithPassword("CINEE")
            .WithCleanUp(true)
            .Build();

        await InicializarBancoDadosAsync(container);
    }

    [AssemblyCleanup]
    public static async Task Teardown()
    {
        await EncerrarBancoDadosAsync();
    }

    [TestInitialize]
    public void ConfigurarTestes()
    {
        if (container is null)
            throw new ArgumentNullException("O banco de dados não foi inicializado.");

        dbContext = ControleDeCinemaDbContextFactory.CriarDbContext(container.GetConnectionString());

        ConfigurarTabelas(dbContext);

        repositorioFilme = new RepositorioFilmeEmOrm(dbContext);
        repositorioGeneroFilme = new RepositorioGeneroFilmeEmOrm(dbContext);
        repositorioSala = new RepositorioSalaEmOrm(dbContext);
        repositorioSessao = new RepositorioSessaoEmOrm(dbContext);
        repositorioIngresso = new RepositorioIngressoEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Sala>(repositorioSala.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Sala>>(repositorioSala.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Sessao>(repositorioSessao.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Sessao>>(repositorioSessao.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<GeneroFilme>(repositorioGeneroFilme.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<GeneroFilme>>(repositorioGeneroFilme.CadastrarEntidades);

        BuilderSetup.SetCreatePersistenceMethod<Filme>(repositorioFilme.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Filme>>(repositorioFilme.CadastrarEntidades);
    }

    private static void ConfigurarTabelas(ControleDeCinemaDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        dbContext.Ingressos.RemoveRange(dbContext.Ingressos);
        dbContext.Sessoes.RemoveRange(dbContext.Sessoes);
        dbContext.Salas.RemoveRange(dbContext.Salas);
        dbContext.Filmes.RemoveRange(dbContext.Filmes);
        dbContext.GenerosFilme.RemoveRange(dbContext.GenerosFilme);

        dbContext.SaveChanges();
    }

    private static async Task InicializarBancoDadosAsync(IDatabaseContainer container)
    {
        await container.StartAsync();
    }

    private static async Task EncerrarBancoDadosAsync()
    {
        if (container is null)
            throw new ArgumentNullException("O Banco de dados não foi inicializado.");

        await container.StopAsync();
        await container.DisposeAsync();
    }
}
