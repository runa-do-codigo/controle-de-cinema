using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;
using ControleDeCinema.Dominio.ModuloSessao;
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
            .PreencherNumeroIngressos(200)
            .PreencherEncerrada(true)
            .SelecionarFilme(new Filme("O Mano", 129, false, new GeneroFilme("Suspense")))
            .SelecionarSala(new Sala(1, 100))
            .PreencherDataHora(new DateTime(2025, 08, 20, 14, 30, 00))
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemSessao(new Guid()));
    }

    [TestMethod]
    public void Deve_Editar_Sessao_Corretamente()
    {
        // Arrange
        var indexPageObject = new SessaoIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNumeroIngressos(200)
            .PreencherEncerrada(true)
            .SelecionarFilme(new Filme("O Mano", 129, false, new GeneroFilme("Suspense")))
            .SelecionarSala(new Sala(1, 100))
            .PreencherDataHora(new DateTime(2025, 08, 20, 14, 30, 00))
            .Confirmar();

        // Act
        indexPageObject
            .ClickEditar()
            .PreencherNumeroIngressos(200)
            .PreencherEncerrada(true)
            .SelecionarFilme(new Filme("O Mano 2", 109, true, new GeneroFilme("Terror")))
            .SelecionarSala(new Sala(2, 150))
            .PreencherDataHora(new DateTime(2025, 08, 22, 14, 30, 00))
            .Confirmar();

        // Assert
        Assert.IsTrue(indexPageObject.ContemSessao(new Guid()));
    }

    [TestMethod]
    public void Deve_Excluir_Sessao_Corretamente()
    {
        // Arrange
        var indexPageObject = new SessaoIndexPageObject(driver!)
            .IrPara(enderecoBase!);

        indexPageObject
            .ClickCadastrar()
            .PreencherNumeroIngressos(200)
            .PreencherEncerrada(true)
            .SelecionarFilme(new Filme("O Mano", 129, false, new GeneroFilme("Suspense")))
            .SelecionarSala(new Sala(1, 100))
            .PreencherDataHora(new DateTime(2025, 08, 20, 14, 30, 00))
            .Confirmar();

        // Act
        indexPageObject
            .ClickExcluir()
            .Confirmar();

        // Assert
        Assert.IsFalse(indexPageObject.ContemSessao(new Guid()));
    }
}