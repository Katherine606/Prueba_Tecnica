using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models;
using Prueba_Tecnica.Models.Entities;
using Prueba_Tecnica.Respositories;

namespace Prueba_Tecnica.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;

        public AuthService(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        //validar credenciales del login
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _authRepository.ObtenerUsuarioPorEmailAsync(email);
            if (user == null)
            {
                throw new ApiException("Credenciales inválidas.", 401);
            }

            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, user.password);

            if (!passwordValido)
            {
                throw new ApiException("Credenciales inválidas.", 401);
            }

            return user;
        }

        //devuelve booleano si existe ese email
        //innecesario
        //public async Task<Boolean> ValidarEmailExisteAsyn(string email)
        //{
        //    var emailV = _authRepository.ObtenerEmailAsync(email);

        //    if(email != null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
