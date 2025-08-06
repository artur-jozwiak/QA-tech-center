using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IProductService
    {
        void AssignProductsWeights(IEnumerable<Product> products);
        void AssignProductWeight(Product product);
    }
}
