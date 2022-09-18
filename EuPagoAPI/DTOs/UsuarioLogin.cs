using System.ComponentModel.DataAnnotations;

namespace EuPagoAPI.DTOs
{
    public class UsuarioLogin
    {
        [Required]
        public long CPF { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
