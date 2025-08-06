
using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IOperationRepository
    {
        Task<Operation> GetMasterControlOperationWithParameters(int laboratoryMasterOperationId);
        Task<Operation> GetMasterControlOperationWithMeasurements(int laboratoryMasterOperationId);
        Task<Operation> GetLaboratoryOperationBy(int productId);
    }
}
