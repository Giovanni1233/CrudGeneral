using Crud.Entrada.Response;
using Crud.Error.Response;
using Crud.Salida.Response;

namespace Crud.Funciones
{
    public interface IModuloFunction
    {
        Task<MensajeSalida<List<MensajeSalidaModulosByRol>>> GetModulosByRol(MensajeEntradaRolesUser rolesUser, List<ParametrosInvalidos> parametrosInvalidos);
        Task<MensajeSalida<MensajeSalidaModulosGrabar>> GuardarFavoritoModulo(MensajeEntradaUserModulo userModulo, List<ParametrosInvalidos> parametrosInvalidos);
    }
}