using L01_NUMEROS_CARNETS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly BlogDBContext _BlogDBContext;

        public comentariosController(BlogDBContext BlogDBContext)
        {
            _BlogDBContext = BlogDBContext;
        }

        // Para poder ver todos los registros:

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listadoComentario = (from e in _BlogDBContext.comentarios select e).ToList();

            if (listadoComentario.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoComentario);
        }

        // Para agregar un nuevo registro:

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarComentario([FromBody] comentarios comentario)
        {
            try
            {
                _BlogDBContext.comentarios.Add(comentario);
                _BlogDBContext.SaveChanges();
                return Ok(comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Para actualizar un registro

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarComentario(int id, [FromBody] comentarios comentarioModificar)
        {
            comentarios? comentarioActual = (from e in _BlogDBContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentarioActual == null)
            { return NotFound(); }

            comentarioActual.comentario = comentarioActual.comentario;

            _BlogDBContext.Entry(comentarioActual).State = EntityState.Modified;
            _BlogDBContext.SaveChanges();

            return Ok(comentarioModificar);
        }

        // Para Eliminar un registro

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarUsuario(int id)
        {
            comentarios? comentario = (from e in _BlogDBContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (comentario == null)
            { return NotFound(); }

            _BlogDBContext.comentarios.Attach(comentario);
            _BlogDBContext.comentarios.Remove(comentario);
            _BlogDBContext.SaveChanges();

            return Ok(comentario);
        }
    }
}
