using ControledeCinema.Dominio.Compartilhado;

namespace ControleDeCinema.Dominio.ModuloGeneroFilme;

public interface IRepositorioGeneroFilme : IRepositorio<GeneroFilme>
{
    List<GeneroFilme> SelecionarRegistrosPorUsuario(Guid usuarioId);
}
