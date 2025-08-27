using ControleDeCinema.Testes.Interface.ModuloSessao;
using TesteFacil.Testes.Interface.Compartilhado;

namespace ControleDeCinema.Testes.Interface.ModuloSessao;

[TestClass]
[TestCategory("Tests de Interface de Sessao")]
public sealed class SessaoInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Sessao_Corretamente()
    {
        // Arange
        var indexPageObject = new SessaoIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        // Act
        indexPageObject
            .ClickCadastrar()
            .PreencherNumero(001)
            .PreencherCapacidade(200)
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemSessao(001));
    }

    [TestMethod]
    public void Deve_Editar_Sessao_Corretamente()
    {
        // Arrange
        var indexPageObject = new SessaoIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNumero(001)
            .PreencherCapacidade(200)
            .Confirmar();

        // Act
        indexPageObject
            .ClickEditar()
            .PreencherNumero(002)
            .PreencherCapacidade(150)
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemSessao(002));
    }

    [TestMethod]
    public void Deve_Excluir_Sessao_Corretamente()
    {
        // Arrange
        var indexPageObject = new SessaoIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNumero(001)
            .PreencherCapacidade(200)
            .Confirmar();

        // Act
        indexPageObject
            .ClickExcluir()
            .Confirmar();

        // Assert
        Assert.IsFalse(indexPageObject.ContemSala(001));
    }
}