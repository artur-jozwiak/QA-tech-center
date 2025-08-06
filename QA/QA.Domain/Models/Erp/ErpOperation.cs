namespace QA.Domain.Models.Erp;

//tabela QA_Operacje
public partial class ErpOperation
{
    public string NazwaOp { get; set; } = null!;
    public string SymbolOp { get; set; } = null!;
    public int IdTechnolog { get; set; }
}
