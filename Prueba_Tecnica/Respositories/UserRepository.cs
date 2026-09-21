using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.Entities;

namespace Prueba_Tecnica.Respositories
{

    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> ListarUsuariosAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CrearUsuario(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
