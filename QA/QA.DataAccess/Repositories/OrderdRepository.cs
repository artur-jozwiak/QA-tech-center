using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class OrderdRepository : IOrderRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Order> _dbSet;

        public OrderdRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Order>();
        }

        public async Task Add(Order order)
        {
            try
            {
                await _dbSet.AddAsync(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> OrderExist(string orderKey)
        {
            try
            {
                return await _dbSet.AnyAsync(o => o.OrderKey == orderKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> GetAllAsyn()
        {
            return await _dbSet.Include(o => o.Product).OrderByDescending(o => o.RowDatetime).ToListAsync();
        }

        public async Task<List<Order>> GetAllWithoutArchivalAsync()
        {
            var excludedDate = new DateTime(1900, 1, 1, 0, 0, 0);
            return await _dbSet
                .Include(o => o.Product)
                .Where(o => o.RowDatetime != excludedDate)
                .OrderByDescending(o => o.RowDatetime)
                .ToListAsync();
        }

        public async Task<Order> GetByAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.Id == id);
        }

        public  Order GetBy(int id)
        {
            return  _dbSet.Include(o => o.Product).FirstOrDefault(o => o.Id == id);
        }

        public Order GetByErpId(int erpId)
        {
            return _dbSet.Include(o => o.Product).FirstOrDefault(o => o.HermesId == erpId);
        }

        public async Task<Order> GetBy(string orderKey)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.OrderKey == orderKey);
        }

        public async Task<Order> GetByShortenedKey(string shortenedKey)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.ShortenedKey.StartsWith(shortenedKey));
        }

        public async Task<Order> GetBlankOrderByShortenedKey(string shortenedKey)
        {
            // return await _dbSet.FirstOrDefaultAsync(o => o.ShortenedKey.StartsWith(shortenedKey) && o.OrderKey.StartsWith("ZPR/2I"));
            // 11.06.25
            return await _dbSet.FirstOrDefaultAsync(o => o.ShortenedKey.StartsWith(shortenedKey) && (o.OrderKey.StartsWith("ZPR/2I") || o.OrderKey.StartsWith("ZPR/2T")));
        }

        public async Task<Order> GetForSinteringPlan(string shortenedKey)
        {
            return await _dbSet.Include(o => o.Product)
                               .FirstOrDefaultAsync(o => o.ShortenedKey
                               .StartsWith(shortenedKey));
        }

        public int? GetIdBy(string orderKey, string pdmNo)
        {
            Order? order = _dbSet.FirstOrDefault(o => o.ShortenedKey.StartsWith(orderKey) && o.Product.PdmNo == pdmNo);

            if (order == null)
            {
                return null;
            }
            return order.Id;
        }

        public async Task<Order> GetWithAllNavPropertiesBy(int id)
        {
            return await _dbSet.Include(o => o.Product.Operations)
                   .Include(o => o.Product.Image)
                   .Include(o => o.VisualInspectionForm)
                        .ThenInclude(vis => vis.Samples)
                            .ThenInclude(s => s.SampleDefects)
                   .Include(o => o.VisualInspectionForm)
                        .ThenInclude(vis => vis.Markers).ThenInclude(m => m.Image)
                   .Include(o => o.VisualInspectionForm)
                         .ThenInclude(vis => vis.Image)
                   .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> GetWithAllNavPropertiesBy(string orderKey)
        {
            return await _dbSet.Include(o => o.Product.Operations)
                               .Include(o => o.Product.Image)
                               .Include(o => o.VisualInspectionForm)
                                    .ThenInclude(vis => vis.Samples)
                                        .ThenInclude(s => s.SampleDefects)
                               .Include(o => o.VisualInspectionForm)
                                    .ThenInclude(vis => vis.Markers)
                               .Include(o => o.VisualInspectionForm)
                                    .ThenInclude(vis => vis.Image)
                                .FirstOrDefaultAsync(o => o.OrderKey.Trim() == orderKey.Trim());
        }

        public async Task<Order> GetSinteringOrder(string orderKey)
        {
            return await _dbSet.Include(o => o.Product)
                                                 .ThenInclude(p => p.Operations.Where(o => o.Name == "Kontrola SMS/HC"))
                                                     .ThenInclude(op => op.Parameters)
                                                         .ThenInclude(m => m.Measurements)
                                                             .ThenInclude(m => m.MeasurementsSeries)
                                                 .FirstOrDefaultAsync(o => o.OrderKey == orderKey);
        }

        public async Task<Order> GetWithProductAndVisAsync(int id)
        {
            return await _dbSet.Include(o => o.Product)
                               .Include(o => o.VisualInspectionForm)
                                    .ThenInclude(vis => vis.Image)
                               .Include(o => o.VisualInspectionForm)
                                    .ThenInclude(vis => vis.Markers)
                                        .ThenInclude(m => m.Image)
                               .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Image GetVISImage(int orderId)
        {
            return _dbSet.Include(o => o.VisualInspectionForm).ThenInclude(vis => vis.Image).FirstOrDefault(o => o.Id == orderId).VisualInspectionForm.Image;
        }


        public bool IsTrialOrder(int orderId)
        {
            return _dbSet.FirstOrDefault(o => o.Id == orderId).ShortenedKey.StartsWith("TA");
        }

        public List<Order> GetProductOrders(int productId)
        {
            return _dbSet.Include(o => o.Pressings).Where(o => o.ProductId == productId).ToList();
        }
    }
}
