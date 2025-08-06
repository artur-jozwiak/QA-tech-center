namespace QA.Domain.Models
{
    public class VisualInspectionForm
    {
        public int Id { get; set; }
        public string? MRBNumber { get; set; }// nie użyte
        public string? InstructionNumber { get; set; }// nie uzyte
        public string? Comments { get; set; }
        public bool KeyenceIsChecked { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public virtual ICollection<Sample>? Samples { get; set; }
        public virtual List<Marker>? Markers { get; set; } = new List<Marker>();
        public virtual Image? Image { get; set; }
        public int? ImageId { get; set; }
    }

    //public string OrderNumber { get; set; } = String.Empty;
    //public string ProductNumber { get; set; } = String.Empty;
}
