
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

[Route("api/notificaciones")]
[ApiController]
[EnableCors(origins: "*", headers: "*", methods: "*")]
public class NotificacionesController : ControllerBase
{
    private readonly AppDbContext dbcontext;
    private readonly INotificacionesFunction notificacionFunction;

    public NotificacionesController(AppDbContext _dbcontext, INotificacionesFunction _notificacionFunction)
    {
        this.dbcontext = _dbcontext;
        this.notificacionFunction = _notificacionFunction;
    }

    [HttpGet("GetNotificacionesByUser/{idUser}")]
    public async Task<object> GetNotificacionesByUser(int idUser)
    {
        var mensajeSalida = new MensajeSalida<List<MensajeSalidaNotificaciones>>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await notificacionFunction.GetNotificacionesByUser(idUser, parametrosInvalidos);

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
    [HttpGet("GetCountNotificaciones/{idUser}")]
    public async Task<object> GetCountNotificaciones(int idUser)
    {
        var mensajeSalida = new MensajeSalida<int>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = notificacionFunction.GetCountNotificaciones(idUser, parametrosInvalidos);

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

     [HttpGet("GetCountNotificacionesType/{idUser}")]
    public async Task<object> GetCountNotificacionesType(int idUser)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaNotificacionesTypes>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await notificacionFunction.GetCountNotificacionesTypes(idUser, parametrosInvalidos);

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
}