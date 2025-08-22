using TesteFacil.Testes.Interface.Compartilhado;

namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

[TestClass]
[TestCategory("Tests de Interface de Genero de Filme")]
public sealed class GeneroFilmeInterfaceTests : TestFixture
{
    [TestMethod]
    public void Deve_Cadastrar_Genero_Filme_Corretamente()
    {
        // Arange
        var indexPageObject = new GeneroFilmeIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        // Act
        indexPageObject
            .ClickCadastrar()
            .PreencherNome("Suspense")
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemGeneroFilme("Suspense"));
    }

    [TestMethod]
    public void Deve_Editar_Genero_Filme_Corretamente()
    {
        // Arrange
        var indexPageObject = new GeneroFilmeIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNome("Suspense")
            .Confirmar();

        // Act
        indexPageObject
            .ClickEditar()
            .PreencherNome("Suspense Editada")
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemGeneroFilme("Suspense Editada"));
    }

    [TestMethod]
    public void Deve_Excluir_Genero_Filme_Corretamente()
    {
        // Arrange
        var indexPageObject = new GeneroFilmeIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNome("Suspense")
            .Confirmar();

        // Act
        indexPageObject
            .ClickExcluir()
            .Confirmar();

        // Assert
        Assert.IsFalse(indexPageObject.ContemGeneroFilme("Suspense"));
    }
}