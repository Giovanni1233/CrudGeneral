using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("modulos")]
    public class ModulosModel
    {
        [Key]
        public int id_modulo { get; set; }
        public string nombre_modulo { get; set; }
        public string icono { get; set; }
        public string ruta { get; set; }
        public int ordenamiento { get; set; }
    }
}