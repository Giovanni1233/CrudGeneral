using System.ComponentModel.DataAnnotations;

namespace Crud.Entrada.Response
{
    public class UsuarioLoginEntrada
    {
        [MaxLength(30)]
        public string? usuario { get; set; }

        [MaxLength(30)]

        public string? clave { get; set; }
    }
}