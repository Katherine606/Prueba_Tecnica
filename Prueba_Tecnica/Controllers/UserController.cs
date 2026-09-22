using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Services;

namespace Prueba_Tecnica.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var users = await _userService.ListarUsuariosAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UserCrearDto dto)
        {
   
           var nuevoUser = await _userService.CrearUsuarioAsync(dto);
           return StatusCode(201, new { message = "Usuario creado exitosamente.", user = nuevoUser });
            
       
        }
    }
}