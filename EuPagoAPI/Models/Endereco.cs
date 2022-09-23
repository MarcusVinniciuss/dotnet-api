using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_ENDERECO")]
    public class Endereco
    {
        [Column("ID_ENDERECO")]
        [Key]
        public decimal Id { get; set; }

        [Column("DS_CIDADE")]
        [StringLength(100)]
        [Required]
        public string Cidade { get; set; }

        [Column("DS_RUA")]
        [StringLength(200)]
        [Required]
        public string Rua { get; set; }

        [Column("DS_BAIRRO")]
        [StringLength(200)]
        [Required]
        public string Bairro { get; set; }

        [Column("NR_ENDERECO")]
        [Required]
        public int Numero { get; set; }

        [Column("DS_COMPLEMENTO")]
        [StringLength(200)]
        [Required]
        public string Complemento { get; set; }

        [Column("DS_CEP")]
        [StringLength(10)]
        [Required]
        public string CEP { get; set; }
        Usuario Usuario { get; set; }

        [Column("ID_USUARIO")]
        [ForeignKey("Usuario")]
        public decimal UsuarioId { get; set; }

    }
}
