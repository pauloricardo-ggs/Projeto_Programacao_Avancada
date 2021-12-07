using FolhaPagamentoService.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FolhaPagamentoService.Data.Context
{
    public class FolhaPagamentoContext : DbContext
    {
        public FolhaPagamentoContext(DbContextOptions<FolhaPagamentoContext> options) : base(options) { }

        public DbSet<FolhaPagamento> FolhasPagamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = MapearPropriedadesEsquecidas(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FolhaPagamentoContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            return sucesso;
        }

        private ModelBuilder MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(entity => entity.GetProperties().Where(property => property.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
            return modelBuilder;
        }
    }
}
