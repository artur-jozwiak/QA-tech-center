using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;

namespace QA.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<IdentityUser> _dbSet;

        public UserRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<IdentityUser>();
        }

        public async Task<List<IdentityUser>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public  List<IdentityUser> GetAll()
        {
            return  _dbSet.ToList();
        }
    }
}
