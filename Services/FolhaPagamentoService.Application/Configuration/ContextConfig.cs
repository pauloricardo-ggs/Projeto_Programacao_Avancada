using FolhaPagamentoService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FolhaPagamentoService.Application.Configuration
{
    public static class ContextConfig
    {
        public static IServiceCollection AddContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FolhaPagamentoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FolhaPagamentoContext"));
            });

            return services;
        }
    }
}
