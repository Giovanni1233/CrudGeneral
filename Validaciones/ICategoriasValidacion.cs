using Crud.Entrada.Response;
using Crud.Error.Response;

namespace Crud.Validaciones
{
    public interface ICategoriasValidacion
    {
        Task<object> ValidoParametros(CategoriaEntrada categoria, List<ParametrosInvalidos> parametrosInvalidos);
        Task<object> ValidoParametrosList(IFormFile categorias, List<ParametrosInvalidos> parametrosInvalidos);
    }
}