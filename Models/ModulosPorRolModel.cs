using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("modulos_por_rol")]
    public class ModulosPorRolModel
    {
        [Key]
        public int id_modulo_rol { get; set; }
        public int id_rol { get; set; }
        public int id_modulo { get; set; }
    }
}