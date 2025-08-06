using QA.Domain.Models.Enums;
using QA.Domain.Models.ToolTests;

namespace QA.Domain.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ImageDestination Destination { get; set; }

        public virtual Product? Product { get; set; }
        public int? ProductId { get; set; }

        public virtual Parameter? Parameter { get; set; }
        public int? ParameterId { get; set; }

        public virtual MRB? MRB { get; set; }
        public int? MRBId { get; set; }

        public virtual Marker? Marker { get; set; }
        public int? MarkerId { get; set; }

        public virtual ICollection<VisualInspectionForm>? VisualInspectionForms { get; set; } = new List<VisualInspectionForm?>();

        public virtual DescriptiveParameter? DescriptiveParameter { get; set; }
        public int? DescriptiveParameterId { get; set; }

        public virtual ICollection<Marker>? Markers { get; set; }

        ///  
        public virtual ComparisonPoint? ComparisonPoint { get; set; }
        public int? ComparisonPointId { get; set; }
    }
}
