using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_NUMEROS_CARNETS.Models;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : Controller
    {
        private readonly BlogDBContext _BlogDBContext;

        public usuariosController(BlogDBContext BlogDBContext)
        {
            _BlogDBContext = BlogDBContext;
        }

        // Para poder ver todos los registros:

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsuario = (from e in _BlogDBContext.usuarios select e).ToList();

            if (listadoUsuario.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuario);
        }

        // Para agregar un nuevo registro:

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuario([FromBody] usuarios usuario)
        {
            try
            {
                _BlogDBContext.usuarios.Add(usuario);
                _BlogDBContext.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Para actualizar un registro

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarUsuario(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioActual = (from e in _BlogDBContext.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (usuarioActual == null)
            { return NotFound(); }

            usuarioActual.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioActual.clave = usuarioModificar.clave;
            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.apellido = usuarioModificar.apellido;

            _BlogDBContext.Entry(usuarioActual).State = EntityState.Modified;
            _BlogDBContext.SaveChanges();

            return Ok(usuarioModificar);
        }

        // Para Eliminar un registro

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarUsuario(int id)
        {
            usuarios? usuario = (from e in _BlogDBContext.usuarios where e.usuarioId == id select e).FirstOrDefault();

            if (usuario == null)
            { return NotFound(); }

            _BlogDBContext.usuarios.Attach(usuario);
            _BlogDBContext.usuarios.Remove(usuario);
            _BlogDBContext.SaveChanges();

            return Ok(usuario);
        }
    }
}
