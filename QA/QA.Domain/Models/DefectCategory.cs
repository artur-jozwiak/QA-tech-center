
namespace QA.Domain.Models
{
    public class DefectCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public virtual ICollection<Defect> Defects { get; set; }
    }
}
