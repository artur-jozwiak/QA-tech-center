using QA.Domain.Models.Enums;

namespace QA.Domain.Models
{
    public class MRB
    {
        public int Id { get; set; } 
        public string? Symbol { get; set; }
        public string? Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? NonConformanceDescription { get; set; }
        public string? RootCause { get; set; }
        //public List<int> MRBDipositions { get; set; } = new();
        public string? Comment { get; set; }
        public bool  IsDeleted { get; set; }
        public virtual MRBInstruction? Instruction { get; set; }

        public virtual ICollection<MRBCorrectiveAction>? CorrectiveActions { get; set; } = new List<MRBCorrectiveAction>();
        public virtual ICollection<MRBMemberSummary>? MemberSummary { get; set; } = new List<MRBMemberSummary>();
        public virtual Order? Order { get; set; }
        public int? OrderId { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();

        public MRBStatus Status { get; set; }
    }
}
