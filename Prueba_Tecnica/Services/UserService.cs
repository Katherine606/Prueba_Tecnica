using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Models.DTOs.Prueba_Tecnica.DTOs;
using Prueba_Tecnica.Models.Entities;
using Prueba_Tecnica.Respositories;

namespace Prueba_Tecnica.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly AppDbContext _context;
        public UserService(UserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IEnumerable<UserListaDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.ListarUsuariosAsync();

          
            return users.Select(u => new UserListaDto
            {
                Id = u.id,
                Email = u.email,
                FullName = u.fullName,
                Role = u.role
            });
        }

        public async Task<UserListaDto> CrearUsuarioAsync(UserCrearDto dto)
        {
            
            var existeEmail = await _context.Users.AnyAsync(u => u.email == dto.Email);
            if (existeEmail)
            {
                throw new ApiException("El correo electrónico ya está registrado.", 400);
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                email = dto.Email,
                password = passwordHash, 
                fullName = dto.FullName,
                role = dto.Role
            };

            await _userRepository.CrearUsuario(user);
            await _userRepository.SaveChangesAsync();

            return new UserListaDto
            {
                Id = user.id,
                Email = user.email,
                FullName = user.fullName,
                Role = user.role
            };
        }
    }
}