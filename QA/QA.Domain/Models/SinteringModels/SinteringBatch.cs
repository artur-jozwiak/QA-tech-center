namespace QA.Domain.Models.SinteringModels
{
    public class SinteringBatch//zmienić nazwe na SinteringBatch?
    {
        public int Id { get; set; }
        public int No { get; set; }
        public DateTime CreationDate { get; set; } = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        public DateTime CompletionDate { get; set; } = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        public bool IsLocked { get; set; }
        public virtual ICollection<TrayLocation> TrayLocations { get; set; }

        public decimal BatchWeight => TrayLocations?.Sum(tl => tl.TrayWeight) / 1000 ?? 0;

        //public decimal BatchHeight => TrayLocations?.GroupBy(tl => tl.StackNo) 
        //                                    .Select(stack => stack.Sum(tl => tl.TrayHeight)) 
        //                                    .DefaultIfEmpty(0)
        //                                    .Max()?? 0;

        public decimal BatchHeight => TrayLocations?.GroupBy(tl => tl.StackNo)
                                    .Select(stack => stack.Sum(tl => tl.TrayHeight))
                                    .DefaultIfEmpty(0)
                                    .Max() + 8 ?? 0; //8 to pierwsza piekładka //nowe 04.04.25
    }
}
