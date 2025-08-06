using QA.Domain.Models.Enums;
using QA.Domain.Models.SinteringModels;

namespace QA.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int HermesId { get; set; }
        public string OrderKey { get; set; } = string.Empty;
        public string? ShortenedKey { get; set; }
        public decimal Qty { get; set; }
        public DateTime RowDatetime { get; set; }
        public OrderStatus Status { get; set; }
        public string? PowderSymbol { get; set; }
        public string? PowderBatch { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual ICollection<Measurement>? Measurements { get; set; }
        public virtual VisualInspectionForm? VisualInspectionForm { get; set; }
        public int? VisualInspectionFormId { get; set; }
        public virtual ICollection<MeasurementsSeries>? MeasurementsSeries { get; set; }
        public virtual MRB? MRB { get; set; }
        public int? MRBId { get; }
        public virtual ICollection<Result>? Results { get; set; }

        //public int Qty { get; set; }
        //public string? StatusModifier { get; set; }

        public virtual ICollection<Pressing> Pressings { get; set; }

        //Sintering
        public virtual ICollection<TrayLocation>? TrayLocations { get; set; } = new List<TrayLocation>();

        public int DistributedQty => TrayLocations?.Sum(ol => ol.Qty) ?? 0;

        public int TraysPerSintering {  get; set; } 
    }
}
