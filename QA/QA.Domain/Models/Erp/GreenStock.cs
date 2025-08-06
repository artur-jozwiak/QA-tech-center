namespace QA.Domain.Models.Erp;

public partial class GreenStock
{
    // widok Q_nrSpieku QA_MagazynGreen
    public int ErpOrderId { get; set; }

    public string KluczZp { get; set; } = null!;

    public string SymbolArt { get; set; } = null!;

    public decimal? Zapas { get; set; }
}
