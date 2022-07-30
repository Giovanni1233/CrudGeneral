using Crud.Entrada.Response;
using Crud.Error.Response;
using Crud.Salida.Response;

namespace Crud.Funciones
{
    public interface ICategoriasFunction
    {
        Task<MensajeSalida<List<MensajeSalidaCategorias>>> GetCategorias(List<ParametrosInvalidos> parametrosInvalidos);
        Task<MensajeSalida<MensajeSalidaCategorias>> AddCategoria(CategoriaEntrada categoria, List<ParametrosInvalidos> parametrosInvalidos);
        Task<MensajeSalida<MensajeSalidaCategoriasList>> AddCategoriaList(IFormFile categoriaList, List<ParametrosInvalidos> parametrosInvalidos);
    }
}