using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FolhaPagamentoService.Application.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            return services;
        }
    }
}
