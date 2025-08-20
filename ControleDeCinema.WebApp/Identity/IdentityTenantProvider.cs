using ControleDeCinema.Dominio.ModuloAutenticacao;
using System.Security.Claims;

namespace ControleDeCinema.WebApp.Identity;

public class IdentityTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor contextAccessor;

    public IdentityTenantProvider(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public Guid? UsuarioId
    {
        get
        {
            
            var claimId = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claimId is null)
                return null;

            return Guid.Parse(claimId.Value);
        }
    }
}
