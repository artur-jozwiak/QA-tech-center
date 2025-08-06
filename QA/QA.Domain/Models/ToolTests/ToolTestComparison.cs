using QA.Domain.Models.Enums;

namespace QA.Domain.Models.ToolTests
{
    public class ToolTestComparison
    {
        public int Id { get; set; }
        public string TypeOfMachinning { get; set; }
        public string WorpieceDescription { get; set; }
        public string WorpieceHardness { get; set; }
        public string Machine { get; set; }
        public string Remarks { get; set; }
        public string? Conclusion { get; set; }
        public TestType TestType { get; set; }
        public virtual Tool Tool { get; set; }
        public int ToolId { get; set; }
        public List<ToolTest> ToolTests { get; set; } = new();
        public virtual ICollection<ComparisonPoint>? ComparisonPoints { get; set; } = new List<ComparisonPoint>();
    }
}
