using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface ILaboratoryRepository
    {
        Task<string?> ReadMeasurementsForOrder(Order order, string? trial, List<Parameter> parameters, string user);
        Task<string?> ReadMeasurementsForMaster(string sinteringNo, string user);
        Task<string?> ReadMeasurementsForOrder(string orderKey, int productId);
    }
}
