using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IMRBRepository
    {
        Task Add(MRB mrb);
        Task<MRB?> GetBy(int? id);
        void Update(MRB mrb);
        Task<List<MRB>> GetAll();
        Task<int> CountAnnualMRB();
        void UpdateMemberSummary(MRBMemberSummary memberSummary);
        Task AddMemberSummary(MRBMemberSummary newMemberSummary);
    }
}
