using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class MeasurementSeriesRepository : IMeasurementSeriesRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<MeasurementsSeries> _dbSet;

        public MeasurementSeriesRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<MeasurementsSeries>();
        }

        public void Delete(MeasurementsSeries measurementsSeries)
        {
            _dbSet.Remove(measurementsSeries);
        }

        public async Task AddRangeAsync(List<MeasurementsSeries> measurementSeriesList)
        {
          await  _dbSet.AddRangeAsync(measurementSeriesList);
        }

        public List<MeasurementsSeries> GetMeasurementSeriesesWithoutOrderAssigned()
        {
           return _dbSet.Include(ms => ms.Measurements).Where(m => m.OrderId == null).ToList();
        }
    }
}
