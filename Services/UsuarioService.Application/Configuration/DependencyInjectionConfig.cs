using Microsoft.Extensions.DependencyInjection;
using UsuarioService.Business.Interfaces;
using UsuarioService.Business.Services;

namespace ma9.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
