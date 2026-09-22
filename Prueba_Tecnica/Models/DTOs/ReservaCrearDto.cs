using Prueba_Tecnica.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models.DTOs
{
    public class ReservaCrearDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int UserId { get; set; }


        [Required]
        public int RoomId { get; set; }
    }
}

