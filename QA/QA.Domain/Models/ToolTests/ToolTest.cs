using QA.Domain.Models.Enums;

namespace QA.Domain.Models.ToolTests
{
    public class ToolTest
    {
        public int Id { get; set; }
        public string HolderType { get; set; }
        public string Manufacturer { get; set; }
        public string ProductSymbol { get; set; }
        public string Application { get; set; }
        public string Substrate { get; set; }
        public string Coating { get; set; }
        public string CoatingThickness { get; set; }
        public string BatchNo { get; set; }
        public string VisualInspection { get; set; }
        public string Other { get; set; }
        public string Feedf { get; set; }
        public string FeedVf { get; set; }
        public string SpeedVc { get; set; }
        public string CuttingDepth { get; set; }
        public string ae { get; set; }
        public string NumberOfPasses { get; set; }
        public string Time { get; set; }
        public string Cooling { get; set; }
        public string ChipShape { get; set; }
        public string WorkpieceRoughness { get; set; }
        public string VisualDamageDescription { get; set; }
        public string EdgeWear { get; set; }
        public string Chipping { get; set; }
        public string PlasticDeformation { get; set; }
        public ToolTestComparison Comparison { get; set; }
        public int ComparisonId { get; set; }
        public virtual ICollection<ComparisonPoint> ComparisonPoints { get; set; }
    }
}
