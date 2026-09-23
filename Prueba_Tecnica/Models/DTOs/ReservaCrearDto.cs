using Prueba_Tecnica.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models.DTOs
{
    public class ReservaCrearDto
    {
        [Required(ErrorMessage = "La fecha y hora de inicio son obligatorias.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "La fecha y hora de fin son obligatorias.")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Debe especificar la sala.")]
        public int RoomId { get; set; }
    }
}

