using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.Entities;

namespace Prueba_Tecnica.Respositories
{
    public class AuthRepository
    {
        private readonly AppDbContext _context;


        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        //obtiene al usuario por email 
        public async Task<User?> ObtenerUsuarioPorEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
            return user;
        }

       
        // devuelve el email (string) 
        public async Task<string?> ObtenerEmailAsync(string email)
        {
            var emailE = await _context.Users.Where(u => u.email == email).Select(u => u.email).FirstOrDefaultAsync();
            return emailE;
        }
    }
}
