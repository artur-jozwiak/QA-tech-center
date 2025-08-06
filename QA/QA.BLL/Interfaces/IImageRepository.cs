using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IImageRepository
    {
        //bool ImgNameExist(string imageName);
        Task<bool> ImgNameExist(string imageName);
        Task Add(Image img);
        void Update(Image img);
        Task<Image> GetBy(string name);
        void Delete(Image img);
    }
}

