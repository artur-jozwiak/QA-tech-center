using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<bool> OrderExist(string orderKey);
        Task<List<Order>> GetAllAsyn();
        Task<List<Order>> GetAllWithoutArchivalAsync();
        Task<Order> GetByAsync(int id);
        Order GetBy(int id);
        Order GetByErpId(int erpId);
        Task<Order> GetBy(string orderKey);
        Task<Order> GetByShortenedKey(string shortenedKey);

        Task<Order> GetBlankOrderByShortenedKey(string shortenedKey);
        int? GetIdBy(string orderKey, string pdmNo);
        Task<Order> GetWithAllNavPropertiesBy(int id);
        Task<Order> GetWithAllNavPropertiesBy(string orderKey);
        Task<Order> GetSinteringOrder(string orderKey);
        Task<Order> GetWithProductAndVisAsync(int id);

        List<Order> GetProductOrders(int productId);
        //
        //Order GetWithProductAndVis(int id);
        //

        Image GetVISImage(int orderId);

        bool IsTrialOrder(int orderId);

    }
}
