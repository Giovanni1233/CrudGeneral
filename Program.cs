using System;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Crud.Models;
using primercrud.Funciones;
using Crud.Validaciones;
using Crud.Funciones;

internal class Program
{

    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        //Connection string
        var connectionString = builder.Configuration.GetConnectionString("AppDbContext");
        builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

        builder.Services.AddScoped<IUsuarioFunction, UsuarioFunction>();

        builder.Services.AddScoped<ILoginFunction, LoginFunction>();
        builder.Services.AddScoped<IModuloFunction, ModuloFunction>();
        builder.Services.AddScoped<ICategoriasFunction, CategoriasFunction>();
        builder.Services.AddScoped<INotificacionesFunction, NotificacionesFunction>();
        /* Validaciones */
        builder.Services.AddScoped<IUsuarioValidacion, UsuarioValidacion>();
        builder.Services.AddScoped<ICategoriasValidacion, CategoriasValidacion>();

        /* Scoped para usuarios */
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/usuarios/GetUsuarios") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/usuarios/UsuarioAdd") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/usuarios/UsuarioUpdate") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/usuarios/UsuarioListId") });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/modulos/GetModulosByUser") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/modulos/GuardarFavoritoModulo") });


        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/categorias/GetCategorias") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/categorias/CategoriaAdd") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/categorias/CategoriaAddMultiple") });
        // Login
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/login/ValidarUsuario") });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/notificaciones/GetNotificacionesByUser") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/notificaciones/GetCountNotificaciones") });
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://www.lekrak.somee.com/api/notificaciones/GetCountNotificacionesType") });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var myCorsPolicy = "MyCorsPolicy";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(myCorsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("x-my-custom-header");
                });
        });
        // ... other configuration

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/v1/swagger.json", "My API V1");
        });
        }
        app.UseCors(myCorsPolicy);
        app.MapControllers();
        app.Run();
    }
}