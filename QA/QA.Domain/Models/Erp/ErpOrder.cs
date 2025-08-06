namespace QA.Domain.Models.Erp
{
    //tabela QA_Zlecenia
    public class ErpOrder
    {
        public int Id { get; set; }

        public string KluczZp { get; set; } = null!;

        public string? KluczSkrocony { get; set; }

        public string SymbolWyr { get; set; } = null!;

        public string? NazwaArt { get; set; }

        public string QasymbWnd { get; set; } = null!;

        public decimal Ilosc { get; set; }

        public int IdTech { get; set; }
        public string? SymbolProszku { get; set; }
        public string? NrPartiiProszku { get; set; }

    }
}
