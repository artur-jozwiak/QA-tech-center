using QA.Domain.Models.Erp;

namespace QA.BLL.Interfaces
{
    public interface IErpOrderRepository
    {
        Task<ErpOrder> GetBy(int erpId);
        Task<List<ErpOrder>> GetAll();
        Task<List<string>?> GetOrdersKeyBy(string productSymbol);


        //05.05.25
        List<string> GetOrderdCoatingProcesses(int erpId);
    }
}
