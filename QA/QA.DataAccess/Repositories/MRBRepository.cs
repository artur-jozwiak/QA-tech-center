using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class MRBRepository : IMRBRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<MRB> _dbSet;

        public MRBRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<MRB>();
        }

        public async Task<List<MRB>> GetAll()
        {
           return await _dbSet
                              .Include(m => m.CorrectiveActions)
                              .Include(m => m.Order)
                              .OrderBy(m => m.Id)
                              .Where(m => !m.IsDeleted)
                              .ToListAsync();
        }

        public async Task<MRB> GetBy(int? id)
        {
            var mrb = await _dbSet.Include(m => m.MemberSummary)
                                  .Include(m => m.CorrectiveActions)
                                  .Include(m => m.Instruction)
                                  .Include(m => m.Images)
                                  .FirstOrDefaultAsync(mrb => mrb.Id == id);
            return mrb;
        }

        public async Task<int> CountAnnualMRB()
        {
          return await  _dbSet.Where(m => m.CreationDate.Year == DateTime.Now.Year).CountAsync();
        }

        public async Task Add(MRB mrb)
        {
            await _dbSet.AddAsync(mrb);
        }

        public void Update(MRB mrb)
        {
            _dbSet.Update(mrb);
        }



        public async Task AddMemberSummary(MRBMemberSummary newMemberSummary)
        {
          await  _context.MRBMemberSummaries.AddAsync(newMemberSummary);
        }

        public void UpdateMemberSummary(MRBMemberSummary memberSummary)
        {
            _context.MRBMemberSummaries.Update(memberSummary);
        }
    }
}
