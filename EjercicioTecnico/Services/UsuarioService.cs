using EjercicioTecnico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioTecnico.Services
{
    public class UsuarioService : Controller
    {

        List<Usuario> usuarios = new List<Usuario>();

        public ActionResult<List<Usuario>> GetUsuarios()
        {
            try
            {
                if (usuarios == null || !usuarios.Any())
                {
                    return StatusCode(400, new { mensaje = "No se encontraron usuarios." });
                }


                return Ok(usuarios);

            }
            catch (Exception e)
            {
                return StatusCode(500, new 
                { 
                    mensaje = $"Error del servidor: {e.Message}" 
                });
            }
        }

        public ActionResult<Usuario> AddUsuario(Usuario data)
        {
            try
            {
                int CantidadAntes = usuarios.Count;

                data.Id = CantidadAntes + 1;

                usuarios.Add(data);

                if (usuarios.Count == CantidadAntes + 1)
                {
                    return Ok(data);
                }

                return BadRequest( new
                { 
                    mensaje = "Error al añadir nuevo usuario"
                });
            }
            catch (Exception e) 
            {
                return StatusCode(500, new
                {
                    mensaje = $"Error del servidor : {e.Message}"
                });
            }
        }

        
    }
}
