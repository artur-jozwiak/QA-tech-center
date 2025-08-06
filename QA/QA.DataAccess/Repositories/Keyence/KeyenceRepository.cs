using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using QA.BLL.Interfaces;
using QA.Domain.Models.Keyence;

namespace QA.DataAccess.Repositories.Keyence
{
    public class KeyenceRepository : IKeyenceRepository
    {
        private readonly QAContext _context;

        public KeyenceRepository(QAContext context)
        {
            _context = context;
        }

        public async Task<List<KeyenceMeasurement>> GetMeasurementsBy(string orderKey, string productSymbol)
        {
            //Czy nie mozna tego porać po pdm number?
            if (!orderKey.StartsWith("TA"))
            {
                if (productSymbol.StartsWith("W-B"))
                {
                    return await _context.KeyenceMeasurements.Where(km => km.OrderNo == orderKey && km.FileName.StartsWith("ST") && km.Value != 255).Include(km => km.Parameter).ToListAsync();
                }
                else if (productSymbol.StartsWith("W-S"))
                {
                    return await _context.KeyenceMeasurements.Where(km => km.OrderNo == orderKey && km.FileName.StartsWith("PG") && km.Value != 255).Include(km => km.Parameter).ToListAsync();
                }
            }
            else
            {
                return _context.KeyenceMeasurements.Where(km => km.OrderNo.Contains(orderKey) && km.Value != 255).Include(km => km.Parameter).ToList();
            }

            return Enumerable.Empty<KeyenceMeasurement>().ToList();
        }

        public async Task<List<KeyenceParameter>> GetParametersBy(string pdmNo, string productSymbol)
        {
            if (String.IsNullOrEmpty(pdmNo))
            {
                return Enumerable.Empty<KeyenceParameter>().ToList();
            }

            if (productSymbol.StartsWith("W-B"))
            {
                return await _context.KeyenceParameters.Where(kp => kp.FileName.Contains(pdmNo) && kp.FileName.StartsWith("ST")).ToListAsync();
            }
            else if (productSymbol.StartsWith("W-S"))
            {
                return await _context.KeyenceParameters.Where(kp => kp.FileName.Contains(pdmNo) && kp.FileName.StartsWith("WB")).ToListAsync();
            }

            return Enumerable.Empty<KeyenceParameter>().ToList();
        }

        public async Task<KeyenceParameter> GetParameterWithMeasurementsBy(int parameterId)
        {
            var measurements = await _context.KeyenceMeasurements.Where(km => km.ParameterId == parameterId && km.Value != 255).Include(km => km.Parameter).ToListAsync();

            return measurements.First().Parameter;
        }
    }
}
