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
        private readonly AuthRepository _authRepository;

        public UserService(UserRepository userRepository, AuthRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;

        }

        public async Task<IEnumerable<UserListaDto>> ListarUsuariosAsync()
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
            
            var existeEmail = await _authRepository.ObtenerEmailAsync(dto.Email);

            if (existeEmail != null)
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