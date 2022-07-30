using Crud.Models;

namespace Crud.Error.Response
{
    public class MensajeSalidaLogin
    {
        public int id_detalle { get; set; }

        public int id_usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
        public string rut { get; set; }
        public string correo { get; set; }
        public int? idRol { get; set; }
        public string rol { get; set; }
    }
}