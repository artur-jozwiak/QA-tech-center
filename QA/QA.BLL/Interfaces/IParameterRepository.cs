using Org.BouncyCastle.Asn1;
using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IParameterRepository
    {
        Parameter GetBy(int id);
        Task<Parameter> GetWithMeasurementsBy(int id);
        List<Parameter> GetByOperationId(int operationId);
        void UpdateRange(List<Parameter> parameters);
        Task Complete();
        Task<List<Parameter>> GetParametersWithMeasurementsBy(int orderId, int[] parametersIds );

        //Task<List<Parameter>> GetParametersWithMeasurementsBy(int orderId, int[] parametersIds, string orderKey);

        Task<List<Parameter>> GetParametersWithMeasurementsBy(int[] parametersIds);

        Task<List<Parameter>> GetParametersWithMeasurementsBy(int[] parametersIds, string orderKey);

        Task<List<Parameter>> GetParametersWithMeasurementsBy(int orderId, int operationId);
        Task<List<Parameter>> GetAllBy(int productId);

        //
        Task<List<Parameter>> GetHeightParameters();
    }
}
