namespace QA.Domain.Models
{
    public class PowderSpecification
    {
        public int Id { get; set; }
        public string PowderType { get; set; }

        public decimal? HCJMin { get; set; }
        public decimal? HCJNominal { get; set; }
        public decimal? HCJMax { get; set; }

        public decimal? COMMin { get; set; }
        public decimal? COMNominal { get; set; }
        public decimal? COMMax { get; set; }

        public decimal? DensityMin { get; set; }
        public decimal? DensityNominal { get; set; }
        public decimal? DensityMax { get; set; }

        public int? HV30Min { get; set; }
        public int? HV30Nominal { get; set; }
        public int? HV30Max { get; set; }

        public string? K1C { get; set; }
        public string? Porosity { get; set; }

        public string? ReleaseRules { get; set; }
        public string? ReleaseRulesForSamples { get; set; }
    }
}
