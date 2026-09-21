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

        public async Task<IEnumerable<Room>> ListarSalasAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task CrearSalaAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
