namespace QA.Domain.Models
{
    public class Marker
    {
        public int Id { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public virtual VisualInspectionForm VisualInspectionForm { get; set; }
        public int VisualInspectionFormId { get; set; }

        public DateTime CreationDate { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;

        public Image? Image { get; set; }
        public int? ImageId { get; set; }
    }
}
