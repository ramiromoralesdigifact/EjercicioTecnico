using EjercicioTecnico.Models;
using EjercicioTecnico.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioTecnico.Controllers
{
    [ApiController]
    [Route("/Usuarios")]
    public class UsuarioController : Controller
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
    }

}
