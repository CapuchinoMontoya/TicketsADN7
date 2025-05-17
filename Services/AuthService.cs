using DocumentFormat.OpenXml.Spreadsheet;
using INTELISIS.APPCORE.BL;
using INTELISIS.APPCORE.EL;
using LIBRARY.COMMON.Crypto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly string _key;
        private readonly TicketsContext _context;

        public AuthService(IConfiguration configuration, IOptions<EncryptionSettings> encryptionOptions, TicketsContext context)
        {
            _configuration = configuration;
            _key = encryptionOptions.Value.Key;
            _context = context;
        }

        public async Task<LoginResponse?> LoginAsync(LoginModel loginModel)
        {
            if (string.IsNullOrWhiteSpace(loginModel.username) || string.IsNullOrWhiteSpace(loginModel.password))
                return null;

            // Buscar el usuario por nombre de usuario o email
            var usuario = await _context.Usuario
                .Where(u => u.NombreUsuario == loginModel.username || u.Email == loginModel.username)
                .FirstOrDefaultAsync();

            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Contrasena))
                return new LoginResponse { Usuario = new Usuario { Resultado = "Error", Mensaje= "Usuario no encontrado" }, Token = null };

            // Desencriptar y comparar contraseñas
            var contrasenaDesencriptada = CryptionHelper.Decrypt(usuario.Contrasena, _key);

            if (contrasenaDesencriptada != loginModel.password)
                return new LoginResponse { Usuario = new Usuario { Resultado = "Error", Mensaje = "Contraseña incorrecta" }, Token = null };

            // Opcional: sobreescribe con la encriptada si tu lógica de negocio lo requiere
            loginModel.password = usuario.Contrasena;

            // Validar inicio de sesión
            var usuarioAutenticado = UsuarioBusiness.IniciarSesion(loginModel);

            if (usuarioAutenticado.Resultado != "Error")
            {
                var token = GenerateJwtToken(usuarioAutenticado.NombreUsuario, usuarioAutenticado.RolID);
                return new LoginResponse { Usuario = usuarioAutenticado, Token = token };
            }

            return new LoginResponse { Usuario = usuarioAutenticado, Token = null };
        }


        public string? GenerateJwtToken(string username, int rol)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, rol.ToString()),
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