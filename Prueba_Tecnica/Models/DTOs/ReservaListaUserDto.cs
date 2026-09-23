using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.Models.DTOs
{
    public class ReservaListaUserDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;

        public required string SalaNombre { get; set; }
    }
}
