

namespace QA.Domain.Models.OldDb;

public partial class Powder
{
    public int Id { get; set; }

    public string? PowderName { get; set; }

    public string? PowderGrade { get; set; }

    public string? PowderBatch { get; set; }

    public double? GreenDensity { get; set; }

    public double? Density { get; set; }

    public double? Vs13 { get; set; }

    public double? Vs16 { get; set; }

    public double? Vs1866 { get; set; }

    public double? Vs21 { get; set; }

    public double? Hs13 { get; set; }

    public double? Hs16 { get; set; }

    public double? Hs1866 { get; set; }

    public double? Hs21 { get; set; }

    public string? F14 { get; set; }

    //public int? ColorId { get; set; }

    public string? Modifier { get; set; }

    public bool Active { get; set; }

    public DateTime? ModificationDate { get; set; }
}
