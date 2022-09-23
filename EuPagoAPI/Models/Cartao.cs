using System.ComponentModel.DataAnnotations;

namespace EuPagoAPI.Models
{
    public class Cartao
    {
        public Cartao()
        {
        }

        [Key]
        public long Id { get; set; }
        public long Numero { get; set; }
        public DateTime DataValidade { get; set; }
        public int CVV { get; set; }
        public string Emissora { get; set; }
        public long UsuarioId { get; set; }

        Usuario Usuario { get; set; }
    }
}
