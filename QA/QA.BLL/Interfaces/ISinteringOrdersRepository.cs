using QA.Domain.Models.Erp;

namespace QA.BLL.Interfaces
{
    public interface ISinteringOrdersRepository
    {
        Task<List<string>> GetOrderKeysBy(int sinteringNo);
        Task<decimal?> GetSineringNoByAsync(string orderKey);

        //decimal? GetSineringNoBy(string shortenedKey);
        Task<SinteringOrder?> GetSineringOrderBy(string shortenedKey);

        List<SinteringReport> GetBy(int sinteringNo);
        List<GreenStock> GetGreensStocks();
        //bool SinteringReportExist(int sinteringNo);
    }
}
