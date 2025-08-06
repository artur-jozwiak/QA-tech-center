using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly QAContext _context;
        private readonly DbSet<Image> _dbSet;

        public ImageRepository(QAContext context)
        {
            _context = context;
            _dbSet = context.Set<Image>();
        }

        public async Task<bool> ImgNameExist(string imageName)
        {
            return await _dbSet.AnyAsync(i => i.Name == imageName);
        }

        public async Task Add(Image img)
        {
;           await _dbSet.AddAsync(img);
        }

        public void  Update (Image img)
        {
            _dbSet.Update(img);
        }

        public async Task<Image> GetBy(string name) 
        { 
            return await _dbSet.FirstOrDefaultAsync(i => i.Name == name);
        }

        //public void Delete(Image img)
        //{
        //    _dbSet.Remove(img);
        //}

        public void Delete(Image img)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.ImageUrl.TrimStart('/'));

            _dbSet.Remove(img);
            _context.SaveChanges();

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
