using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using ControleDeCinema.Dominio.ModuloSessao;

namespace ControleDeCinema.Dominio.ModuloFilme;

public class Ingresso : EntidadeBase<Ingresso>
{
    public string Titulo { get; set; }
    public int Duracao { get; set; }
    public bool Lancamento { get; set; }
    public ModuloGeneroFilme.Ingresso Genero { get; set; }
    public List<Sessao> Sessoes { get; set; }

    protected Ingresso()
    {
        Sessoes = new List<Sessao>();
    }

    public Ingresso(string titulo, int duracao, bool lancamento, ModuloGeneroFilme.Ingresso genero) : this()
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Duracao = duracao;
        Lancamento = lancamento;
        Genero = genero;
    }

    public override void AtualizarRegistro(Ingresso registroEditado)
    {
        Titulo = registroEditado.Titulo;
        Duracao = registroEditado.Duracao;
        Genero = registroEditado.Genero;
        Lancamento = registroEditado.Lancamento;
    }
}