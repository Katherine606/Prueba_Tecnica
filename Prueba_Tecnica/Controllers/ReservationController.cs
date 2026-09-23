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
        [Authorize(Roles ="User, Admin")]
        public async Task<IActionResult> ListarMisReservas()
        {
            var misReservas = await _reservationService.ListarMisReservasAsync();
            return Ok(misReservas);
        }

        [HttpGet("todas")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarReservas()
        {
            var reservas = await _reservationService.ListarReservasAsync();
            return Ok(reservas);
        }


        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CrearReserva([FromBody] ReservaCrearDto dto)
        {
            await _reservationService.CrearReservasAsync(dto);
            return Ok(new { mensaje = "Reserva creada con éxito" });
        }

        [HttpPatch("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AprobarReserva(int id)
        {
           
            await _reservationService.AprobarReservaAsync(id);
            return Ok(new { mensaje = "Reserva aprobada con éxito" });
        }

        [HttpPatch("{id}/decline")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RechazarReserva(int id)
        {
            
            await _reservationService.RechazarReservaAsync(id);

            return Ok(new { mensaje = "Reserva rechazada con éxito" });
        }
    }

}