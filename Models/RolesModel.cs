using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("roles")]
    public class RolesModel
    {
        [Key]
        public int id_rol { get; set; }
        public string nombre_rol { get; set; }
    }
}