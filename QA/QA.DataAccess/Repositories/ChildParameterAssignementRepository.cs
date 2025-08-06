using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.DataAccess.Repositories
{
    public class ChildParameterAssignementRepository : IChildParameterAssignementRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<ChildParametersAssignement> _dbSet;

        public ChildParameterAssignementRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<ChildParametersAssignement>();
        }

        public void Add(ChildParametersAssignement childParametersAssignement)
        {
            _dbSet.Add(childParametersAssignement);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
