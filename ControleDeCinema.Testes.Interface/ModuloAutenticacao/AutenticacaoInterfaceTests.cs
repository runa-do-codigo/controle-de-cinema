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
            .PreencherSenha("gustavIado12345!")
            .PreencherConfirmarSenha("gustavIado12345!")
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
            .PreencherSenha("SenhaForce123!")
            .PreencherConfirmarSenha("SenhaForce123!")
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
            .PreencherSenha("SenhaForce123!")
            .PreencherConfirmarSenha("SenhaForce123!")
            .SelecionarTipoDeUsuario("Cliente")
            .Confirmar();

        autenticacaoIndex
             .FazerLogout(enderecoBase);

        // Act
        AutenticacaoFormPageObject autenticacaoForm = autenticacaoIndex
             .IrParaLogin(enderecoBase);

        autenticacaoForm
            .PreencherEmail("iagustavo@gmail.com")
            .PreencherSenha("SenhaForce123!")
            .Confirmar();

        // Assert
        Assert.IsTrue(autenticacaoIndex.EstaLogado());
    }
}
