using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.DataAccess.Migrations;
using QA.Domain.Models.Erp;

namespace QA.DataAccess.Repositories.ERPRepositories
{
    public class SinteringOrdersRepository : ISinteringOrdersRepository
    {
        private readonly ERPContext _context;
        private readonly DbSet<SinteringOrder> _dbSet;

        public SinteringOrdersRepository(ERPContext context)
        {
            _context = context;
            _dbSet = context.Set<SinteringOrder>();
        }

        public async Task<List<string>> GetOrderKeysBy(int sinteringNo)
        {
            if (_dbSet.Any(so => so.MeNrprpo == sinteringNo))
            {
                var sinteringOrders = _dbSet.Where(so => so.MeNrprpo == sinteringNo);
                var orderKeys = await sinteringOrders.Select(so => so.KluczZp).ToListAsync();
                return orderKeys;
            }
            else
            {
                return Enumerable.Empty<string>().ToList();
            }
        }

        public async Task<decimal?> GetSineringNoByAsync(string orderKey)
        {
            if (_dbSet.Any(so => so.KluczZp == orderKey))
            {
                var sintering = await _dbSet.FirstOrDefaultAsync(so => so.KluczZp == orderKey);
                return sintering.MeNrprpo;
            }
            return null;
        }

        public async Task<SinteringOrder?> GetSineringOrderBy(string shortenedKey)
        {
            if (_dbSet.Any(so => so.KluczZpSkr == shortenedKey))
            {
                var sintering = await _dbSet.FirstOrDefaultAsync(so => so.KluczZpSkr == shortenedKey);
                return sintering;
            }
            return null;
        }

        public List<SinteringReport> GetBy(int sinteringNo)
        {
            return _context.SinteringReport.Where(sr => sr.MeNrprpo == (decimal)sinteringNo).ToList();
        }

        public List<GreenStock> GetGreensStocks()
        {
            return _context.GreensStocks.ToList();
        }

    }
}
