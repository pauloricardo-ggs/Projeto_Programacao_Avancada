using FolhaPagamentoService.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FolhaPagamentoService.Data.Mappings
{
    class FolhaPagamentoMapping : IEntityTypeConfiguration<FolhaPagamento>
    {
        public void Configure(EntityTypeBuilder<FolhaPagamento> builder)
        {
            builder.HasKey(folha => folha.Id);

            builder.Property(folha => folha.FuncionarioId)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(folha => folha.SalarioBruto)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(folha => folha.SalarioLiquido)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(folha => folha.DescontoInss)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(folha => folha.DescontoIrrf)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(folha => folha.DescontoPlanoSaude)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("FolhasPagamento");
        }
    }
}
