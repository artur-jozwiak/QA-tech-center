
namespace QA.Domain.Models
{
    public class ChildParametersAssignement
    {
        public int Id { get; set; }
        public int ParameterOrder { get; set; }
        public Parameter? ChildParameter { get; set; }
        public int? ChildParameterId { get; set; }
        public Parameter? ParentParameter { get; set; }
        public int? ParentParameterId { get; set; }
    }
}
