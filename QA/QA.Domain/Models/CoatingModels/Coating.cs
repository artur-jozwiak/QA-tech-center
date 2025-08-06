namespace QA.Domain.Models.CoatingModels
{
    public class Coating
    {
        public int Id { get; set; }
        public string CoatingSymbol { get; set; }
        public string ProcessId { get; set; }
        public string Type { get; set; }
        public string CoatingName { get; set; }
        public string InternalName { get; set; }
        public decimal LSL { get; set; }
        public decimal USL { get; set; }
        public int Limit { get; set; }
    }
}
