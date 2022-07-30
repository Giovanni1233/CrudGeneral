using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class PersonasModel
    {
        [Key]
        public int id_persona { get; set; }
        public string usuario { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
    }
}