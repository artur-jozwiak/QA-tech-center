namespace QA.Domain.Models
{
    public class Pressing
    {
        public int Id { get; set; }
        public DateTime? RowDateTime { get; set; }
        public string? OrderKey { get; set; }
        public string? TrialNo { get; set; }
        public string PDMNo { get; set; }
        public decimal? Height1 { get; set; }
        public decimal? Height2 { get; set; }
        public decimal? Height3 { get; set; }
        public decimal? Height4 { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Force { get; set; }

        //upper compaction stroke B
        public decimal? UCSB { get; set; }

        //Underpressing stroke
        public decimal? UPS { get; set; }

        public decimal? PrecompactingA { get; set; }
        public decimal? PrecompactingB { get; set; }
        public string? PressStrokeRelation { get; set; }
        public decimal? Decopression1A { get; set; }
        public decimal? Decopression1B { get; set; }
        public decimal? DecopressionV1 { get; set; }
        public decimal? Decopression2A { get; set; }
        public decimal? Decopression2B { get; set; }
        public decimal? DecopressionV2 { get; set; }
        public decimal? UnderfillStrokeB { get; set; }
        public bool SuctionFill { get; set; }
        public bool CounturFilling { get; set; }
        public string? TrayQty { get; set; }
        public string? BaloonNo { get; set; }
        public string? RobotProgam { get; set; }
        public string? BurringPrassuereCloseValve { get; set; }
        public string? BurringPrassuereOpenValve { get; set; }
        public string? Comment { get; set; }
        public string? Powder { get; set; } = string.Empty;

        public Order? Order { get; set; } 
        public int? OrderId { get; set; }
    }
}
