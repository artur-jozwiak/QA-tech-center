
namespace QA.Domain.Models.Helicheck.Models;

public partial class Program
{
    public int Ix { get; set; }

    public int GrIx { get; set; }//grupa

    public int Icon { get; set; }

    public string Name { get; set; } = null!;

    public string Datum1 { get; set; } = null!;

    public string Datum2 { get; set; } = null!;

    public string Text1 { get; set; } = null!;

    public string Text2 { get; set; } = null!;

    public int LastToolNum { get; set; }

    public int IncToolNum { get; set; }

    public int HeadStrIx { get; set; }

    public int HeadValIx { get; set; }

    public int KundeIx { get; set; }//klient

    public bool? MessPrint { get; set; }

    public bool? MessLp { get; set; }

    public bool? MessSpc { get; set; }

    public int Spctrans { get; set; }

    public string PicPath { get; set; } = null!;

    public string Filter { get; set; } = null!;

    public Guid RowGuid { get; set; }

    public byte[]? Xml { get; set; }

    public string ProgComment { get; set; } = null!;

    public bool? Autopilot { get; set; }

    public virtual ICollection<HelicheckMeasurements> Messungs { get; set; }
    public virtual ICollection<HelicheckParameter> Kriteriens { get; set; }
}
