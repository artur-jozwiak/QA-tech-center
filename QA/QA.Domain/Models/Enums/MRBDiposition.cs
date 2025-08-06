
using System.ComponentModel.DataAnnotations;

namespace QA.Domain.Models.Enums
{
    public enum MRBDiposition
    {
        [Display(Name = "Use as it is")]
        UseAsItIs = 1,
        [Display(Name = "Scrap All")]
        ScrapAll = 2,

        [Display(Name = "Complaint To Supplier")]
        ComplaintToSupplier = 3,
        [Display(Name = "Notify Customer")]
        NotifyCustomer = 4,

        Rework =  5,
        Selection = 6,
        Other = 7,

    }
}
