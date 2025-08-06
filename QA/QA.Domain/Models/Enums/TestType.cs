using System.ComponentModel.DataAnnotations;

namespace QA.Domain.Models.Enums
{
    public enum TestType
    {
        [Display(Name = "Tool life test")]
        ToolLifeTest = 1,

        [Display(Name = "Roughness evaluation")]
        RoughnessEvaluation = 2,
    }
}
