namespace Crud.Salida.Response
{
    public class MensajeSalidaUsuario
    {
        public int id_detalle { get; set; }

        public int id_usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
        public string rut { get; set; }
        public string correo { get; set; }
       
    }
}