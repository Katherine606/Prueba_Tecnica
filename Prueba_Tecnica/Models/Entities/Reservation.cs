using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_Tecnica.Models.Entities
{

        public class Reservation
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public DateTime StartTime { get; set; }

            [Required]
            public DateTime EndTime { get; set; }

            [Required]
            [MaxLength(50)]
            public string Status { get; set; } = "Pendiente"; 

            [Required]
            public int UserId { get; set; }

            [ForeignKey("UserId")]
            public User? User { get; set; }

            [Required]
            public int RoomId { get; set; }

            [ForeignKey("RoomId")]
            public Room? Room { get; set; }
        }
}




