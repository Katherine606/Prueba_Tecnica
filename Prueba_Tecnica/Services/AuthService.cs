using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.Entities;

namespace Prueba_Tecnica.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService( AppDbContext context)
        {
        
            _context = context;
        }
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.email == email);
            if (user == null)
            {
                throw new ApiException("Credenciales inválidas.", 401);
            }

            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, user.password);
            if (!passwordValido)
            {
                throw new ApiException("Credenciales inválidas.", 401);
            }

            return user;
        }
    }
}
