using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("estados_usuarios")]
    public class EstadoUsuariosModel
    {
        [Key]
        public int id_estado {get; set;}
        public string descripcion {get; set;}
    }
}