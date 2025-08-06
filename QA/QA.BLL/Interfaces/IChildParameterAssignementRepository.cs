using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IChildParameterAssignementRepository
    {
       void Add(ChildParametersAssignement childParametersAssignement);
        void SaveChanges();
    }
}

