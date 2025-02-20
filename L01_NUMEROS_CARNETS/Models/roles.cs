using System.ComponentModel.DataAnnotations;
namespace L01_NUMEROS_CARNETS.Models
{
    public class roles
    {
        [Key]
        public int id_rol { get; set; }
        public string rol { get; set; }
    }
}
