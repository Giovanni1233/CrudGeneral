using Crud.Entrada.Response;
using Crud.Error.Response;
using ExcelDataReader;

namespace Crud.Validaciones
{
    public class CategoriasValidacion : ICategoriasValidacion
    {

        public Task<object> ValidoParametros(CategoriaEntrada categoria, List<ParametrosInvalidos> parametrosInvalidos)
        {
            if (categoria.nombre_categoria == null || categoria.nombre_categoria == "")
            {
                parametrosInvalidos.Add(new ParametrosInvalidos("El nombre de la categoria no puede estar vacio", "Error interno"));
            }
            return null;
        }

        public Task<object> ValidoParametrosList(IFormFile categorias, List<ParametrosInvalidos> parametrosInvalidos)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                categorias.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        var valor = reader.GetValue(0).ToString();
                        if (valor != "Nombre")
                        {
                            if (valor.All(char.IsDigit))
                            {
                                parametrosInvalidos.Add(new ParametrosInvalidos("El nombre de la categoria no puede contener numero vacio", "Error interno"));
                            }
                            if (valor == "")
                            {
                                parametrosInvalidos.Add(new ParametrosInvalidos("El nombre de la categoria no puede estar vacio", "Error interno"));
                            }
                       
                        }
                    }
                }
            }
            return null;
        }
    }
}