using Crud.Error.Response;
using Crud.Salida.Response;

namespace Crud.Funciones
{
    public interface ILoginFunction
    {
        Task<MensajeSalida<MensajeSalidaLogin>> ValidarLogin(string usuario, string clave, List<ParametrosInvalidos> parametrosInvalidos);

    }
}