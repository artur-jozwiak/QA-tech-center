using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Operation> _dbSet;

        public OperationRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Operation>();
        }

        public async Task<Operation> GetMasterControlOperationWithParameters(int laboratoryMasterOperationId) 
        {
           return await _dbSet.Include(o => o.Parameters)
                              .FirstOrDefaultAsync(o => o.Id == laboratoryMasterOperationId);
        }

        public async Task<Operation> GetMasterControlOperationWithMeasurements(int laboratoryMasterOperationId)
        {
            return await _dbSet.Include(o => o.Product)
                               .Include(o => o.Parameters)
                               .ThenInclude(p => p.Measurements)
                                     .ThenInclude(m => m.MeasurementsSeries)
                               .FirstOrDefaultAsync(o => o.Id == laboratoryMasterOperationId);
        }

        public async Task<Operation> GetLaboratoryOperationBy(int productId)
        {
            var labOperation = await _dbSet.Include(o => o.Parameters)
                                                .ThenInclude(p => p.Measurements)
                                           .Where(o => o.ProductId == productId)
                                           .FirstOrDefaultAsync(o => o.Name.Contains("Kontrola SMS/HC"));
            return labOperation;
        }
    }
}
