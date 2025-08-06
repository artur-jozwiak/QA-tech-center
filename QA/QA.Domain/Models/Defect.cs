

namespace QA.Domain.Models
{
    public class Defect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public virtual DefectCategory DefectCategory { get; set; }
        public int DefectCategoryId { get; set; }
    }
}
