using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSessao;
using TesteFacil.Testes.Integracao.Compartilhado;


namespace ControleDeCinema.Testes.Integracao.ModuloIngresso;

[TestClass]
[TestCategory("Testes de Integração de Ingresso")]
public sealed class RepositorioIngressoEmOrmTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Ingresso_Corretamente()
    {
        
        // Arrange
        var ingresso = new Ingresso("O Mano");

        // Act
        repositorioIngresso?.Cadastrar(ingresso);
        dbContext?.SaveChanges();

        // Assert
        var registroSelecionado = repositorioIngresso?.SelecionarRegistroPorId(ingresso.Id);

        Assert.AreEqual(ingresso, registroSelecionado);
    }
}