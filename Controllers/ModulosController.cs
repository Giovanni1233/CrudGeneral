
using Microsoft.AspNetCore.Mvc;
using Crud.Models;
using primercrud.Funciones;
using System.Web.Http.Cors;
using Crud.Error.Response;
using Crud.Salida.Response;
using Crud.Entrada.Response;
using Crud.Validaciones;
using Crud.Funciones;

namespace Crud.Controllers;

[Route("api/modulos")]
[ApiController]
[EnableCors(origins: "*", headers: "*", methods: "*")]
public class ModulosController : ControllerBase
{
    private readonly AppDbContext dbcontext;
    private readonly IModuloFunction moduloFunction;

    public ModulosController(AppDbContext _dbcontext, IModuloFunction _moduloFunction)
    {
        this.dbcontext = _dbcontext;
        this.moduloFunction = _moduloFunction;
    }

    [HttpPost("GetModulosByUser")]
    public async Task<object> GetModulosByRolYUser(MensajeEntradaRolesUser roles)
    {
        var mensajeSalida = new MensajeSalida<List<MensajeSalidaModulosByRol>>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await moduloFunction.GetModulosByRol(roles, parametrosInvalidos);

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

    [HttpPost("GuardarFavoritoModulo")]
    public async Task<object> GuardarFavoritoModulo(MensajeEntradaUserModulo UserModulo)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaModulosGrabar>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            mensajeSalida = await moduloFunction.GuardarFavoritoModulo(UserModulo, parametrosInvalidos);

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
