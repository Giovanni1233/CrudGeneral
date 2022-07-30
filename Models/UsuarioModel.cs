using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class UsuarioModel
    {
        [Key]
        public int id { get; set; }
        public string usuario { get; set; }

        public string clave { get; set; }

        public int? estado { get; set; }
        public int? id_rol { get; set; }


    }
}