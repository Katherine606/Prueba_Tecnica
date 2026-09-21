using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models.DTOs
{
    public class UserCrearDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Role { get; set; } = "User";
    }

}
