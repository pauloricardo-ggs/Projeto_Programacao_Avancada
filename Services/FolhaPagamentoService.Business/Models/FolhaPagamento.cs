using System;

namespace FolhaPagamentoService.Business.Models
{
    public class FolhaPagamento
    {
        public string Id { get; set; }
        public string FuncionarioId { get; set; }
        public decimal SalarioBruto { get; set; }
        public decimal SalarioLiquido { get; set; }
        public decimal DescontoInss { get; set; }
        public decimal DescontoIrrf { get; set; }
        public decimal DescontoPlanoSaude { get; set; }

        public FolhaPagamento() { }

        public FolhaPagamento(string id, string funcionarioId, decimal salarioBruto)
        {
            Id = id;
            FuncionarioId = funcionarioId;
            SalarioBruto = salarioBruto;
            DescontoPlanoSaude = 125;
            DescontoInss = CalcularInss(salarioBruto);
            DescontoIrrf = CalcularIrrf(salarioBruto);
            SalarioLiquido = SalarioBruto -
                             DescontoPlanoSaude -
                             DescontoInss -
                             DescontoIrrf;
        }

        private decimal CalcularInss(decimal salarioBruto)
        {
            if (salarioBruto <= (decimal)1830.29) return (decimal)0.08 * salarioBruto;
            if ((decimal)1830.30 <= salarioBruto && salarioBruto <= (decimal)3050.52) return (decimal)0.09 * salarioBruto;
            if ((decimal)3050.53 <= salarioBruto && salarioBruto <= (decimal)6101.06) return (decimal)0.11 * salarioBruto;
            if ((decimal)6101.07 <= salarioBruto) return (decimal)671.12;
            throw new ArgumentOutOfRangeException();
        }

        private decimal CalcularIrrf(decimal salarioBruto)
        {
            if (salarioBruto <= (decimal)1903.98) return 0;
            if ((decimal)1903.99 <= salarioBruto && salarioBruto <= (decimal)2826.66) return (decimal)0.075 * salarioBruto;
            if ((decimal)2826.67 <= salarioBruto && salarioBruto <= (decimal)3751.05) return (decimal)0.15 * salarioBruto;
            if ((decimal)3751.06 <= salarioBruto && salarioBruto <= (decimal)4664.68) return (decimal)0.22 * salarioBruto;
            if ((decimal)4664.69 <= salarioBruto) return (decimal)0.275 * salarioBruto;
            throw new ArgumentOutOfRangeException();
        }
    }
}
