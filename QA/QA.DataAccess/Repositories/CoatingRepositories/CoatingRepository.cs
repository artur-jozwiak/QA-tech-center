using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models.CoatingModels;

namespace QA.DataAccess.Repositories.CoatingRepositories
{
    public class CoatingRepository : ICoatingRepository
    {
        private readonly QAContext _context;

        public CoatingRepository(QAContext context)
        {
            _context = context;
        }

        public CoatingProcess GetBy(int processNo)
        {
            return _context.CoatingProcess.FirstOrDefault(cp => cp.RunNo == processNo);
        }

        public Domain.Models.CoatingModels.Coating GetBy(string coating)
        {
            return _context.Coatings.FirstOrDefault(cp => cp.CoatingSymbol == coating);
        }

        public Domain.Models.CoatingModels.Coating GetCoatingBy(int id)
        {
            return _context.Coatings.FirstOrDefault(cp => cp.Id == id);
        }

        public List<Domain.Models.CoatingModels.Coating> GetAll()
        {
            return _context.Coatings.OrderBy(c => c.CoatingSymbol).ToList();
        }
        public List<CoatingProcess> GetAllProcesses()
        {
            return _context.CoatingProcess.OrderByDescending(cp => cp.RunNo).ToList();
        }

        public async Task<List<CoatingProcess>> GetAllProcessesAsync()
        {
            return await _context.CoatingProcess.OrderByDescending(cp => cp.RunNo).ToListAsync();
        }



        public void Add(CoatingProcess coatingProcess)
        {
            _context.CoatingProcess.Add(coatingProcess);
        }

        public void Add(Domain.Models.CoatingModels.Coating coatingSpecification)
        {
            _context.Coatings.Add(coatingSpecification);
        }

        public void Add(CoatingMeasurementSeries measurementSeries)
        {
            _context.CoatingMeasurementSeriess.Add(measurementSeries);
        }

        public void RemoveMeasurement(CoatingMeasurementSeries measurementSeries)
        {
            _context.CoatingMeasurementSeriess.Remove(measurementSeries);
        }

        public void RemoveCoating(Coating coating)
        {
            _context.Coatings.Remove(coating);
        }

        public List<CoatingMeasurementSeries>? GetProcessMeasurementsBy(int processNo)
        {

            var result = _context.CoatingMeasurementSeriess.Where(cms => cms.CoatingProcess.RunNo == processNo)?.ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return Enumerable.Empty<CoatingMeasurementSeries>().ToList();
            }
        }

        public List<CoatingMeasurementSeries>? GetProcessMeasurementsBy(string procesSymbol)
        {
            var result = _context.CoatingMeasurementSeriess.Where(cms => cms.CoatingProcess.Coating == procesSymbol)?.ToList();

            if (result != null)
            {
                return result;
            }
            else
            {
                return Enumerable.Empty<CoatingMeasurementSeries>().ToList();
            }
        }

        public List<CoatingMeasurementSeries>? GetCoatingMeasurementsBy(string coatingName)
        {
            var coatingSymbols = _context.Coatings.Where(c => c.CoatingName == coatingName).Select(c => c.CoatingSymbol).ToList();

            var result = _context.CoatingMeasurementSeriess.Where(cms => coatingSymbols.Contains(cms.CoatingProcess.Coating)).ToList();

            if (result != null)
            {
                return result;
            }
            else
            {
                return Enumerable.Empty<CoatingMeasurementSeries>().ToList();
            }
        }
    }
}
