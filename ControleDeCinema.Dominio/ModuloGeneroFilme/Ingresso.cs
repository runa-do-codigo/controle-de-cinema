using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloFilme;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeCinema.Dominio.ModuloGeneroFilme;

public class Ingresso : EntidadeBase<Ingresso>
{
    public string Descricao { get; set; }
    public List<ModuloFilme.Ingresso> Filmes { get; set; }


    [ExcludeFromCodeCoverage]
    protected Ingresso()
    {
        Filmes = new List<ModuloFilme.Ingresso>();
    }

    public Ingresso(string descricao) : this()
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
    }

    public override void AtualizarRegistro(Ingresso registroEditado)
    {
        Descricao = registroEditado.Descricao;
    }
}
