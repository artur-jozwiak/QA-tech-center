using System.ComponentModel.DataAnnotations;

namespace QA.Domain.Models
{
    public class DescriptiveParameter
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; } = string.Empty;
        public string? TestingInstrument { get; set; }
        public string? FillingMethod { get; set; }
        public string? Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Operation Operation { get; set; }
        public int OperationId { get; set; }
        public virtual ICollection<Result> Values { get; set; } = new List<Result>();
        public virtual Image? Image { get; set; }
        public int? ImageId { get; set; }
    }
}
