using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models.DTOs
{
    public class RoomCrearDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]

        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
