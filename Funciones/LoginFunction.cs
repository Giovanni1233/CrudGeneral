using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Data.Common;
using Crud.Error.Response;
using Crud.Models;
using Microsoft.EntityFrameworkCore;
using Crud.Salida.Response;

namespace Crud.Funciones
{
    public class LoginFunction : ILoginFunction
    {
        private readonly AppDbContext dbcontext;

        public LoginFunction(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<MensajeSalida<MensajeSalidaLogin>> ValidarLogin(string usuario, string clave, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaLogin>();
            var respuesta = new MensajeSalidaLogin();

            var result = "";
            var valido = new UsuarioModel();
            try
            {

                respuesta = await (from x in dbcontext.usuarios
                                   join y in dbcontext.usuariosDetalle on x.id equals y.id_usuario
                                   
                                   where x.usuario == usuario && x.clave == clave && x.estado == 1
                                   select new MensajeSalidaLogin
                                   {
                                       id_detalle = y.id_detalle,
                                       id_usuario = y.id_usuario,
                                       nombres = y.nombres,
                                       apellidos = y.apellidos,
                                       edad = y.edad,
                                       rut = y.rut,
                                       correo = y.correo,
                                       idRol = x.id_rol,
                                       rol = (from n in dbcontext.Roles where n.id_rol == x.id_rol select n.nombre_rol).FirstOrDefault()

                                   }).FirstOrDefaultAsync();
                if (respuesta == null)
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos($"No se encontró información del usuario", "Error"));
                    return null;
                }
                salida.respuesta = respuesta;
            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error"));
                return null;
            }

            return salida;
        }



    }
}