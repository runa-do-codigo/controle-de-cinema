using ControledeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloGeneroFilme;
using Microsoft.EntityFrameworkCore;

namespace ControleDeCinema.Infraestrutura.Orm.Compartilhado;

public class ControleDeCinemaDbContext : DbContext, IUnitOfWork
{
    public DbSet<GeneroFilme> GenerosFilme { get; set; }

    public ControleDeCinemaDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(ControleDeCinemaDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }

    public void Commit()
    {
        SaveChanges();
    }

    public void Rollback()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }
    }
}
