using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<int> MigrateErpOrders();
        Task ChangeStatusIfHasMeasurements(IEnumerable<Order> waitingOrders);

        Task AssignOrderToPressing();
    }
}
