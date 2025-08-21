using EjercicioTecnico.Models;
using EjercicioTecnico.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioTecnico.Controllers
{
    [ApiController]
    [Route("/Usuarios")]
    public class UsuarioController : ControllerBase
    {

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
                return _usuarioService.AddUsuario(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensaje = $"Error del servidor: {e.Message}" });
            }

        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetIdUsuario([FromRoute] int id) {
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
        public ActionResult<Usuario> DeleteUsuario([FromRoute] int id) {
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
        public ActionResult<Usuario> UpdateUsuario([FromRoute] int id, [FromBody] Usuario data)
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
