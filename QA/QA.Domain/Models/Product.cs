using System.ComponentModel.DataAnnotations;

namespace QA.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Symbol jest wymagany")]
        public string Symbol { get; set; } = string.Empty;
        public string? PdmNo { get; set; }
        public string? Description { get; set; }
        public int TechnologyId { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Operation>? Operations { get; set; }
        public bool IsPattern { get; set; }
        public virtual Image? Image { get; set; }
        public int? ImageId { get; set; }
        public string? InstructionPath { get; set; }

        //to mozna przenieść do klasy i tabeli additional colummns
        public int UnitsPerSinteringTray { get; set; } = 0;
        public decimal SpacerHeight { get; set; } = 0;// zmienić nazwe na SpacerHeight
        public decimal Weight { get; set; } = 0;

        //dodać tolerancje  zwężenia - NarrowingTolerance - ?
        //dodać tolerancje  zwężenia - NarrowingTolerance + ?

        //tylko dla głowic
        public decimal? NarrowingLTol { get; set; }
        public decimal? NarrowingUTol { get; set; }
    }
}
