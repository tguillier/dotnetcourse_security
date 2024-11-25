using System.ComponentModel.DataAnnotations;

namespace UserManager.Api.Commands
{
    public class LoginCommand
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
