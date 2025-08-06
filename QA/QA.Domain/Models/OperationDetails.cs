using QA.Domain.Models.Enums;

namespace QA.Domain.Models
{
    // jedno dla wszystkich zleceń z tym produktem
    // zmienić nazwe na ProductOperationDetails
    // dodać klase OrderOperationDetails jeśli pojawi sie potrzeba przypisywania dodatkowych parametrów do zlecenia

    public class OperationDetails
    {
        public int Id { get; set; }
        public DateTime? ModificationDate {  get; set; } 

        //Piaskowanie
        public string? Program { get; set; } 
        public decimal? PressureLeft { get; set; }
        public decimal? PressureRight { get; set; }
        public int? Feed { get; set; }
        public int? SandblastingHeight { get; set; }
        public int? BurrRate { get; set; }
        public string? ProcessTray { get; set; } 
        public int? HeadsQty { get; set; }
        public string? ScanningMode { get; set; } 
        public int? NoOfPasses { get; set; }
        public decimal? GunsPitch { get; set; }
        public string? NoBlastingBetweenRows { get; set; }
        //Piaskowanie

        //Szlifowanie góra dół
        public string? Cassette { get; set; }
        public int? CassetteInsertsQty { get; set; }
        public decimal? UpperRPM { get; set; }
        public string? UpperDirection { get; set; }
        public decimal? LowerRPM { get; set; }
        public string? LowerDirection { get; set; }
        public decimal? CentralTableRPM { get; set; }
        public string? CentralTableDirection{ get; set; }
        public string? OrderKey { get; set; }
        //Szlifowanie góra dół

        public OperationType OperationType { get; set; }

        public virtual int OperationId { get; set; }
        public virtual Operation Operation { get; set; }
    }
}
