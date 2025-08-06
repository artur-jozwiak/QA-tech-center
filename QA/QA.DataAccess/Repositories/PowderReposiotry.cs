using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class PowderReposiotry : IPowderRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<PowderSpecification> _dbSet;

        public PowderReposiotry(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<PowderSpecification>();
        }

        public async Task<PowderSpecification?> GetBy(string powderName)
        {
            return await _dbSet.FirstOrDefaultAsync(ps => ps.PowderType == powderName);
        }

        public async Task<List<PowderSpecification>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

    }
}
