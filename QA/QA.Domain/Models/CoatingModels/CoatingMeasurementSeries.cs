

namespace QA.Domain.Models.CoatingModels
{
    public class CoatingMeasurementSeries
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TowerNo { get; set; }
        public char Level { get; set; }
        public bool NonRotatingRod { get; set; }
        public bool IsReferenceSample { get; set; }
        public string? Operator { get; set; }

        public decimal? Thickness1 { get; set; }
        public decimal? Thickness2 { get; set; }
        public decimal? Thickness3 { get; set; }
        public decimal? Thickness4 { get; set; }

        public int? Adhesion1 { get; set; }
        public int? Adhesion2 { get; set; }
        public int? Adhesion3 { get; set; }
        public int? Adhesion4 { get; set; }

        public string Comment { get; set; } = string.Empty;

        public virtual CoatingProcess CoatingProcess { get; set; }
        public  int CoatingProcessId { get; set; }
    }
}
