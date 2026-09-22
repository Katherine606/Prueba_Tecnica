namespace Prueba_Tecnica.Models.DTOs
{
    public class ReservaListaDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;
        public string SalaNombre { get; set; } = string.Empty;
    }
}
