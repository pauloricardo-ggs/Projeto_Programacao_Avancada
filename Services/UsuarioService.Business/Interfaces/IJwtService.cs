using UsuarioService.Business.Models;

namespace UsuarioService.Business.Interfaces
{
    public interface IJwtService
    {
        string GerarJwt(Usuario usuario);
    }
}