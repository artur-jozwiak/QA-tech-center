namespace QA.Domain.Models.ToolTests
{
    public class ComparisonPoint// zmienić na Control point
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Parameter { get; set; }
        public string? ControlPointValue { get; set; }
        public string? Remarks { get; set; }
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ToolTestComparison? ToolTestComparison { get; set; }
        public  int? ToolTestComparisonId { get; set; }
        public virtual ToolTest? ToolTest { get; set; }
        public int? ToolTestId { get; set; }
    }
}
