using Crud.Error.Response;
using Crud.Salida.Response;

namespace Crud.Funciones
{
    public interface INotificacionesFunction
    {
        Task<MensajeSalida<List<MensajeSalidaNotificaciones>>> GetNotificacionesByUser(int idUser, List<ParametrosInvalidos> parametrosInvalidos);
        MensajeSalida<int> GetCountNotificaciones(int idUser, List<ParametrosInvalidos> parametrosInvalidos);
        Task<MensajeSalida<MensajeSalidaNotificacionesTypes>> GetCountNotificacionesTypes(int idUser, List<ParametrosInvalidos> parametrosInvalidos);
    }
}