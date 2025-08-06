using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Product>();
        }

        public async Task<bool> ProductExist(string productSymbol)
        {
            try
            {
                return await _dbSet.AnyAsync(p => p.Symbol == productSymbol);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductWitchOrdersBySymbol(string productSymbol)
        {
            try
            {
                return await _dbSet.Include(p => p.Orders).Include(p => p.Operations).FirstOrDefaultAsync(p => p.Symbol == productSymbol);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> GetWBProducts()
        {
            return await _dbSet.Where(p => p.Symbol.StartsWith("W-B"))
                               .OrderBy(p => p.Description)
                               .ToListAsync();
        }

        public  List<Product> GetWBProductsWithMeasyrements()
        {
            //return  _dbSet.Include(p => p.Orders).ThenInclude(o => o.Measurements).ThenInclude(m => m.Parameter).Where(p => p.Symbol.StartsWith("W-B"))
            //                   .OrderBy(p => p.Description)
            //                   .ToList();

            //return _dbSet.Include(p => p.Operations).ThenInclude(o => o.Parameters).ThenInclude(m => m.Measurements).Where(p => p.Symbol.StartsWith("W-B"))
            //                   .OrderBy(p => p.Description)
            //                   .ToList();

            return _dbSet.Include(p => p.Orders).ThenInclude(o => o.Pressings).Where(p => p.Symbol.StartsWith("W-B")).ToList();

        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbSet.Where(p => p.IsPattern == false).OrderBy(p => p.Description).ToListAsync();
        }
    }
}
