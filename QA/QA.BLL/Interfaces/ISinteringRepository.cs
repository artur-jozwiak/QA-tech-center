using QA.Domain.Models;
using QA.Domain.Models.SinteringModels;

namespace QA.BLL.Interfaces
{
    public interface ISinteringRepository
    {
        SinteringBatch? GetSinteringBatchBy(int? id);
        void AddTrayLocations(List<TrayLocation> trayLocations);
        void AddTrayLocation(TrayLocation trayLocation);
        void AddIfNotExist(int sinteringNo);
        SinteringBatch GetSinteringBy(int sinteringNo);
        void SeedFurnaceLocalization();
        Task<List<FurnaceLocalization>> GetAllLocations();
        List<SinteringBatch> GetAllProcesses();
        void AddReange(List<TrayLocation> sinteringLocalizations);
        Task<List<TrayLocation>> GetBy(int orderId);
        void Remove(TrayLocation trayLocation);
        TrayLocation GetTrayLocationBy(int sinteringId, int furnaceLocalizationId);
        List<Order> GetAdditionalSinteringOrders(int sinteringId, List<Order> orders);
        void RemoveSinteringTrayLocations(int sinteringId);
        List<TrayLocation> GetTrayLocationsBy(int sinteringId);
    }
}
