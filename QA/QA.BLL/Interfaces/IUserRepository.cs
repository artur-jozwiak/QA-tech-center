using Microsoft.AspNetCore.Identity;

namespace QA.BLL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<IdentityUser>> GetAllAsync();
        List<IdentityUser> GetAll();
    }
}
