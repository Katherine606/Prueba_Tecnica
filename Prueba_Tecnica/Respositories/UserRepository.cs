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

        //listar usuarios
        public async Task<IEnumerable<User>> ListarUsuariosAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //crear usuario

        public async Task CrearUsuario(User user)
        {
            await _context.Users.AddAsync(user);
        }

        //obtener usuario por Id
        public async Task<User?> ObtenerUsuarioAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.id == userId);
            return user;
        }

        //guardar cambios
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
