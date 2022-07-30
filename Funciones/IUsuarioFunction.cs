using Crud.Error.Response;
using Crud.Entrada.Response;
using Crud.Models;
using Crud.Salida.Response;

namespace primercrud.Funciones
{
    public interface IUsuarioFunction
    {
        Task<MensajeSalida<List<MensajeSalidaUsuario>>> GetUsuarios(List<ParametrosInvalidos> parametrosInvalidos);
        Task<MensajeSalida<MensajeSalidaUsuario>> GetUsuarioById(int id, List<ParametrosInvalidos> parametrosInvalidos);
        Task<MensajeSalida<MensajeSalidaUsuario>> AddUsuario(UsuarioEntrada user, List<ParametrosInvalidos> parametrosInvalidos);
       
    }
}