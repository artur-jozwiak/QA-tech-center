
namespace QA.Domain.Models.Helicheck.Models;

public partial class HelicheckInfo
{
    public int Ix { get; set; }

    public int IdIx { get; set; }//  odniesienie w Mesung
    public HelicheckMeasurements Messung { get; set; }//  odniesienie w Mesung

    public int IdC { get; set; }

    public int Vflag { get; set; }

    public string Str { get; set; } = null!;
}
