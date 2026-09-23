using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Services;

namespace Prueba_Tecnica.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }

   
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ListarSalas()
        {
            var salas = await _roomService.ListarSalasAsync();
            return Ok(salas);
        }

      
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CrearSala([FromBody] RoomCrearDto model)
        {
 
           var NuevaSala = await _roomService.CrearSalasAsync(model.Name, model.Capacity, model.Location);
           return Ok(new { message = "Sala creada con éxito", sala = NuevaSala });
          
        }
    }

}