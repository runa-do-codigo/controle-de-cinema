using TesteFacil.Testes.Interface.Compartilhado;

namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

[TestClass]
[TestCategory("Tests de Interface de Filme")]
public sealed class FilmeInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Filme_Corretamente()
    {
        // Arange
        var indexPageObject = new FilmeIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        // Act
        indexPageObject
            .ClickCadastrar()
            .PreencherNome("O Mano")
            .PreencherDuracao(129)  
            .PreencherLancamento(true)
            .SelecionarGenero("Suspense")
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemFilme("O Mano"));
    }

    [TestMethod]
    public void Deve_Editar_Filme_Corretamente()
    {
        // Arrange
        var indexPageObject = new FilmeIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNome("O Mano")
            .PreencherDuracao(129)  
            .PreencherLancamento(true)
            .SelecionarGenero("Suspense")
            .Confirmar();

        // Act
        indexPageObject
            .ClickEditar()
            .PreencherNome("O Mano Editada")
            .PreencherDuracao(109)  
            .PreencherLancamento(false)
            .SelecionarGenero("Terror")
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemFilme("O Mano Editada"));
    }

    [TestMethod]
    public void Deve_Excluir_Filme_Corretamente()
    {
        // Arrange
        var indexPageObject = new FilmeIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNome("O Mano")
            .PreencherDuracao(129)  
            .PreencherLancamento(true)
            .SelecionarGenero("Suspense")
            .Confirmar();

        // Act
        indexPageObject
            .ClickExcluir()
            .Confirmar();

        // Assert
        Assert.IsFalse(indexPageObject.ContemFilme("O Mano"));
    }
}