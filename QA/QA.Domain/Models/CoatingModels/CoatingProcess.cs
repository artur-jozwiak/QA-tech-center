
namespace QA.Domain.Models.CoatingModels
{
    public class CoatingProcess
    {
        public int Id { get; set; }
        public int RunNo { get; set; }
        public string ProcessId { get; set; }
        public string Coating { get; set; }
        public ICollection<CoatingMeasurementSeries> Measurements { get; set; }
    }
}
