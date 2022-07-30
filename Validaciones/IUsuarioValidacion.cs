using Crud.Entrada.Response;
using Crud.Error.Response;

namespace Crud.Validaciones
{
    public interface IUsuarioValidacion
    {
      Task<object> ValidoParametros(UsuarioEntrada user, List<ParametrosInvalidos> parametrosInvalidos);
      Task<object> ValidoParametrosLogin(UsuarioLoginEntrada user, List<ParametrosInvalidos> parametrosInvalidos);
       Task<object> ValidoParametrosModulos(int idRol, List<ParametrosInvalidos> parametrosInvalidos);
    }
}