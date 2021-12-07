using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Saga.Orchestrator.Business.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        public Task<string> GerarToken(string email, UserManager<IdentityUser> userManager);
    }
}
