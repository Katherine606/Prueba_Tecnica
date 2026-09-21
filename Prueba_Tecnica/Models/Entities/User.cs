using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required, MaxLength(100)]
        public string email { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string fullName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string role { get; set; } = "User";

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
