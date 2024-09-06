using GestionEntrenamientoDeportivo.DTO;
using GestionEntrenamientoDeportivo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionEntrenamientoDeportivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        //Registro de Usuarios
        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult<Usuario>> RegistrarUsuario([FromBody] UsuarioRegistrado model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = model.UserName,
                    Email = model.CorreoElectronico,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Edad = model.Edad
                };

                var result = await _userManager.CreateAsync(user, model.Contrasena);

                if (result.Succeeded)
                {
                    // Asignar el rol "Usuario" por defecto
                    await _userManager.AddToRoleAsync(user, "Usuario");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var roles = await _userManager.GetRolesAsync(user); // Obtener los roles del usuario
                    var token = GenerateJwtToken(user,roles);
                    return Ok(new { message = "Se registró un Usuario exitosamente", token });
                }

                return BadRequest(new { message = "Error al registrar el usuario", errors = result.Errors });
            }

            return BadRequest(new { message = "Datos no válidos", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<object>> LoginUsuario([FromBody] Login model)
        {
            var user = await _userManager.FindByEmailAsync(model.CorreoElectronico);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Contrasena))
            {
                var roles = await _userManager.GetRolesAsync(user); // Obtener los roles del usuario
                var token = GenerateJwtToken(user, roles);
                return Ok(new { message = "Inicio de sesión exitoso", token });
            }

            return Unauthorized(new { message = "Contraseña o Usuario Inválido" });
        }


        private string GenerateJwtToken(Usuario user, IList<string> roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            // Agregar los roles como claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
