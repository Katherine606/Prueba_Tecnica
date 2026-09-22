using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Models.Entities;
using Prueba_Tecnica.Repositories;
using Prueba_Tecnica.Respositories;

namespace Prueba_Tecnica.Services
{
    public class ReservationService
    {
        private readonly ReservationsRepository _reservationRepository;
        private readonly AppDbContext _context;

        public ReservationService(ReservationsRepository reservationRepository, AppDbContext context)
        {
            _reservationRepository = reservationRepository;
            _context = context;
        }


       
        public async Task<IEnumerable<ReservaListaDto>> ListarReservasAsync()
        {
            var reservas = await _reservationRepository.ListarReservasAsync();

            if (reservas == null || !reservas.Any())
            {
                throw new ApiException("No hay reservas", 400);
            }


            return reservas.Select(r => new ReservaListaDto
            {
                Id = r.Id,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Status = r.Status,
                UsuarioNombre = r.User != null ? r.User.fullName : "",
                SalaNombre = r.Room != null ? r.Room.Name : ""
            }).ToList();
        }

        public async Task<IEnumerable<ReservaListaDto>> ObtenerReservasPorUsuarioAsync(int userId)
        {
            var reservas = await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Room)
                .ToListAsync();

            return reservas.Select(r => new ReservaListaDto
            {
                Id = r.Id,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Status = r.Status,
                UsuarioNombre = r.User != null ? r.User.fullName : "",
                SalaNombre = r.Room != null ? r.Room.Name : ""
            }).ToList();
        }


        public async Task<Reservation> CrearReservasAsync(ReservaCrearDto dto)
        {
            if (dto.StartTime < DateTime.Today)
            {
                throw new ApiException("La fecha de la reserva no puede ser anterior a la fecha actual.", 400);
            }

            if (dto.EndTime <= dto.StartTime)
            {
                throw new ApiException("La hora de finalización deberá ser posterior a la hora de inicio.", 400);
            }

            var reservaRoom = await _context.Rooms.FindAsync(dto.RoomId);
            if (reservaRoom == null) throw new ApiException("La sala no existe.", 404);

            var reservaUser = await _context.Users.FindAsync(dto.UserId);
            if (reservaUser == null) throw new ApiException("El usuario no existe.", 404);

            var reserva = new Reservation
            {
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = "pendiente",
                UserId = dto.UserId,
                RoomId = dto.RoomId
            };

            await _reservationRepository.CrearReservasAsync(reserva);
            await _reservationRepository.SaveChangesAsync();

            return reserva;
        }

        public async Task AprobarReservaAsync(int reservaId)
        {

            var reserva = await _reservationRepository.ObtenerReservaAsync(reservaId);

            if (reserva != null)
            {
                reserva.Status = "aprobada";
                
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApiException("La reserva no existe.", 404);
            }
        }

        public async Task RechazarReservaAsync(int reservaId)
        {

            var reserva = await _reservationRepository.ObtenerReservaAsync(reservaId);

            if (reserva != null)
            {
                reserva.Status = "rechazada";


                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApiException("La reserva no existe.", 404);
            }
        }
    }
}