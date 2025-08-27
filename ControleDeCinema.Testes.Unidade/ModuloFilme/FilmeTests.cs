using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGeneroFilme;

namespace ControleDeCinema.Testes.Unidade.ModuloFilme;

[TestClass]
[TestCategory("Testes de Unidade de FIlme")]
public sealed class FilmeTests
{
    private Filme? filme;

    [TestMethod]
    public void DeveCriarGeneroFilme()
    {
        // Arrange
        string titulo = "O Mano";
        int duracao = 129;
        bool lancamento = true;
        var generoFilme = new GeneroFilme("Susupense");
        // Act
        filme = new Filme(titulo, duracao, lancamento, generoFilme);
        // Assert
        Assert.IsNotNull(filme);
        Assert.AreEqual(titulo, filme.Titulo);
        Assert.AreEqual(duracao, filme.Duracao);
        Assert.AreEqual(lancamento, filme.Lancamento);
        Assert.AreEqual(generoFilme, filme.Genero);
        Assert.AreNotEqual(Guid.Empty, generoFilme.Id);
    }

    [TestMethod]
    public void DeveAtualizarFilme()
    {
        // Arrange
        string titulo = "O Mano";
        int duracao = 129;
        bool lancamento = true;
        var generoFilme = new GeneroFilme("Susupense");

        string tituloAtualizado = "O Mano 2";
        int duracaoAtualizado = 109;
        bool lancamentoAtualizado = false;
        var generoFilmeAtualizado = new GeneroFilme("Terror");

        var filme = new Filme(titulo, duracao, lancamento, generoFilme);
        var filmeEditado = new Filme(tituloAtualizado, duracaoAtualizado, lancamentoAtualizado, generoFilmeAtualizado) { Id = filme.Id };
        // Act
        filme.AtualizarRegistro(filmeEditado);
        // Assert
        Assert.AreEqual(tituloAtualizado, filme.Titulo);
        Assert.AreEqual(duracaoAtualizado, filme.Duracao);
        Assert.AreEqual(lancamentoAtualizado, filme.Lancamento);
        Assert.AreEqual(generoFilmeAtualizado, filme.Genero);
    }
}