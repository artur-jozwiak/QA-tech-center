using QA.Domain.Models;

namespace QA.UI.Models
{
    public class MeasurementsGroup
    {
        public List<Measurement> Measurements { get; set; }
        public string GrouppingKey {  get; set; }
    }
}
