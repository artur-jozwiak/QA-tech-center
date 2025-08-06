using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace QA.Domain.Models.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Oczekujące")]
        Waiting = 0,

        [Display(Name = "W trakcie realizacji")]
        InProgress = 1,

        [Display(Name = "Sprawdzone - niezatwierdzone")]
        VerificationRequired = 2,

        [Display(Name = "Sprawdzone")]
        Checked = 3,

        [Display(Name = "Niezatwierdzone")]
        NotApproved = 4,

        [Display(Name = "Zatwierdzone")]
        Approved = 5
    }

   
}
