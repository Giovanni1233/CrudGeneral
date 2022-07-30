using System.Data;
using Crud.Entrada.Response;
using Crud.Error.Response;
using Crud.Models;
using Crud.Salida.Response;
using Microsoft.EntityFrameworkCore;

namespace Crud.Funciones
{
    public class ModuloFunction : IModuloFunction
    {
        private readonly AppDbContext dbcontext;

        public ModuloFunction(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<MensajeSalida<List<MensajeSalidaModulosByRol>>> GetModulosByRol(MensajeEntradaRolesUser rolesUser, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<List<MensajeSalidaModulosByRol>>();
            var mensajeSalidaList = new List<MensajeSalidaModulosByRol>();
            try
            {

                var listadoModulos = await (from i in dbcontext.Modulos
                                            join y in dbcontext.ModulosRol on i.id_modulo equals y.id_modulo
                                            where y.id_rol == rolesUser.idRol
                                            select i
                                         ).OrderBy(x => x.ordenamiento).ToListAsync();

                if (listadoModulos == null)
                {
                    parametrosInvalidos.Add(new ParametrosInvalidos("No se encontraron registros", "Error interno"));
                    salida = null;
                }

                foreach (var item in listadoModulos)
                {
                    MensajeSalidaModulosByRol msr = new MensajeSalidaModulosByRol();
                    var existe = (from i in dbcontext.ModulosUsuarios where i.id_usuario == rolesUser.idUsuario && i.id_modulo == item.id_modulo select i).FirstOrDefault();
                    if (existe != null)
                    {
                        msr.favorito = 1;
                    }
                    else
                    {
                        msr.favorito = 0;
                    }
                    msr.icono_modulo = item.icono;
                    msr.id_modulo = item.id_modulo;
                    msr.nombre_modulo = item.nombre_modulo;
                    msr.ordenamiento = item.ordenamiento;
                    msr.ruta_modulo = item.ruta;
                    mensajeSalidaList.Add(msr);
                }
                salida.respuesta = mensajeSalidaList;

            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error interno"));
                salida = null;
            }


            return salida;
        }

        public async Task<MensajeSalida<MensajeSalidaModulosGrabar>> GuardarFavoritoModulo(MensajeEntradaUserModulo userModulo, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaModulosGrabar>();
            var mensajeSalidaList = new MensajeSalidaModulosGrabar();
            ModulosUsuariosModel muser = new ModulosUsuariosModel();
            try
            {
                var existe = await (from i in dbcontext.ModulosUsuarios
                                    where i.id_modulo == userModulo.idModulo && i.id_usuario == userModulo.idUsuario
                                    select i).ToListAsync();
                if (existe.Count == 0 || existe == null)
                {
                    muser.id_modulo = userModulo.idModulo;
                    muser.id_usuario = userModulo.idUsuario;
                    dbcontext.Add(muser);
                    var valorReturn = await dbcontext.SaveChangesAsync();
                    if (valorReturn > 0)
                    {
                        mensajeSalidaList.respuesta = true;
                        mensajeSalidaList.mensaje = "El módulo ha sido añadido a favoritos";
                    }
                    else
                    {
                        mensajeSalidaList.respuesta = false;
                        mensajeSalidaList.mensaje = "Ha ocurrido un problema al añadir el módulo";
                    }
                }
                else
                {
                    foreach (var item in existe)
                    {
                        dbcontext.Remove(item);
                    }

                    var valorReturn = await dbcontext.SaveChangesAsync();
                    if (valorReturn > 0)
                    {
                        mensajeSalidaList.respuesta = true;
                        mensajeSalidaList.mensaje = "El módulo ha sido eliminado de favoritos";
                    }
                    else
                    {
                        mensajeSalidaList.respuesta = false;
                        mensajeSalidaList.mensaje = "Ha ocurrido un problema al eliminar el módulo";
                    }
                }


            }
            catch (Exception ex)
            {
                mensajeSalidaList.respuesta = false;
                mensajeSalidaList.mensaje = "Ha ocurrido un problema en el sistema";
            }
            salida.respuesta = mensajeSalidaList;
            return salida;
        }
    }
}