namespace Crud.Error.Response
{
    public class MensajeSalidaErrores
    {
        public string codigoError {get; set;}
        public string titulo {get; set;}
        public int codigoStatus {get; set;}
        public List<ParametrosInvalidos> parametrosInvalidos {get;set;}
    }
}