using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IImgService
    {
        Task DeleteImage(Image img);

    }
}
