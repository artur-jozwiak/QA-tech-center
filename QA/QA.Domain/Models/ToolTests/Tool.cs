using QA.Domain.Models.Enums;


namespace QA.Domain.Models.ToolTests
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToolTestComparison> ToolTestComparisons { get; set; } = new List<ToolTestComparison>();
    }
}
