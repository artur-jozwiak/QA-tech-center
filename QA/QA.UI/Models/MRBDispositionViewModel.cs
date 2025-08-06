using QA.Domain.Models.Enums;

namespace QA.UI.Models
{
    public class MRBDispositionViewModel
    {
        public MRBDiposition Disposition { get; set; }
        public bool IsSelected { get; set; }

        public int MemberSummaryId { get; set; }
    }
}
