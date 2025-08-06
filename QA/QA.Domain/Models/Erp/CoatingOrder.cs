
namespace QA.Domain.Models.Erp
{
    // widok Q_nrSpieku QA_ZleceniaPowlekania
    public class CoatingOrder
    {
        public int ErpId { get; set; }
        public string KluczZp { get; set; } = null!;
        public string KluczZpSkr { get; set; } = null!;

        public decimal MeNrprpo { get; set; }

        public DateTime Data { get; set; }
    }
}
