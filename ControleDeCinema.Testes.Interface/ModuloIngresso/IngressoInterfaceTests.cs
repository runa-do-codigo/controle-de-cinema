//using ControleDeCinema.Testes.Interface.Compartilhado;

//namespace ControleDeCinema.Testes.Interface.ModuloIngresso;

//[TestClass]
//[TestCategory("Testes de Interface de Ingresso")]
//public sealed class IngressoInterfaceTests : TestFixture
//{
//    [TestMethod]
//    public void Deve_Cadastrar_Ingresso_Corretamente()
//    {
//        var indexPageObject = new IngressoIndexPageObject(driver!)
//            .IrPara(enderecoBase!);

//        indexPageObject
//            .ClickCadastrar()
//            .PreencherNumero(1)
//            .SelecionarSessao("Sessao 1")
//            .MeioPagamento(true)
//            .Confirmar();

//        Assert.IsTrue(indexPageObject.ContemIngresso("1"));
//    }

//    [TestMethod]
//    public void Deve_Editar_Ingresso_Corretamente()
//    {
//        var indexPageObject = new IngressoIndexPageObject(driver!)
//            .IrPara(enderecoBase!);

//        indexPageObject
//            .ClickCadastrar()
//            .PreencherNumero(1)
//            .SelecionarSessao("Sessao 1")
//            .MeioPagamento(true)
//            .Confirmar();

//        indexPageObject
//            .ClickEditar()
//            .PreencherNumero(2)
//            .SelecionarSessao("Sessao 2")
//            .MeioPagamento(false)
//            .Confirmar();

//        Assert.IsTrue(indexPageObject.ContemIngresso("2"));
//    }

//    [TestMethod]
//    public void Deve_Excluir_Ingresso_Corretamente()
//    {
//        var indexPageObject = new IngressoIndexPageObject(driver!)
//            .IrPara(enderecoBase!);

//        indexPageObject
//            .ClickCadastrar()
//            .PreencherNumero(1)
//            .SelecionarSessao("Sessao 1")
//            .MeioPagamento(true)
//            .Confirmar();

//        indexPageObject
//            .ClickExcluir()
//            .Confirmar();

//        Assert.IsFalse(indexPageObject.ContemIngresso("1"));
//    }
//}