using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_NUMEROS_CARNETS.Models;

namespace L01_NUMEROS_CARNETS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly BlogDBContext _BlogDBContext;

        public calificacionesController(BlogDBContext BlogDBContext)
        {
            _BlogDBContext = BlogDBContext;
        }

        // Para poder ver todos los registros:

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Index()
        {
            List<calificaciones> listadocalificaciones = (from e in _BlogDBContext.calificaciones select e).ToList();

            if (listadocalificaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadocalificaciones);
        }
        

        // Para agregar un nuevo registro:

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarCalificacion([FromBody] calificaciones calificaciones)
        {
            try
            {
                _BlogDBContext.calificaciones.Add(calificaciones);
                _BlogDBContext.SaveChanges();
                return Ok(calificaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Para actualizar un registro

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult Actualizarcalificacion(int id, [FromBody] calificaciones calificacionModificar)
        {
            calificaciones? calificacionActual = (from e in _BlogDBContext.calificaciones where e.calificacionId == id select e).FirstOrDefault();

            if (calificacionActual == null)
            { return NotFound(); }

            calificacionActual.publicacionId = calificacionModificar.publicacionId;
            calificacionActual.usuarioId = calificacionModificar.usuarioId;
            calificacionActual.calificacion = calificacionModificar.calificacion;

            _BlogDBContext.Entry(calificacionActual).State = EntityState.Modified;
            _BlogDBContext.SaveChanges();

            return Ok(calificacionModificar);
        }

        // Para Eliminar un registro

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarcalificacion(int id)
        {
            calificaciones? calificaciones = (from e in _BlogDBContext.calificaciones where e.calificacionId == id select e).FirstOrDefault();

            if (calificaciones == null)
            { return NotFound(); }

            _BlogDBContext.calificaciones.Attach(calificaciones);
            _BlogDBContext.calificaciones.Remove(calificaciones);
            _BlogDBContext.SaveChanges();

            return Ok(calificaciones);
        }

        //Retornar el listado por una publicación en especifico

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            var calificaciones = (from e in _BlogDBContext.publicaciones
                                 join t in _BlogDBContext.calificaciones
                                 on e.publicacionId equals t.publicacionId
                                 where e.publicacionId == id
                                 select new
                                 {
                                     e.publicacionId,
                                     e.titulo,
                                     e.descripcion,
                                     calificaciones = (from e in _BlogDBContext.publicaciones
                                                       join t in _BlogDBContext.calificaciones
                                                       on e.publicacionId equals t.publicacionId
                                                       where e.publicacionId == id
                                                       select new
                                                       {
                                                           t.calificacionId,
                                                           t.calificacion
                                                       }).ToList()
                                 }).FirstOrDefault();

            if (calificaciones == null)
            {
                return NotFound();
            }
            return Ok(calificaciones);
        }
    }
}
