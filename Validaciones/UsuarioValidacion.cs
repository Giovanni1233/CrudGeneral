using Crud.Entrada.Response;
using Crud.Error.Response;

namespace Crud.Validaciones
{
    public class UsuarioValidacion : IUsuarioValidacion
    {
        Task<object> IUsuarioValidacion.ValidoParametros(UsuarioEntrada user, List<ParametrosInvalidos> parametrosInvalidos)
        {
            if (user.usuario == null || user.usuario == "")
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("El nombre de usuario no puede estar vacio", "Error interno"));
            }

            if (user.clave == null || user.clave == "")
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("La contraseña no puede estar vacia", "Error interno"));
            }

            if (user.correo == null || user.correo == "")
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("El correo no puede estar vacio", "Error interno"));
            }

            return null;
        }

        Task<object> IUsuarioValidacion.ValidoParametrosLogin(UsuarioLoginEntrada user, List<ParametrosInvalidos> parametrosInvalidos)
        {
            if (user.usuario == null || user.usuario == "")
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("El nombre de usuario no puede estar vacio", "Error interno"));
            }

            if (user.clave == null || user.clave == "")
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("La contraseña no puede estar vacia", "Error interno"));
            }


            return null;
        }

        Task<object> IUsuarioValidacion.ValidoParametrosModulos(int idRol, List<ParametrosInvalidos> parametrosInvalidos)
        {
           
            if (idRol == null)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("El rol no puede ser vacio", "Error interno"));
            }

            return null;
        }

        
    }
}