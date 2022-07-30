using Crud.Entrada.Response;
using Crud.Error.Response;
using Crud.Models;
using Crud.Salida.Response;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace Crud.Funciones
{
    public class CategoriasFunction : ICategoriasFunction
    {
        private readonly AppDbContext dbcontext;

        public CategoriasFunction(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<MensajeSalida<MensajeSalidaCategorias>> AddCategoria(CategoriaEntrada categoria, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaCategorias>();
            MensajeSalidaCategorias scategoria = new MensajeSalidaCategorias();
            try
            {
                var validar = await dbcontext.Categorias
                .Where(x => (x.descripcion == categoria.nombre_categoria))
                .FirstOrDefaultAsync();



                if (validar == null)
                {
                    var categoriaIn = new CategoriasModel();
                    categoriaIn.descripcion = categoria.nombre_categoria;
                    categoriaIn.estado = 1;
                    dbcontext.Categorias.Add(categoriaIn);
                    dbcontext.SaveChanges();

                    var salidaC = await dbcontext.Categorias
                                .Where(x => (x.descripcion == categoria.nombre_categoria))
                                .FirstOrDefaultAsync();
                    scategoria.estado = "Activo";
                    scategoria.id_categoria = salidaC.id_categoria;
                    scategoria.nombre_categoria = salidaC.descripcion;

                    salida.respuesta = scategoria;
                }
                else
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos("La categoria ya se encuentra en la base de datos", "Error"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error"));
                return null;
            }
            return salida;
        }

        public async Task<MensajeSalida<MensajeSalidaCategoriasList>> AddCategoriaList(IFormFile categoriaList, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaCategoriasList>();
            MensajeSalidaCategoriasList salidaList = new MensajeSalidaCategoriasList();
            var contador = 0;
            var contadoringresado = 0;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                categoriaList.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        var valor = reader.GetValue(0).ToString();
                        if (valor != "Nombre")
                        {
                            var existe = await dbcontext.Categorias
                                    .Where(x => (x.descripcion == valor))
                                    .FirstOrDefaultAsync();
                            if (existe == null)
                            {
                                var categoriaIn = new CategoriasModel();
                                categoriaIn.descripcion = valor;
                                categoriaIn.estado = 1;
                                dbcontext.Categorias.Add(categoriaIn);
                                dbcontext.SaveChanges();
                                contadoringresado = contadoringresado + 1;
                            }
                            else
                            {
                                contador = contador + 1;
                            }
                        }
                    }
                    var mensaje = "Las categorias ingresadas fueron: " + contadoringresado + ", y las existentes fueron: " + contador;
                    salidaList.respuesta = mensaje;
                    salida.respuesta = salidaList;
                }
            }
            return salida;
        }

        public async Task<MensajeSalida<List<MensajeSalidaCategorias>>> GetCategorias(List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<List<MensajeSalidaCategorias>>();
            try
            {
                var listadoCategorias = await (from i in dbcontext.Categorias
                                               select new MensajeSalidaCategorias
                                               {
                                                   id_categoria = i.id_categoria,
                                                   nombre_categoria = i.descripcion,
                                                   estado = i.estado == 1 ? "Activo" : "Inactivo"
                                               }).ToListAsync();

                if (listadoCategorias == null)
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos("No se encontraron registros", "Error interno"));
                    salida = null;
                }
                salida.respuesta = listadoCategorias;

            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error interno"));
                salida = null;
            }


            return salida;
        }
    }
}