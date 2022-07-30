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

[Route("api/login")]
[ApiController]
[EnableCors(origins: "*", headers: "*", methods: "*")]
public class LoginController : ControllerBase
{

    private readonly ILoginFunction loginFunction;
    private readonly IUsuarioValidacion usuarioValidacion;

    public LoginController(ILoginFunction _loginFunction, IUsuarioValidacion userValidacion)
    {
        this.loginFunction = _loginFunction;
        this.usuarioValidacion = userValidacion;
    }

    [HttpPost("ValidarUsuario")]
    public async Task<object> ValidarUsuario(UsuarioLoginEntrada user)
    {
        var mensajeSalida = new MensajeSalida<MensajeSalidaLogin>();
        var parametrosInvalidos = new List<ParametrosInvalidos>();
        try
        {
            var x = usuarioValidacion.ValidoParametrosLogin(user, parametrosInvalidos);

            if (parametrosInvalidos.Count == 0)
            {
                mensajeSalida = await loginFunction.ValidarLogin(user.usuario, user.clave, parametrosInvalidos);
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
            mensajeError.codigoError = "500";
            mensajeError.codigoStatus = -1;
            mensajeError.parametrosInvalidos = parametrosInvalidos;
            mensajeError.titulo = ex.Message;

            return mensajeError;
        }

        return mensajeSalida;
    }

  
}
