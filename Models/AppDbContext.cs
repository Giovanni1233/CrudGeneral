using Microsoft.EntityFrameworkCore;

namespace Crud.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UsuarioModel> usuarios { get; set; }

        public DbSet<DetalleUsuarioModel> usuariosDetalle { get; set; }

        public DbSet<ModulosModel> Modulos { get; set; }
        public DbSet<ModulosPorRolModel> ModulosRol { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet<ModulosUsuariosModel> ModulosUsuarios { get; set; }
        public DbSet<CategoriasModel> Categorias { get; set; }
        public DbSet<NotificacionesModel> Notificaciones { get; set; }
        public DbSet<DetalleNotificacionesModel> DetalleNotificaciones { get; set; }
        public DbSet<NotificacionesTypesModel> TiposNotificaciones { get; set; }
    }
}