using Microsoft.AspNetCore.Http;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Models.Entities;
using Prueba_Tecnica.Repositories;
using Prueba_Tecnica.Respositories;
using System.Security.Claims;

namespace Prueba_Tecnica.Services
{
    public class ReservationService
    {
        private readonly ReservationsRepository _reservationRepository;
        private readonly RoomsRepository _roomsRepository;
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationService(
            ReservationsRepository reservationRepository,
            RoomsRepository roomsRepository,
            UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _reservationRepository = reservationRepository;
            _roomsRepository = roomsRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Listar todas las reservas (general)
        public async Task<IEnumerable<ReservaListaDto>> ListarReservasAsync()
        {
            var reservas = await _reservationRepository.ListarReservasAsync();

            if (reservas == null || !reservas.Any())
            {
                throw new ApiException("No hay reservas", 404);
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

        // Listar las reservas del usuario autenticado automáticamente
        public async Task<IEnumerable<ReservaListaDto>> ListarMisReservasAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new ApiException("No autorizado o token inválido.", 401);
            }

            var reservas = await _reservationRepository.ListarReservasPorIdAsync(userId);

            if (reservas == null)
            {
                throw new ApiException("No tienes reservas registradas.", 404);
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

        // Crear reservas asignando el usuario del token automáticamente
        public async Task CrearReservasAsync(ReservaCrearDto dto)
        {
            if (dto.StartTime < DateTime.Today)
            {
                throw new ApiException("La hora de la reserva no puede ser anterior a la fecha actual.", 400);
            }

            if (dto.EndTime <= dto.StartTime)
            {
                throw new ApiException("La hora de finalización deberá ser posterior a la hora de inicio.", 400);
            }

            var cruze = await _reservationRepository.ExisteSolapamientoAsync(dto.RoomId, dto.StartTime, dto.EndTime);

            if (cruze)
            {
                throw new ApiException("La sala ya se encuentra reservada en ese rango de horario.", 400);
            }

            var reservaRoom = await _roomsRepository.ObtenerSalaAsync(dto.RoomId);

            if (reservaRoom == null) throw new ApiException("La sala no existe.", 404);

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new ApiException("No autorizado para crear reservas.", 401);
            }

            var reserva = new Reservation
            {
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = "pendiente",
                UserId = userId,
                RoomId = dto.RoomId
            };

            await _reservationRepository.CrearReservasAsync(reserva);
            await _reservationRepository.SaveChangesAsync();

        }

        public async Task AprobarReservaAsync(int reservaId)
        {
            var reserva = await _reservationRepository.ObtenerReservaAsync(reservaId);

            if (reserva != null)
            {
                reserva.Status = "aprobada";
                await _reservationRepository.SaveChangesAsync();
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
                await _reservationRepository.SaveChangesAsync();
            }
            else
            {
                throw new ApiException("La reserva no existe.", 404);
            }
        }
    }
}