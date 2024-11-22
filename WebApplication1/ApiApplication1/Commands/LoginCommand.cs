using System.ComponentModel.DataAnnotations;

namespace ApiApplication1.Commands
{
    public class LoginCommand
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
