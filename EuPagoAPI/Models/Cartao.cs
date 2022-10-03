using EuPagoAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_CARTAO")]
    public class Cartao
    {
        public Cartao()
        {
        }

        [Key]
        [Column("ID_CARTAO")]
        public decimal Id { get; set; }

        [Column("NM_IMPRESSO")]
        [Required(ErrorMessage = "Nome impresso no cartão é obrigatório.")]
        [MaxLength(100)]
        public string NomeImpresso { get; set; }

        [Column("NR_CARTAO")]
        [Required(ErrorMessage = "Número do cartão é obrigatório.")]
        public long Numero { get; set; }

        [Column("DT_VALIDADE")]
        [Required(ErrorMessage = "Data de validade é obrigatório.")]
        public DateTime DataValidade { get; set; }

        [Column("NR_CVV")]
        [MinLength(3)]
        [MaxLength(4)]
        [Required(ErrorMessage = "CVV é obrigatório.")]
        public string CVV { get; set; }

        [Column("DS_BANDEIRA")]
        [Required(ErrorMessage = "Nome da emissora é obrigatório.")]
        public string Emissora { get; set; }
        
        public Usuario Usuario { get; set; }

        [Column("ID_USUARIO")]
        [ForeignKey("Usuario")]
        public decimal UsuarioId { get; set; }

        [Column("ST_CADASTRO")]
        public string StatusCadastro { get; set; }
    }
}
