using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Saga.Orchestrator.Business.Interfaces;
using Saga.Orchestrator.Business.Manager;
using System.Text;

namespace Saga.Orchestrator.Configuration
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            var key = "50UGhlE8aluUIn5Zh6XNrZEZ5vcHG0tXvMWqq5YSEB4t3yQsQ";
            var expiracaoHoras = 2;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key, expiracaoHoras));

            return services;
        }
    }
}
