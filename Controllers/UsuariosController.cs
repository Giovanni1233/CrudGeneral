
using Microsoft.AspNetCore.Mvc;
using Crud.Models;
using primercrud.Funciones;
using System.Web.Http.Cors;
using Crud.Error.Response;
using Crud.Salida.Response;
using Crud.Entrada.Response;
using Crud.Validaciones;

namespace Crud.Controllers;

[Route("api/usuarios")]
[ApiController]
[EnableCors(origins: "*", headers: "*", methods: "*")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext dbcontext;
    private readonly IUsuarioFunction usuarioFunction;
    private readonly IUsuarioValidacion usuarioValidacion;

    public UsuariosController(AppDbContext _dbcontext, IUsuarioFunction _usuarioFunction, IUsuarioValidacion userValidacion)
    {
        this.dbcontext = _dbcontext;
        this.usuarioFunction = _usuarioFunction;
        this.usuarioValidacion = userValidacion;
    }


    [HttpGet("GetUsuarios")]
    public async Task<object> GetUsuarios()
    {
        var mensajeSalida = new MensajeSalida<List<MensajeSalidaUsuario>>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await usuarioFunction.GetUsuarios(parametrosInvalidos);

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


    [HttpGet("UsuarioListId/{id}")]
    public async Task<object> UsuarioListId(int id)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaUsuario>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await usuarioFunction.GetUsuarioById(id, parametrosInvalidos);

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


    [HttpPost("UsuarioAdd")]
    public async Task<object> AddUsuarios(UsuarioEntrada usuario)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaUsuario>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            var x = usuarioValidacion.ValidoParametros(usuario, parametrosInvalidos);

            if (parametrosInvalidos.Count == 0)
            {
                mensajeSalida = await usuarioFunction.AddUsuario(usuario, parametrosInvalidos);
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

// Hola Mundo
}
