namespace QA.Domain.Models.Helicheck.Models;

public partial class HelicheckParameter
{
    public int Ix { get; set; }

    public int PrgIx { get; set; }
    public Program HelicheckProgram { get; set; }

    public string Name { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Otol { get; set; } = null!;

    public string Utol { get; set; } = null!;

    public int Dim { get; set; }

    public int LfdNr { get; set; }

    public string Ids { get; set; } = null!;

    public ICollection<HelicheckResult> HelicheckResults { get; set; } = new List<HelicheckResult>();

    //////////
    public virtual ICollection<HelicheckMeasurements> HelicheckMeasurements { get; set; } = new List<HelicheckMeasurements>();
    ///
}
