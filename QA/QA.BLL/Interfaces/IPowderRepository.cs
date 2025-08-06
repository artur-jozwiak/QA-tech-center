using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IPowderRepository
    {
         Task<PowderSpecification?> GetBy(string powderName);
         Task<List<PowderSpecification>> GetAll();
    }
}
