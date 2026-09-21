namespace Prueba_Tecnica.Models.DTOs
{
    namespace Prueba_Tecnica.DTOs
    {
 
        public class UserListaDto
        {
            public int Id { get; set; }
            public string Email { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
        }
    }
}
