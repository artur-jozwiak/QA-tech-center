
//skorzystać z tabeli Parameters?
namespace QA.Domain.Models.Keyence
{
    public class KeyenceParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public decimal LSL { get; set; }
        public decimal LowerTollerance { get; set; }
        public decimal Nominal { get; set; }
        public decimal UpperTollerance { get; set; }
        public decimal USL { get; set; }

        public string Unit { get; set; }

        public string FileName { get; set; }
        public DateTime ModificationDate { get; set; }

        public virtual ICollection<KeyenceMeasurement>? Measurements { get; set; } = new List<KeyenceMeasurement>();
    }
}
