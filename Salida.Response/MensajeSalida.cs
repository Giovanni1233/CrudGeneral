namespace Crud.Error.Response
{
    public class MensajeSalida<T>
    {
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public T respuesta { get; set; }
    }
}