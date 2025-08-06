
using System.ComponentModel.DataAnnotations;

namespace QA.Domain.Models.OldDb;

//najwyższe id (przed migracją) s1_bk = 10469 - dalsze rkordy pochodzą z migracji

public partial class S1Bk
{
    
    public int Id { get; set; }

    public string? Parameter { get; set; }

    public string? Unit { get; set; }

    public double? Usl { get; set; }

    public double? Lsl { get; set; }

    public int? Qty { get; set; }

    public double? MaxValue { get; set; }

    public double? AvgValue { get; set; }

    public double? MinValue { get; set; }

    public double? Cp { get; set; }

    public double? Cpk { get; set; }

    public double? Ro { get; set; }

    public double? Delta { get; set; }

    public string? OrderNo { get; set; }

    public double? Nominal { get; set; }
}
