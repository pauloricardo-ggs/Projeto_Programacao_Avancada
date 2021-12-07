using Microsoft.EntityFrameworkCore.Migrations;

namespace FolhaPagamentoService.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolhasPagamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    FuncionarioId = table.Column<string>(type: "varchar(100)", nullable: false),
                    SalarioBruto = table.Column<string>(type: "varchar(50)", nullable: false),
                    SalarioLiquido = table.Column<string>(type: "varchar(50)", nullable: false),
                    DescontoInss = table.Column<string>(type: "varchar(50)", nullable: false),
                    DescontoIrrf = table.Column<string>(type: "varchar(50)", nullable: false),
                    DescontoPlanoSaude = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolhasPagamento", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolhasPagamento");
        }
    }
}
