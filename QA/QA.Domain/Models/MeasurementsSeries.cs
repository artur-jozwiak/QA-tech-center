using QA.Domain.Models.Enums;
using QA.Domain.Models.SinteringModels;

namespace QA.Domain.Models
{
    //Tylko dla piaskowania + Laboratorium

    public class MeasurementsSeries
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string TrayNo { get; set; } = string.Empty;
        public string PositionOnTray { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;

        //Lab
        public int? StackNo { get; set; }
        public string PorosityClass {  get; set; } = string.Empty;
        public string GrainSize {  get; set; } = string.Empty;
        public string StorageLocation { get; set; } = string.Empty;

        //Lab
        public List<Measurement>? Measurements { get; set; }
        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        public virtual Order? Order { get; set; }
        public int? OrderId { get; set; }
        public Side Side { get; set; }

        //Wybrać i zostawić jedno jedno
        //public int? SampleLocationId { get; set; }
        //public FurnaceLocalization? SampleLocation { get; set; }//?? furanceocalization??

        public int? TrayLocationId { get; set; }
        public TrayLocation? TrayLocation { get; set; }//?? furanceocalization??

        // PVD
        // Lokalizacja w piecu 1T REF 1M REF2B
        // wierza  t/m/b top/middle/bottom
        //
    }
}