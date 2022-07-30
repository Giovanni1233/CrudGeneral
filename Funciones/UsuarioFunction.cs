using System.Data.Common;
using System.Text;
using Crud.Entrada.Response;
using Crud.Error.Response;
using Crud.Funciones;
using Crud.Models;
using Crud.Salida.Response;
using Microsoft.EntityFrameworkCore;

namespace primercrud.Funciones
{
    public class UsuarioFunction : IUsuarioFunction
    {
        private readonly AppDbContext dbcontext;

        public UsuarioFunction(AppDbContext _dbContext)
        {
            this.dbcontext = _dbContext;
        }
        public async Task<MensajeSalida<List<MensajeSalidaUsuario>>> GetUsuarios(List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<List<MensajeSalidaUsuario>>();
            try
            {
                var listadoUsers = await (from i in dbcontext.usuarios
                                          join y in dbcontext.usuariosDetalle on i.id equals y.id_usuario
                                          select new MensajeSalidaUsuario
                                          {
                                              id_detalle = y.id_detalle,
                                              id_usuario = y.id_usuario,
                                              nombres = y.nombres,
                                              apellidos = y.apellidos,
                                              edad = y.edad,
                                              rut = y.rut,
                                              correo = y.correo
                                          }).ToListAsync();

                if (listadoUsers == null)
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos("No se encontraron registros", "Error interno"));
                    salida = null;
                }
                salida.respuesta = listadoUsers;

            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error interno"));
                salida = null;
            }


            return salida;
        }

        public async Task<MensajeSalida<MensajeSalidaUsuario>> GetUsuarioById(int id, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaUsuario>();
            try
            {
                var listadoUsers = await (from i in dbcontext.usuarios
                                          join x in dbcontext.usuariosDetalle on i.id equals x.id_usuario
                                          select new MensajeSalidaUsuario
                                          {
                                              id_detalle = x.id_detalle,
                                              id_usuario = x.id_usuario,
                                              nombres = x.nombres,
                                              apellidos = x.apellidos,
                                              edad = x.edad,
                                              rut = x.rut,
                                              correo = x.correo
                                          })
                .FirstOrDefaultAsync();

                if (listadoUsers == null)
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos("No se encotraron registros", "Error interno"));
                    salida = null;
                }
                else
                {
                    salida.respuesta = listadoUsers;
                }

            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error"));
                return null;
            }
            return salida;
        }
        
        public async Task<MensajeSalida<MensajeSalidaUsuario>> AddUsuario(UsuarioEntrada user, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaUsuario>();
            try
            {
                var validar = await dbcontext.usuarios
                .Where(x => (x.usuario == user.usuario) && x.clave == user.clave)
                .FirstOrDefaultAsync();



                if (validar == null)
                {
                    var usuarioE = new UsuarioModel();
                    usuarioE.usuario = user.usuario;
                    usuarioE.clave = user.clave;
                    usuarioE.estado = 1;
                    dbcontext.usuarios.Add(usuarioE);
                    dbcontext.SaveChanges();

                    var detaUsua = new DetalleUsuarioModel();
                    detaUsua.id_usuario = await (from i in dbcontext.usuarios
                                                 where i.usuario == usuarioE.usuario && i.clave == usuarioE.clave
                                                 select i.id).FirstOrDefaultAsync();
                    detaUsua.nombres = "";
                    detaUsua.apellidos = "";
                    detaUsua.rut = "";
                    detaUsua.edad = 0;
                    dbcontext.usuariosDetalle.Add(detaUsua);
                    dbcontext.SaveChanges();

                    var u = new MensajeSalidaUsuario();
                    u.id_usuario = usuarioE.id;
                    u.nombres = "";
                    u.apellidos = "";
                    u.correo = "";
                    u.edad = 0;
                    salida.respuesta = u;
                }
                else
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos("El usuario ya se encuentra en la base de datos", "Error"));
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


    }
}