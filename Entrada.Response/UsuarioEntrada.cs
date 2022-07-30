using System.ComponentModel.DataAnnotations;

namespace Crud.Entrada.Response
{
    public class UsuarioEntrada
    {
        [MaxLength(30)]
        public string usuario { get; set; }

        [MaxLength(30)]

        public string clave { get; set; }

        [MaxLength(100)]
        public string correo { get; set; }

    }
}