namespace QA.Domain.Models.Helicheck.Models;

//seria
public partial class HelicheckMeasurements
{
    public int Ix { get; set; }

    public int PrgIx { get; set; }
    public Program Programme { get; set; }

    public string Datum { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int ToolStrIx { get; set; }//odniesienie do Infodat2.str
    public virtual HelicheckInfo InfoDat2 { get; set; }//odniesienie do Infodat2.str

    public int ToolNum { get; set; }

    public int KundeIx { get; set; }//klient

    public string Kunde { get; set; } = null!;//klient

    public Guid ReportGuid { get; set; }

    public byte[]? ReportData { get; set; }

    public virtual ICollection<HelicheckResult> Results { get; set; } = new List<HelicheckResult>();

    public virtual ICollection<HelicheckParameter> Parameters { get; set; } = new List<HelicheckParameter>();

}
