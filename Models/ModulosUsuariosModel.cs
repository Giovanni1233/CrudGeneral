using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("modulos_favoritos_usuarios")]
    public class ModulosUsuariosModel
    {
        [Key]
        public int id_mfu { get; set; }
        public int id_modulo { get; set; }
        public int id_usuario { get; set; }
    }
}