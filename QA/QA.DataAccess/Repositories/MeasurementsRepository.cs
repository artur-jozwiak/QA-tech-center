using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;
using System.Runtime.CompilerServices;

namespace QA.DataAccess.Repositories
{
    public class MeasurementsRepository : IMeasurementRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Measurement> _dbSet;

        public MeasurementsRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Measurement>();
        }

        public async Task Add(Measurement measurement)
        {
            await _dbSet.AddAsync(measurement);
        }

        public void Update(Measurement measurement)
        {
            _dbSet.Update(measurement);
        }

        public void Remove(Measurement measurement)
        {
            _dbSet.Remove(measurement);
        }

        public async Task<bool> OrderHasMeasurements(int orderId)
        {
            return await _dbSet.AnyAsync(m => m.OrderId == orderId);
        }

        public void DeleteMeasurements(List<Measurement> measurements)
        {
            _dbSet.RemoveRange(measurements);
        }

        public async Task<List<Measurement>> GetAllBy(string parameterName, int productId)
        {
            return await _dbSet.Where(m => m.Parameter.Name == parameterName && m.Parameter.Operation.Name == "Kontrola SMS/HC" && m.Parameter.Operation.ProductId == productId)
                               .Include(m => m.Parameter)
                               .OrderBy(m => m.Date)
                               .ToListAsync();
        }

        public List<Measurement> GetMeasurementsWithoutOrderAssigned()
        {
            var measurementsWithoutOrders = _dbSet.Where(m => m.OrderId == null).ToList();

            return measurementsWithoutOrders.Where(m => !m.OrderKey.StartsWith("M")).ToList();
        }

        //public async Task<List<Measurement>> GetAllBy(int parameterId)
        //{
        //  return await  _dbSet.Include(m => m.Parameter).Where(m => m.ParameterId == parameterId).ToListAsync();
        //}
    }
}
