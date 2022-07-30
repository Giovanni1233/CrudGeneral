using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.Models
{
    [Table("detalle_notificacion")]
    public class DetalleNotificacionesModel
    {
        [Key]
        public int id_detalle { get; set; }
        public int id_notificacion { get; set; }
        public int tipo_notificacion { get; set; }
        public string descripcion { get; set; }
        
    }
}