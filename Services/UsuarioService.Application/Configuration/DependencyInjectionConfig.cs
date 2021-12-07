using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using UsuarioService.Business.Interfaces;
using UsuarioService.Business.Notificacoes;

namespace UsuarioService.Application.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
