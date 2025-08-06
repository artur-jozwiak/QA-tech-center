
using QA.Domain.Models;
using System.Threading.Tasks;

namespace QA.BLL.Interfaces
{
    public interface IMeasurementRepository
    {
        Task Add(Measurement measurement);
        void Update(Measurement measurement);
        void Remove(Measurement measurement);
        void DeleteMeasurements(List<Measurement> measurements);
        Task<bool> OrderHasMeasurements(int orderId);
        Task<List<Measurement>> GetAllBy(string parameterName, int productId);
        //Task<List<Measurement>> GetAllBy(int parameterId);


        List<Measurement> GetMeasurementsWithoutOrderAssigned();
    }
}
