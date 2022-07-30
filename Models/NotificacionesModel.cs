using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("Notificaciones")]
    public class NotificacionesModel
    {
        [Key]
        public int id_notificacion { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_notificacion { get; set; }
        public int leido { get; set; }
        
    }
}