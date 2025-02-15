using API.Attributes;
using API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.UserDto
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Field required: Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field required: Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Field required: UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Field required: Dni")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Field required: Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field required: Password")]
        [PasswordValidation]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field required: Rol")]
        public string Rol { get; set; }

    }
}
