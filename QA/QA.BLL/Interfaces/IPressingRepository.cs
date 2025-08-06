using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IPressingRepository
    {
        Task<List<Pressing>> GetAll();
        Task Add(Pressing pressing);
        Task<List<Pressing>> GetBy(int orderId);
        Task<List<Pressing>> GetByWithoutOrder(string pdmNo, int orderId);
        void Update(Pressing pressing);
        void Remove(Pressing pressing);
        void RemoveRange(List<Pressing> pressings);
        Task<bool> OrderHasPressingRecords(int orderId);

        //List<Pressing> GetDuplicatePressings();
    }
}
