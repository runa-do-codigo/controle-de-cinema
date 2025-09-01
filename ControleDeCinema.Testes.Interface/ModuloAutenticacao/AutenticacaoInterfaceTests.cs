using ControleDeCinema.Testes.Interface.Compartilhado;
using ControleDeCinema.Testes.Interface.ModuloGeneroFilme;

namespace ControleDeCinema.Testes.Interface.ModuloAutenticacao;

[TestClass]
[TestCategory("Testes de Interface de Autenticacao")]
public sealed class AutenticacaoInterfaceTests : TestFixture
{
    
    [TestMethod]
    public void Deve_Cadastrar_Empresa_Corretamente()
    {
        // Arrange
        var autenticacaoIndex = new AutenticacaoIndexPageObject(driver!)
             .IrPara(enderecoBase!);

        // Act
        autenticacaoIndex
            .ClickCadastrar()
            .PreencherEmail("iagustavo@gmail.com")
            .PreencherSenha("gustaviago12345")
            .PreencherConfirmarSenha("gustaviago12345")
            .SelecionarTipoDeUsuario("Empresa")
            .Confirmar();

        // Assert

        Assert.IsTrue(autenticacaoIndex.EstaLogado());
    }

    [TestMethod]
    public void Deve_Cadastrar_Cliente_Corretamente()
    {
        // Arrange
        var autenticacaoIndex = new AutenticacaoIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        // Act
        autenticacaoIndex
            .ClickCadastrar()
            .PreencherEmail("iagustavo@hotmail.com")
            .PreencherSenha("senhaforte")
            .PreencherConfirmarSenha("senhaforte")
            .SelecionarTipoDeUsuario("Cliente")
            .Confirmar();

        // Assert
        Assert.IsTrue(autenticacaoIndex.EstaLogado());
    }
    [TestMethod]
    public void Deve_Realizar_Login_Corretamente()
    {
        // Arrange
        AutenticacaoIndexPageObject autenticacaoIndex = new(driver);
        autenticacaoIndex
            .IrParaRegistro(enderecoBase)
            .PreencherEmail("iagustavo@gmail.com")
            .PreencherSenha("senhaforte")
            .PreencherConfirmarSenha("senhaPadrao")
            .SelecionarTipoDeUsuario("Empresa")
            .Confirmar();

        autenticacaoIndex
             .FazerLogout(enderecoBase);

        // Act
        AutenticacaoFormPageObject autenticacaoForm = autenticacaoIndex
             .IrParaLogin(enderecoBase);

        autenticacaoForm
            .PreencherEmail("senhaforte")
            .PreencherSenha("senhaforte")
            .Confirmar();

        // Assert
        Assert.IsTrue(autenticacaoIndex.EstaLogado());
    }
}
