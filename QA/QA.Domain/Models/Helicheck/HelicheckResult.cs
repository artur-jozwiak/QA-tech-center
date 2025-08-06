namespace QA.Domain.Models.Helicheck.Models;

public partial class HelicheckResult
{
    public int MessIx { get; set; }
    public HelicheckMeasurements HelicheckMeasurements { get; set; }

    public int KritIx { get; set; }
    public HelicheckParameter HelicheckParameter { get; set; } // Ensure not null

    public string ValueF { get; set; } = null!;

    public int Valid { get; set; }
}
