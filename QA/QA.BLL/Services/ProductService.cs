using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AssignProductsWeights(IEnumerable<Product> products)
        {
            //Bład w przypadku braku produktu - trzeba pobrać zlecenie z tym produktem
            if (products != null && products.Any(p => p != null && p.Weight == 0))
            {
                foreach (var product in products)
                {
                    if (product != null)
                        AssignProductWeight(product);
                }
                _unitOfWork.Complete();
            }
        }

        public void AssignProductWeight(Product product)
        {
            if (product.Weight == 0)
            {
                var orders = _unitOfWork.Order.GetProductOrders(product.Id);
                var pressings = orders.SelectMany(o => o.Pressings);
                var weights = new List<decimal>();

                if (pressings.Any(p => p.Weight != null))
                {
                    weights = pressings
                            .Where(p => p.Weight.HasValue)
                            .Select(p => p.Weight.Value)
                            .ToList();
                }

                if (weights.Count() != 0)
                {
                    product.Weight = Math.Round(weights.Average(), 3);
                }
            }

            int count = _unitOfWork.Complete();
        }
    }
}
