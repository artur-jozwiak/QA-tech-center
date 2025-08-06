using QA.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace QA.UI.Models
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Hasła się róznią")]
        public string ConfirmPassword { get; set; }
    }
}
