using QA.Domain.Models;
using QA.Domain.Models.Erp;

namespace QA.BLL.Interfaces
{
    public interface IOperationService
    {
        bool IsPressingOperation(Operation operation);
        bool IsSandblastingOperation(Operation operation);
        bool IsLaboratoryOperation(Operation operation);
        bool IsGindingTopAndBottomOperation(Operation operation);

        IEnumerable<string> ReturnMissingOperationsIfExist(List<ErpOperation> erpOperations, List<Operation> operations);
        void CreateOperationDetailsIfNotExist(Operation operation);
    }
}
