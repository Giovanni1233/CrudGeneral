
using Microsoft.AspNetCore.Mvc;
using Crud.Models;
using primercrud.Funciones;
using System.Web.Http.Cors;
using Crud.Error.Response;
using Crud.Salida.Response;
using Crud.Entrada.Response;
using Crud.Validaciones;
using Crud.Funciones;
using System.Text;
using ExcelDataReader;

namespace Crud.Controllers;

[Route("api/categorias")]
[ApiController]
[EnableCors(origins: "*", headers: "*", methods: "*")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext dbcontext;
    private readonly ICategoriasFunction categoriaFunction;
    private readonly ICategoriasValidacion categoriaValidacion;

    public CategoriasController(AppDbContext _dbcontext, ICategoriasFunction _categoriaFunction, ICategoriasValidacion _categoriaValidacion)
    {
        this.dbcontext = _dbcontext;
        this.categoriaFunction = _categoriaFunction;
        this.categoriaValidacion = _categoriaValidacion;
    }

    [HttpGet("GetCategorias")]
    public async Task<object> GetCategorias()
    {
        var mensajeSalida = new MensajeSalida<List<MensajeSalidaCategorias>>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await categoriaFunction.GetCategorias(parametrosInvalidos);

            if (parametrosInvalidos.Count > 0)
            {
                var mensajeError = new MensajeSalidaErrores();
                mensajeError.codigoError = "400";
                mensajeError.codigoStatus = -1;
                mensajeError.parametrosInvalidos = parametrosInvalidos;
                mensajeError.titulo = "Error en la aplicacion";

                return mensajeError;
            }
            mensajeSalida.codigo = 200;
            mensajeSalida.descripcion = "Transacción exitosa";
            mensajeSalida.respuesta = mensajeSalida.respuesta;
        }
        catch (Exception ex)
        {
            var mensajeError = new MensajeSalidaErrores();
            mensajeError.codigoError = "500";
            mensajeError.codigoStatus = -1;
            mensajeError.parametrosInvalidos = parametrosInvalidos;
            mensajeError.titulo = ex.Message;

            return mensajeError;
        }

        return mensajeSalida;
    }

    [HttpPost("CategoriaAdd")]
    public async Task<object> AddCategoria(CategoriaEntrada categoria)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaCategorias>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            var x = categoriaValidacion.ValidoParametros(categoria, parametrosInvalidos);

            if (parametrosInvalidos.Count == 0)
            {
                mensajeSalida = await categoriaFunction.AddCategoria(categoria, parametrosInvalidos);
            }

            if (parametrosInvalidos.Count > 0)
            {
                var mensajeError = new MensajeSalidaErrores();
                mensajeError.codigoError = "400";
                mensajeError.codigoStatus = -1;
                mensajeError.parametrosInvalidos = parametrosInvalidos;
                mensajeError.titulo = "Error en la aplicacion";

                return mensajeError;
            }
            mensajeSalida.codigo = 200;
            mensajeSalida.descripcion = "Transacción exitosa";
            mensajeSalida.respuesta = mensajeSalida.respuesta;

        }
        catch (Exception ex)
        {
            var mensajeError = new MensajeSalidaErrores();
            mensajeError.codigoError = "400";
            mensajeError.codigoStatus = -1;
            mensajeError.parametrosInvalidos = parametrosInvalidos;
            mensajeError.titulo = ex.Message;

            return mensajeError;
        }
        return mensajeSalida;

    }

    [HttpPost("CategoriaAddMultiple")]
    public async Task<object> AddCategoriaMultiple(IFormFile categorias)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaCategoriasList>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            var x = categoriaValidacion.ValidoParametrosList(categorias, parametrosInvalidos);

            if (parametrosInvalidos.Count == 0)
            {
                mensajeSalida = await categoriaFunction.AddCategoriaList(categorias, parametrosInvalidos);
            }

            if (parametrosInvalidos.Count > 0)
            {
                var mensajeError = new MensajeSalidaErrores();
                mensajeError.codigoError = "400";
                mensajeError.codigoStatus = -1;
                mensajeError.parametrosInvalidos = parametrosInvalidos;
                mensajeError.titulo = "Error en la aplicacion";

                return mensajeError;
            }
            mensajeSalida.codigo = 200;
            mensajeSalida.descripcion = "Transacción exitosa";
            mensajeSalida.respuesta = mensajeSalida.respuesta;

        }
        catch (Exception ex)
        {
            var mensajeError = new MensajeSalidaErrores();
            mensajeError.codigoError = "400";
            mensajeError.codigoStatus = -1;
            mensajeError.parametrosInvalidos = parametrosInvalidos;
            mensajeError.titulo = ex.Message;

            return mensajeError;
        }
        return mensajeSalida;
        
    }
}