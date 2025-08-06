using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace QA.DataAccess.Repositories
{
    public class PressingRepository : IPressingRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Pressing> _dbSet;

        public PressingRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Pressing>();
        }

        public async Task<List<Pressing>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Pressing>> GetAllWhere()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Add(Pressing pressing)
        {
            await _dbSet.AddAsync(pressing);
        }

        public async Task<List<Pressing>> GetBy(int orderId)
        {
            return await _dbSet.Where(p => p.OrderId == orderId).ToListAsync();
        }

        public async Task<List<Pressing>> GetByWithoutOrder(string pdmNo, int orderId)
        {
            return await _dbSet.Where(p => p.PDMNo == pdmNo && p.OrderId != orderId).ToListAsync();
        }

        public void Update(Pressing pressing)
        {
            _dbSet.Update(pressing);
        }

        public void Remove(Pressing pressing)
        {
            _dbSet.Remove(pressing);
        }

        public void RemoveRange(List<Pressing> pressings)
        {
            _dbSet.RemoveRange(pressings);
        }

        public async Task<bool> OrderHasPressingRecords(int orderId)
        {
            return await _dbSet.AnyAsync(p => p.OrderId == orderId);
        }
    }
}
