using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
     [Table("categorias")]
    public class CategoriasModel
    {
        [Key]
        public int id_categoria {get; set;}
        public string descripcion {get; set;}
        public int estado {get; set;}
    }
}