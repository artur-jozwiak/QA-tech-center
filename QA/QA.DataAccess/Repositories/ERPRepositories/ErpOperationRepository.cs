using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models.Erp;


namespace QA.DataAccess.Repositories.ERPRepositories
{
    public class ErpOperationRepository : IErpOperationRepository
    {
        private readonly ERPContext _context;
        private readonly DbSet<ErpOperation> _dbSet;

        public ErpOperationRepository(ERPContext context)
        {
            _context = context;
            _dbSet = context.Set<ErpOperation>();
        }

        public async Task<List<ErpOperation>> GetOperationsByTechId(int IdTechnology)
        {
            return await _dbSet.Where(o => o.IdTechnolog == IdTechnology).ToListAsync();
        }
    }
}
