namespace Crud.Error.Response
{
    public class ParametrosInvalidos
    {
        public ParametrosInvalidos(string nombre, string razon)
        {
            this.nombre = nombre;
            this.razon = razon;
        }

        public string nombre {get; set;}
        public string razon {get; set;}
    }
}