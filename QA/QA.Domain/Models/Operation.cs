namespace QA.Domain.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Symbol { get; set; }
        public int TechnologyId { get; set; }
        public string? Comment { get; set; } = String.Empty;
        public virtual Product? Product { get; set; }
        public int? ProductId { get; set; }
        public virtual ICollection<Parameter> Parameters { get; set; } = new List<Parameter>();
        public List<MeasurementsSeries>? MeasurementsSeries { get; set; } = new List<MeasurementsSeries>();
        public virtual List<OperationDetails> OperationDetails { get; set; } = new List<OperationDetails>();

        public virtual ICollection<DescriptiveParameter> DescriptiveParameters { get; set; } = new List<DescriptiveParameter>();

    }
}
