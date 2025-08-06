

using QA.Domain.Models.Erp;

namespace QA.BLL.Interfaces
{
    public interface IErpOperationRepository
    {
        Task<List<ErpOperation>> GetOperationsByTechId(int IdTechnology);
    }
}
