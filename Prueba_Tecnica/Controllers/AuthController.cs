using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Models.DTOs;
using Prueba_Tecnica.Models.Entities;
using Prueba_Tecnica.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prueba_Tecnica.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
  
             var user = await _authService.LoginAsync(dto.Email, dto.Password);

              
             var token = GenerateJwtToken(user);

             return Ok(new
             {
                token,
                    user = new
                    {
                        id = user.id,
                        email = user.email,
                        fullName = user.fullName,
                        role = user.role
                    }
                });
            }
  
      
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Name, user.fullName),
                new Claim(ClaimTypes.Role, user.role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"] ?? "120")),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}