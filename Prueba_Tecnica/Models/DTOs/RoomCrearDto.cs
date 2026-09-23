using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models.DTOs
{
    public class RoomCrearDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, 1000, ErrorMessage = "La capacidad debe ser al menos de 1 persona.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        [StringLength(150, ErrorMessage = "La ubicación no puede superar los 150 caracteres.")]
        public string Location { get; set; } = string.Empty;
    }
}