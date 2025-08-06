using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class ParameterRepository : IParameterRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Parameter> _dbSet;

        public ParameterRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Parameter>();
        }

        public Parameter GetBy(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<Parameter> GetWithMeasurementsBy(int id)
        {
            return await _dbSet.Include(p => p.Operation).Include(p => p.Measurements).FirstOrDefaultAsync(p => p.Id == id);// Operation bedzie nie potrzebny jeśli nie użyjesz spc
        }

        public List<Parameter> GetByOperationId(int operationId)
        {
            return _dbSet.Where(p => p.OperationId == operationId).ToList();
        }

        public void UpdateRange(List<Parameter> parameters)
        {
            _dbSet.UpdateRange(parameters);
        }

        public async Task<List<Parameter>> GetParametersWithMeasurementsBy(int orderId, int[] parametersIds)
        {
            return await _dbSet.Include(p => p.Measurements.Where(m => m.OrderId == orderId))
                               .Include(p => p.Operation)
                               .ThenInclude(o => o.Product)
                               .Include(p => p.Operation)
                                .ThenInclude(o => o.MeasurementsSeries.Where(ms => ms.OrderId == orderId))
                            .Include(p => p.DevicePort)
                                .ThenInclude(dp => dp.MeasuringDevice)
                            .Include(p => p.Image)
                            .Where(p => parametersIds.Contains(p.Id))
                            .OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<List<Parameter>> GetParametersWithMeasurementsBy(int[] parametersIds)
        {
            //return await _dbSet.Include(p => p.Measurements)
            //                   .Include(p => p.Operation)
            //                        .ThenInclude(o => o.Product)
            //                   .Include(p => p.DevicePort)
            //                        .ThenInclude(dp => dp.MeasuringDevice)
            //                   .Include(p => p.Image)
            //                   .Where(p => parametersIds.Contains(p.Id))
            //                   .OrderBy(p => p.DevicePort.Name).ToListAsync();


            return await _dbSet.Include(p => p.Measurements)
                   .Include(p => p.Operation)
                        .ThenInclude(o => o.Product)
                    .Include(p => p.Operation)
                        .ThenInclude(o => o.OperationDetails)
                   .Include(p => p.DevicePort)
                        .ThenInclude(dp => dp.MeasuringDevice)
                   .Include(p => p.Image)
                   .Where(p => parametersIds.Contains(p.Id))
                   .OrderBy(p => p.DevicePort.Name).ToListAsync();
        }

        public async Task<List<Parameter>> GetParametersWithMeasurementsBy(int[] parametersIds, string orderKey)
        {
            return await _dbSet.Include(p => p.Measurements.Where(m => m.OrderKey == orderKey))
                               .Include(p => p.Operation)
                                    .ThenInclude(o => o.Product)
                               .Include(p => p.DevicePort)
                                    .ThenInclude(dp => dp.MeasuringDevice)
                               .Include(p => p.Image)
                               .Where(p => parametersIds.Contains(p.Id))
                               .OrderBy(p => p.DevicePort.Name).ToListAsync();
        }

        public async Task<List<Parameter>> GetParametersWithMeasurementsBy(int orderId, int operationId )
        {
            return await _dbSet.Include(p => p.Measurements.Where(m => m.OrderId == orderId))
                                .ThenInclude(m => m.MeasurementsSeries)
                            .Include(p => p.Image)
                            .Where(p => p.OperationId == operationId)
                            .OrderBy(p => p.Id).ToListAsync();
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Parameter>> GetAllBy(int productId)
        {
            var operations =  _context.Operations.Where(o => o.ProductId == productId).ToList();
            return await _context.Operations.Include(o => o.Parameters).Where(o => o.ProductId == productId).SelectMany(o => o.Parameters).ToListAsync();
        }

        //

        public async Task<List<Parameter>> GetHeightParameters ()
        {
            return await _dbSet.Include(p => p.Measurements)
                                    .ThenInclude(m => m.Order)
                               .Include(p => p.Operation)
                               .Where(p => p.Name.StartsWith("S") && p.Operation.Symbol == "WM.2702.NW")
                               .ToListAsync();
        }
    }
}
