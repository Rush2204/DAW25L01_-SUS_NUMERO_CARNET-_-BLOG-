using System.ComponentModel.DataAnnotations;
namespace L01_NUMEROS_CARNETS.Models
{
    public class usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        public int id_rol { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
    }
}
