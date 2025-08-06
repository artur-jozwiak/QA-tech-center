using QA.Domain.Models;


namespace QA.BLL.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> ProductExist(string productSymbol);
        Task<List<Product>> GetWBProducts();
        Task<List<Product>> GetAll();
        Task<Product> GetProductWitchOrdersBySymbol(string productSymbol);

        List<Product> GetWBProductsWithMeasyrements();
    }
}
