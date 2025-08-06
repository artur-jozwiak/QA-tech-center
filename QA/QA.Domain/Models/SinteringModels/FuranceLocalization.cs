namespace QA.Domain.Models.SinteringModels
{
    public class FurnaceLocalization
    {
        public int Id { get; set; }
        public int StackNo { get; set; }
        public int LevelNo { get; set; }
        public virtual ICollection<TrayLocation> TrayLocations { get; set; }  = new List<TrayLocation>();
        //public virtual ICollection<MeasurementsSeries> MeasurementsSeries { get; set; }
    }
}
