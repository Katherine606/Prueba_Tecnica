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
        public async Task<IEnumerable<Reservation>> ListarReservasAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> ListarReservasPorIdAsync(int userId)
        {
            var reservas = await _context.Reservations.Where(r => r.UserId == userId).Include(r => r.User).Include(r => r.Room).ToListAsync();

            return reservas;
        }

        public async Task<Reservation> ObtenerReservaAsync(int reservaId)
        {
            var reserva = await _context.Reservations.Where(r => r.Id == reservaId).FirstAsync();

            return reserva;
        }



        public async Task CrearReservasAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
