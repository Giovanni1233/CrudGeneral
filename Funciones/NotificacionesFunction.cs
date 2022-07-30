using Crud.Error.Response;
using Crud.Models;
using Crud.Salida.Response;
using Microsoft.EntityFrameworkCore;

namespace Crud.Funciones
{
    public class NotificacionesFunction : INotificacionesFunction
    {
        private readonly AppDbContext dbcontext;

        public NotificacionesFunction(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public MensajeSalida<int> GetCountNotificaciones(int idUser, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var result = new MensajeSalida<int>();
            try
            {
                var resultCount = (from i in dbcontext.Notificaciones
                                   join n in dbcontext.DetalleNotificaciones on i.id_notificacion equals n.id_notificacion
                                   join u in dbcontext.usuarios on i.id_usuario equals u.id
                                   join us in dbcontext.usuariosDetalle on u.id equals us.id_usuario
                                   where u.id == idUser
                                   select i.id_notificacion).Count();
                result.respuesta = resultCount;
            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error"));
                return null;
            }

            return result;
        }

        public async Task<MensajeSalida<MensajeSalidaNotificacionesTypes>> GetCountNotificacionesTypes(int idUser, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<MensajeSalidaNotificacionesTypes>();

            try
            {
                var resultList = await (from i in dbcontext.usuarios

                                           where i.id == idUser
                                           select new MensajeSalidaNotificacionesTypes()
                                           {
                                               countCorreo = countCorreo(),
                                               countFelicitacion = countFelicitacion(),
                                               countReclamo = countReclamo(),
                                               countSuspension = countSuspension()
                                           }).FirstOrDefaultAsync();
                salida.respuesta = resultList;
            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error"));
                return null;
            }
            return salida;
        }

        public async Task<MensajeSalida<List<MensajeSalidaNotificaciones>>> GetNotificacionesByUser(int idUser, List<ParametrosInvalidos> parametrosInvalidos)
        {
            var salida = new MensajeSalida<List<MensajeSalidaNotificaciones>>();

            try
            {
                var resultListNot = await (from i in dbcontext.Notificaciones
                                           join n in dbcontext.DetalleNotificaciones on i.id_notificacion equals n.id_notificacion
                                           join u in dbcontext.usuarios on i.id_usuario equals u.id
                                           join us in dbcontext.usuariosDetalle on u.id equals us.id_usuario
                                           join tn in dbcontext.TiposNotificaciones on n.tipo_notificacion equals tn.id_tipo_notificacion
                                           where u.id == idUser
                                           select new MensajeSalidaNotificaciones()
                                           {
                                               usuario = us.nombres,
                                               descripcion = n.descripcion,
                                               leido = i.leido,
                                               tipo_notificacion = tn.descripcion
                                           }).ToListAsync();
                salida.respuesta = resultListNot;
            }
            catch (Exception ex)
            {
                parametrosInvalidos.Add(new ParametrosInvalidos(ex.Message, "Error"));
                return null;
            }
            return salida;
        }

        public int countCorreo()
        {
            int valor = 0;
            valor = (from i in dbcontext.DetalleNotificaciones
                     join n in dbcontext.Notificaciones on i.id_notificacion equals n.id_notificacion
                     join m in dbcontext.TiposNotificaciones on i.tipo_notificacion equals m.id_tipo_notificacion
                     where i.tipo_notificacion == 3
                     select n).Count();
            return valor;
        }
        public int countFelicitacion()
        {
            int valor = 0;
            valor = (from i in dbcontext.DetalleNotificaciones
                     join n in dbcontext.Notificaciones on i.id_notificacion equals n.id_notificacion
                     join m in dbcontext.TiposNotificaciones on i.tipo_notificacion equals m.id_tipo_notificacion
                     where i.tipo_notificacion == 1
                     select n).Count();
            return valor;
        }
        public int countReclamo()
        {
            int valor = 0;
            valor = (from i in dbcontext.DetalleNotificaciones
                     join n in dbcontext.Notificaciones on i.id_notificacion equals n.id_notificacion
                     join m in dbcontext.TiposNotificaciones on i.tipo_notificacion equals m.id_tipo_notificacion
                     where i.tipo_notificacion == 2
                     select n).Count();
            return valor;
        }
        public int countSuspension()
        {
            int valor = 0;
            valor = (from i in dbcontext.DetalleNotificaciones
                     join n in dbcontext.Notificaciones on i.id_notificacion equals n.id_notificacion
                     join m in dbcontext.TiposNotificaciones on i.tipo_notificacion equals m.id_tipo_notificacion
                     where i.tipo_notificacion == 4
                     select n).Count();
            return valor;
        }
    }
}