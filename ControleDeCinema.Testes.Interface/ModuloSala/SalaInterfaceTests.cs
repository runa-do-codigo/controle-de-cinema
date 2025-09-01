//using ControleDeCinema.Testes.Interface.Compartilhado;

//namespace ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

//[TestClass]
//[TestCategory("Tests de Interface de Sala")]
//public sealed class SalaInterfaceTests : TestFixture
//{
//    [TestMethod]
//    public void Deve_Cadastrar_Sala_Corretamente()
//    {
//        // Arange
//        var indexPageObject = new SalaIndexPageObject(driver!)
//            .IrPara(enderecoBase!);

//        // Act
//        indexPageObject
//            .ClickCadastrar()
//            .PreencherNumero(001)
//            .PreencherCapacidade(200)
//            .Confirmar();

//        // Assert
//        Assert.IsTrue(indexPageObject.ContemSala(001));
//    }

//    [TestMethod]
//    public void Deve_Editar_Sala_Corretamente()
//    {
//        // Arrange
//        var indexPageObject = new SalaIndexPageObject(driver!)
//            .IrPara(enderecoBase!);

//        indexPageObject
//            .ClickCadastrar()
//            .PreencherNumero(001)
//            .PreencherCapacidade(200)
//            .Confirmar();

//        // Act
//        indexPageObject
//            .ClickEditar()
//            .PreencherNumero(002)
//            .PreencherCapacidade(150)
//            .Confirmar();

//        // Assert
//        Assert.IsTrue(indexPageObject.ContemSala(002));
//    }

//    [TestMethod]
//    public void Deve_Excluir_Sala_Corretamente()
//    {
//        // Arrange
//        var indexPageObject = new SalaIndexPageObject(driver!)
//            .IrPara(enderecoBase!);

//        indexPageObject
//            .ClickCadastrar()
//            .PreencherNumero(001)
//            .PreencherCapacidade(200)
//            .Confirmar();

//        // Act
//        indexPageObject
//            .ClickExcluir()
//            .Confirmar();

//        // Assert
//        Assert.IsFalse(indexPageObject.ContemSala(001));
//    }
//}