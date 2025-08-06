using QA.BLL.Interfaces;
using QA.Domain.Models.OldDb;

//najwyższe id (przed migracją) s1_bk = 10469 - dalsze rkordy pochodzą z migracji
// najwyższe id (przed migracją) press02 = 25780 , QA.Pressing = 24985

namespace QA.DataAccess.Repositories.OldDbRepositories
{
    public class OldDbRepository : IOldDbRepository
    {
        private readonly OldDbContext _context;

        public OldDbRepository(OldDbContext context)
        {
            _context = context;
        }

        public async Task Add(S1Bk newrecord)
        {
            await _context.S1Bks.AddAsync(newrecord);
        }

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public bool RecordExist(string parameterName, string orderKey)
        {
            return _context.S1Bks.Any(r => r.Parameter == parameterName && r.OrderNo == orderKey);
        }

        public List<Powder> GetAllRtpShrinkages()
        {
            return _context.Powders.OrderByDescending(p => p.ModificationDate).ToList();
        }

        public void AddRtpShrinkage(Powder powder)
        {
            _context.Powders.Add(powder);
        }
    }
}
