using EjercicioTecnico.Helpers;
using EjercicioTecnico.Middleware;
using EjercicioTecnico.Models;
using EjercicioTecnico.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioTecnico.Controllers
{
    [ApiController]
    [Route("/Usuarios")]
    public class UsuarioController : ControllerBase
    {

        private readonly GenerateJwt _tokenGenerator;

        public UsuarioController(GenerateJwt tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        UsuarioValidator validator = new UsuarioValidator();

        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
