using INTELISIS.APPCORE.BL;
using INTELISIS.APPCORE.EL;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketsADN7.Models;

namespace TicketsADN7.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LoginResponse?> LoginAsync(LoginModel loginModel)
        {
            if (!string.IsNullOrEmpty(loginModel.username) && !string.IsNullOrEmpty(loginModel.password))
            {
                Usuario result = UsuarioBusiness.IniciarSesion(loginModel);

                if (result.Resultado != "Error")
                {
                    return new LoginResponse
                    {
                        Usuario = result,
                        Token = GenerateJwtToken(result.NombreUsuario)
                    };
                }
                return new LoginResponse
                {
                    Usuario = result,
                    Token = null
                };
            }
            return null;
        }

        public string? GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}