
namespace QA.Domain.Models
{
    public class SampleDefect
    {
        public int Id { get; set; }
        public int? DefectsQty { get; set; }
        public virtual Sample Sample { get; set; }
        public int SampleId { get; set; }

        public virtual Defect Defect { get; set; }
        public int DefectId { get; set; }
        public string DefectSymbol { get; set; } = string.Empty;
        //Czy dodać order?
    }
}
