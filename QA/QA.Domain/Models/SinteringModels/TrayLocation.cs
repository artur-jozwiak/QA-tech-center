namespace QA.Domain.Models.SinteringModels
{
    public class TrayLocation
    {
        public int Id { get; set; }
        public int StackNo { get; set; }
        public int LevelNo { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public string? TrayCoating { get; set; } = string.Empty;
        public int Qty { get; set; }
        public DateTime? RowDt { get; set; }

        public virtual Order? Order { get; set; }
        public int? OrderId { get; set; }

        public virtual FurnaceLocalization FuranceLocalization { get; set; }
        public int FuranceLocalizationId { get; set; }

        public virtual SinteringBatch? Sintering { get; set; } 
        public int? SinteringId { get; set; }

        public virtual ICollection<MeasurementsSeries>? MeasurementsSeries { get; set; }

        //public decimal TrayWeight => Order?.Product.Weight * Qty ?? 0;
        public decimal TrayWeight => IsScrapTray ? 1500 : (Order?.Product.Weight * Qty ?? 0); // nowe 04.04.25


        //public decimal TrayHeight => Order?.Product?.Height is decimal height ? new[] { 8, 12, 16 }.FirstOrDefault(h => h >= height + 1) + 6.5m : 14.5m;// to tylko na potrzeby testów
        //public decimal TrayHeight => Order?.Product?.SpacerHeight is decimal height ? (decimal)Order?.Product?.SpacerHeight + 6.5m : 14.5m;
        public decimal TrayHeight => Order?.Product?.SpacerHeight is decimal height ? (decimal)Order?.Product?.SpacerHeight + 6m : 14m;// korekcja wysokości 14.04.25

        public bool IsScrapTray {  get; set; }
        public bool IsEmptyTray {  get; set; }
        public bool ContainsMasterSample {  get; set; }
    }
}
