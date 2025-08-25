using EjercicioTecnico.Middleware;
using EjercicioTecnico.Models;
using EjercicioTecnico.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EjercicioTecnico.Controllers
{
    [ApiController]
    [Route("/Usuarios")]
    public class UsuarioController : ControllerBase
    {
        UsuarioValidator validator = new UsuarioValidator();

        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _configuration; 

        public UsuarioController(UsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] Usuario model)
        {
            var token = "";

            if(model.Nombre.Equals("Admin"))  token = GenerateJwtToken(model);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Usuario user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Nombre)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddMinutes(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        [HttpGet]
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            try
            {
                return _usuarioService.GetUsuarios();

            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {e.Message}" });
            }
        }

        [HttpPost]
        public ActionResult<Usuario> PostUsuario([FromBody] Usuario data) {
            try
            {
                ValidationResult results = validator.Validate(data);

                if (!results.IsValid)
                {
                    var errores = results.Errors.Select(error => new
                    {
                        Campo = error.PropertyName,
                        Mensaje = error.ErrorMessage
                    });

                    return BadRequest(new
                    {
                        mensaje = "Error de validación",
                        errores = errores
                    });
                }

                return _usuarioService.AddUsuario(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {e.Message}" });
            }

        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetIdUsuario([FromRoute] Guid id) {
            try
            {
                return _usuarioService.GetByIdUsuario(id);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {e.Message}" });
            }
        
        }

        [HttpDelete("{id}")]
        public ActionResult<Usuario> DeleteUsuario([FromRoute] Guid id) {
            try
            {
                return _usuarioService.DeleteUsuario(id);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {e.Message}" });
            }
        }

        [HttpPatch("{id}")]
        public ActionResult<Usuario> UpdateUsuario([FromRoute] Guid id, [FromBody] Usuario data)
        {
            try
            {
                return _usuarioService.UpdateUsuario(id, data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {e.Message}" });
            }

        }
    }

}
