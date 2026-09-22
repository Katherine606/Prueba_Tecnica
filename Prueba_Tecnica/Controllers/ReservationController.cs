using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Services;
using System.Security.Claims;


namespace Prueba_Tecnica.Controllers
{
    [Route("api/Reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarMisReservas()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                var misReservas = await _reservationService.ObtenerReservasPorUsuarioAsync(userId);
                return Ok(misReservas);
            }

            return Unauthorized();
        }

        [HttpGet("todas")]
        public async Task<IActionResult> ListarReservas()
        {
            var reservas = await _reservationService.ListarReservasAsync();
            return Ok(reservas);
        }


        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] ReservaCrearDto dto)
        {

            var NuevaReserva = await _reservationService.CrearReservasAsync(dto);
            return Ok(new { message = "Reserva creada con éxito", sala = NuevaReserva });

        }

        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> AprobarReserva(int id)
        {
           
            await _reservationService.AprobarReservaAsync(id);

            return Ok(new { mensaje = "Reserva aprobada con éxito" });
        }

        [HttpPatch("{id}/decline")]
        public async Task<IActionResult> RechazarReserva(int id)
        {
            
            await _reservationService.RechazarReservaAsync(id);

            return Ok(new { mensaje = "Reserva rechazada con éxito" });
        }
    }

}