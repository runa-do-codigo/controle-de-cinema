using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Infraestrutura.Orm.Compartilhado;

namespace ControleDeCinema.Infraestrutura.Orm.ModuloSessao;

public class RepositorioSessaoEmOrm : RepositorioBaseEmOrm<Sessao>, IRepositorioSessao
{
    public RepositorioSessaoEmOrm(ControleDeCinemaDbContext contexto) : base(contexto) { }
}
