using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models.CoatingModels;
using QA.Domain.Models.Erp;

namespace QA.DataAccess.Repositories.ERPRepositories
{
    public class ErpOrderRepository : IErpOrderRepository
    {
        private readonly ERPContext _context;
        private readonly DbSet<ErpOrder> _dbSet;

        public ErpOrderRepository(ERPContext context)
        {
            _context = context;
            _dbSet = context.Set<ErpOrder>();
        }

        public async Task<ErpOrder> GetBy( int erpId)
        {
          return  _dbSet.FirstOrDefault(o => o.Id == erpId);
        }

        public async Task<List<ErpOrder>> GetAll()
        {
            return await _dbSet.Where(o => (o.SymbolWyr.StartsWith("W-S") || o.SymbolWyr.StartsWith("W-B")) && (o.KluczZp.StartsWith("ZPR/2I")
                                        || o.KluczZp.StartsWith("ZPR/2T")
                                        || o.KluczZp.StartsWith("ZPR/2W"))).ToListAsync();
        }

        public async Task<List<string>?> GetOrdersKeyBy(string productSymbol)
        {
            var productOrders = await _dbSet.Where(o => o.SymbolWyr == productSymbol).Select(o => o.KluczSkrocony).ToListAsync();
            return productOrders;
        }

        //05.05.25
        public List<string> GetOrderdCoatingProcesses(int erpId)
        {
            var processes = _context.CoatingOrders.Where(co => co.ErpId == erpId).ToList();
            var runNumbers = processes.Select(p => p.MeNrprpo.ToString()).ToList(); ;
            return runNumbers;
        }
    }
}
