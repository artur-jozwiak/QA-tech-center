using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.DataAccess.Migrations;
using QA.Domain.Models;
using QA.Domain.Models.SinteringModels;

namespace QA.DataAccess.Repositories
{
    public class SinteringRepository : ISinteringRepository
    {
        private readonly QAContext _context;

        public SinteringRepository(QAContext context)
        {
            _context = context;
        }

        public SinteringBatch? GetSinteringBatchBy(int? id)
        {
            return _context.Sinterings.FirstOrDefault(s => s.Id == id);
        }

        public void AddTrayLocation(TrayLocation trayLocation)
        {
            _context.TrayLocations.Add(trayLocation);
        }

        public void AddTrayLocations(List<TrayLocation> trayLocations)
        {
            _context.TrayLocations.AddRange(trayLocations);
        }

        public void AddIfNotExist(int sinteringNo)
        {
            if(!_context.Sinterings.Any(s => s.No == sinteringNo))
            {
                SinteringBatch sintering = new SinteringBatch()
                {
                    No = sinteringNo,
                    CreationDate = DateTime.Now,
                };

                _context.Sinterings.Add(sintering);
                _context.SaveChanges();
            }
        }

        public  SinteringBatch GetSinteringBy(int sinteringNo)
        {
          return   _context.Sinterings.FirstOrDefault(s => s.No == sinteringNo);
        }

        public async Task<List<FurnaceLocalization>> GetAllLocations()
        {
          return await _context.FuranceLocalizations.ToListAsync();
        }

        public  List<SinteringBatch> GetAllProcesses()
        {
            return  _context.Sinterings.OrderByDescending(s => s.No).ToList();
        }

        public  TrayLocation GetTrayLocationBy(int sinteringId, int furnaceLocalizationId)
        {
            return _context.TrayLocations.FirstOrDefault(tl => tl.SinteringId == sinteringId && tl.FuranceLocalizationId == furnaceLocalizationId);
        }

        public List<TrayLocation> GetTrayLocationsBy(int sinteringId)
        {
            return _context.TrayLocations.Include(tl => tl.Order)
                                            .ThenInclude(o => o.Product)
                                         .Where(tl => tl.SinteringId == sinteringId)
                                         .ToList();
        }

        public  List<Order> GetAdditionalSinteringOrders(int sinteringId, List<Order> orders)
        {
            var trayLocations =  _context.TrayLocations
                .Include(tl => tl.MeasurementsSeries)
                    .ThenInclude(ms => ms.Measurements)
                        .ThenInclude(m => m.Parameter)
                .Include(tl => tl.FuranceLocalization)
                .Include(tl => tl.Order)
                    .ThenInclude(o => o.Product)
                .Where(ol => ol.SinteringId == sinteringId)
                .ToList();

            var additionalOrders = trayLocations.Select(tl => tl.Order).Where(o => !orders.Contains(o)).Distinct().ToList();
            return additionalOrders;
        }

        public async Task<List<TrayLocation>> GetBy(int orderId)
        {
            return await _context.TrayLocations
                .Include(tl => tl.MeasurementsSeries)
                    .ThenInclude(ms => ms.Measurements)
                        .ThenInclude(m => m.Parameter)
                .Include(tl => tl.FuranceLocalization)
                .Where(ol => ol.OrderId == orderId)
                .ToListAsync();
        }

        public void AddReange(List<TrayLocation> sinteringLocalizations)
        {
            _context.TrayLocations.AddRange(sinteringLocalizations);
        }

        public void SeedFurnaceLocalization()
        {
            List<FurnaceLocalization> newLocalizations = new();

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 100; j++)
                {
                    FurnaceLocalization newLocalization = new FurnaceLocalization()
                    {
                        StackNo = i,
                        LevelNo = j,
                    };
                    newLocalizations.Add(newLocalization);
                }
            }

            _context.FuranceLocalizations.AddRange(newLocalizations);
            _context.SaveChanges();
        }

        public void Remove(TrayLocation trayLocation)
        {
            _context.TrayLocations.Remove(trayLocation);
        }

        public void RemoveSinteringTrayLocations(int sinteringId)
        {
           var trayLocations =  _context.TrayLocations.Where(tl => tl.SinteringId == sinteringId).ToList();
           _context.TrayLocations.RemoveRange(trayLocations);
        }
    }
}
