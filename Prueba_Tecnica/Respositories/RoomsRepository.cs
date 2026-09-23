using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.Entities;

namespace Prueba_Tecnica.Repositories
{
    public class RoomsRepository 
    {
        private readonly AppDbContext _context;

        public RoomsRepository(AppDbContext context)
        {
            _context = context;
        }

        //listar sala
        public async Task<IEnumerable<Room>> ListarSalasAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        //crear sala
        public async Task CrearSalaAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        //obtener sala por id
        public async Task<Room?> ObtenerSalaAsync(int salaId)
        {
            var sala = await _context.Rooms.FirstOrDefaultAsync(s => s.Id == salaId);
            return sala;
        }

        //guardar cambios
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
