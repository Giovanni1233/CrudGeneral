using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("tipos_notificaciones")]
    public class NotificacionesTypesModel
    {
        [Key]
        public int id_tipo_notificacion { get; set; }
        public string descripcion { get; set; }
        
    }
}