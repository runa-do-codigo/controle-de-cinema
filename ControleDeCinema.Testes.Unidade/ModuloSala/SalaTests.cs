using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSala;

namespace ControleDeCinema.Testes.Unidade.ModuloSala;

[TestClass]
[TestCategory("Testes de Unidade de Sala")]
public sealed class SalaTests
{
    private Sala? sala;

    [TestMethod]
    public void DeveCriarSalaFilme()
    {
        // Arrange
        int numero = 001;
        int capacidade = 200;
        // Act
        sala = new Sala(numero, capacidade);
        // Assert
        Assert.IsNotNull(sala);
        Assert.AreEqual(numero, sala.Numero);
        Assert.AreEqual(capacidade, sala.Capacidade);
        Assert.AreNotEqual(Guid.Empty, sala.Id);
    }

    [TestMethod]
    public void DeveAtualizarSalaFilme()
    {
        // Arrange
        int numero = 001;
        int capacidade = 200;

        int numeroAtualizado = 002;
        int capacidadeAtualizado = 150;
        sala = new Sala(numero, capacidade);
        var salaEditado = new Sala(numeroAtualizado, capacidadeAtualizado) { Id = sala.Id };
        // Act
        sala.AtualizarRegistro(salaEditado);
        // Assert
        Assert.AreEqual(numeroAtualizado, sala.Numero);
        Assert.AreEqual(capacidadeAtualizado, sala.Capacidade);
    }
}