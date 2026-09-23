using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.Entities;

namespace Prueba_Tecnica.Respositories
{
    public class ReservationsRepository
    {
        private readonly AppDbContext _context;

        public ReservationsRepository(AppDbContext context)
        {
            _context = context;
        }

        //listar reservas
        public async Task<IEnumerable<Reservation>> ListarReservasAsync()
        {
            return await _context.Reservations.Include(r => r.User).Include(r => r.Room).ToListAsync(); ;
        }

        //listar reservas por id
        public async Task<IEnumerable<Reservation?>> ListarReservasPorIdAsync(int userId)
        {
            var reservas = await _context.Reservations.Where(r => r.UserId == userId).Include(r => r.User).Include(r => r.Room).ToListAsync();
            return reservas;
        }

        //Obtener reservas para aprobar y rechazar
        public async Task<Reservation?> ObtenerReservaAsync(int reservaId)
        {
            var reserva = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservaId);
            return reserva;
        }

        //crear reservas
        public async Task CrearReservasAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        //verficar solapamiento 
        public async Task<bool> ExisteSolapamientoAsync(int roomId, DateTime startTime, DateTime endTime)
        {
            return await _context.Reservations.AnyAsync(r => r.RoomId == roomId && r.Status != "rechazada" && startTime < r.EndTime && endTime > r.StartTime);
        }

        //guardar cambios
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
