using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infraestrutura.Orm.ModuloSessao;

public class RepositorioSessaoEmOrm : RepositorioBaseEmOrm<Sessao>, IRepositorioSessao
{
    public RepositorioSessaoEmOrm(ControleDeCinemaDbContext contexto) : base(contexto) { }

    public override Sessao? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(x => x.Filme)
            .Include(x => x.Sala)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Sessao> SelecionarRegistros()
    {
        return registros
            .Include(x => x.Filme)
            .Include(x => x.Sala)
            .ToList();
    }
}
