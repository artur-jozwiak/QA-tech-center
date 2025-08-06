
namespace QA.Domain.Models
{
    public class Sample
    {
        public int Id { get; set; }
        public int SampleNumber { get; set; }//Kolejność próbki 1,2,3...
        public int SampleQty { get; set; }
        public int? DefectsQty { get; set; }
        public int? GoodsQty { get; set; }
        public string? Operator { get; set; }
        public DateTime Date { get; set; }  
        public virtual VisualInspectionForm VisualInspectionForm { get; set; }

        public int VisualInspectionFormId { get; set; }
        public virtual ICollection<SampleDefect>? SampleDefects { get; set; } = new List<SampleDefect>();

        //public string OrderKey { get; set; }
    }
}
