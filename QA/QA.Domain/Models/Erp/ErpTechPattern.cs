
namespace QA.Domain.Models.Erp;

//tabela QA_Technologie_Wzorcowe
public partial class ErpTechPattern
{
    public int Id { get; set; }

    public string SymbolTec { get; set; } = null!;

    public string Nazwa { get; set; } = null!;

    public string SymbolWyr { get; set; } = null!;
}
