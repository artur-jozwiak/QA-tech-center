using QA.Domain.Models.OldDb;

namespace QA.BLL.Interfaces
{
    public interface IOldDbRepository
    {
        Task Add(S1Bk newrecord);
        bool RecordExist(string parameterName, string orderKey);
        Task<int> Complete();
        List<Powder> GetAllRtpShrinkages();
        void AddRtpShrinkage(Powder powder);
    }
}
